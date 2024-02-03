using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace AgrarianTradeSystemWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowReactApp")]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        public AuthController(DataContext context)
        {
            _context = context;
        }
        public static User user = new User();
        private readonly IConfiguration _configuration;

        //public AuthController(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.FirstName = request.First_Name;
            user.LastName = request.Last_Name;
            user.Email = request.Email;
            user.PhoneNumber = request.Phone;
            user.NIC = request.NICnumber;
            user.AddL1 = request.AddressLine1;
            user.AddL2 = request.AddressLine2;
            user.AddL3 = request.AddressLine3;
            
            //return Ok(user);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User created");
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {
            if(user.Username != request.Username)
            {
                return BadRequest("User or password is incorrect");
            }
            if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("User or password is incorrect");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    claims:claims,
                    expires:DateTime.Now.AddDays(1),
                    signingCredentials:cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}

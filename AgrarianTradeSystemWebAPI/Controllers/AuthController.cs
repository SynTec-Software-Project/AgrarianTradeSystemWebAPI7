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
using System.Security.Cryptography;

namespace AgrarianTradeSystemWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("ReactJSDomain")]
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
        public async Task<IActionResult> Register(UserDto request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("Email exist");
            }

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
            user.VerificationToken = CreateCustomToken();
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User created");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (loginuser == null)
            {
                return BadRequest("User or password is incorrect");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, loginuser.PasswordHash))
            {
                return BadRequest("User or password is incorrect");
            }
            if (loginuser.EmailVerified == false)
            {
                return BadRequest("Email is not verified");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (loginuser == null)
            {
                return BadRequest("Invalid Token");
            }
            loginuser.EmailVerified = true;
            loginuser.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return Ok("Email Verified");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (loginuser == null)
            {
                return BadRequest("Invalid Email!");
            }
            loginuser.PasswordResetToken = CreateCustomToken();
            loginuser.ResetTokenExpireAt = DateTime.Now.AddMinutes(10);
            await _context.SaveChangesAsync();
            return Ok("Reset within 10 minutes");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (loginuser == null || loginuser.ResetTokenExpireAt < DateTime.Now)
            {
                return BadRequest("Invalid Token!");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            loginuser.PasswordResetToken = null;
            loginuser.ResetTokenExpireAt = null;
            loginuser.PasswordResetToken = CreateCustomToken();
            loginuser.ResetTokenExpireAt = DateTime.Now.AddMinutes(10);
            await _context.SaveChangesAsync();
            return Ok("Password Successfully Reset");
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

        private string CreateCustomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}

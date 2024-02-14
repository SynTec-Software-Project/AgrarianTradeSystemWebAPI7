using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AgrarianTradeSystemWebAPI.Services.UserServices
{
    public class UserServices : IUserServices
    {
        public readonly DataContext _context;
        public readonly IConfiguration _configuration;
        public UserServices(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public static User user = new User();
        public async Task Register(UserDto request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                throw new EmailException("Email exist");
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
        }
        public async Task<string> Login(LoginDto request)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (loginuser == null)
            {
                throw new LoginException("Email or password is incorrect");
            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, loginuser.PasswordHash))
            {
                throw new LoginException("Email or password is incorrect");
            }
            if (loginuser.EmailVerified == false)
            {
                throw new LoginException("Email is not verified");
            }
            string token = CreateToken(loginuser);
            return (token);
        }
        public async Task<string> Verify(string token)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (loginuser == null)
            {
                throw new Exception("Invalid Token");
            }
            loginuser.EmailVerified = true;
            loginuser.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return ("Email Verified");
        }
        public async Task<string> ForgotPassword(ForgotPasswordDto request)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (loginuser == null)
            {
                throw new Exception("Invalid Email!");
            }
            loginuser.PasswordResetToken = CreateCustomToken();
            loginuser.ResetTokenExpireAt = DateTime.Now.AddMinutes(10);
            await _context.SaveChangesAsync();
            return ("Reset within 10 minutes");
        }
        public async Task<string> ResetPassword(ResetPasswordDto request)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (loginuser == null || loginuser.ResetTokenExpireAt < DateTime.Now)
            {
                throw new Exception("Invalid Token!");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            loginuser.PasswordResetToken = null;
            loginuser.ResetTokenExpireAt = null;
            loginuser.PasswordHash = passwordHash;
            await _context.SaveChangesAsync();
            return ("Password Successfully Reset");
        }
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        public string CreateCustomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}

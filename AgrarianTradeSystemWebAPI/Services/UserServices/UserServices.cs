using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models.RefreshToken;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using AgrarianTradeSystemWebAPI.Services.EmailService;
using Microsoft.AspNetCore.Identity;
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
        private readonly IEmailService _emailService;
        public UserServices(DataContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }

        public static User user = new User();
        public static Farmer farmer = new Farmer();
        public static Courier courier = new Courier();

        public async Task UserRegister(UserDto request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            var existingFarmer = await _context.Farmers.FirstOrDefaultAsync(u => u.Email == request.Email);
            var existingCourier = await _context.Couriers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null || existingFarmer != null || existingCourier != null)
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
        public async Task FarmerRegister(FarmerDto request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            var existingFarmer = await _context.Farmers.FirstOrDefaultAsync(u => u.Email == request.Email);
            var existingCourier = await _context.Couriers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null || existingFarmer != null || existingCourier != null)
            {
                throw new EmailException("Email exist");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            farmer.Username = request.Username;
            farmer.PasswordHash = passwordHash;
            farmer.FirstName = request.First_Name;
            farmer.LastName = request.Last_Name;
            farmer.Email = request.Email;
            farmer.PhoneNumber = request.Phone;
            farmer.NIC = request.NICnumber;
            farmer.AddL1 = request.AddressLine1;
            farmer.AddL2 = request.AddressLine2;
            farmer.AddL3 = request.AddressLine3;
            farmer.CropDetails = request.CropDetails;
            farmer.VerificationToken = CreateCustomToken();

            _context.Farmers.Add(farmer);
            await _context.SaveChangesAsync();
        }
        public async Task CourierRegister(CourierDto request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            var existingFarmer = await _context.Farmers.FirstOrDefaultAsync(u => u.Email == request.Email);
            var existingCourier = await _context.Couriers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null || existingFarmer != null || existingCourier != null)
            {
                throw new EmailException("Email exist");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            courier.Username = request.Username;
            courier.PasswordHash = passwordHash;
            courier.FirstName = request.First_Name;
            courier.LastName = request.Last_Name;
            courier.Email = request.Email;
            courier.PhoneNumber = request.Phone;
            courier.NIC = request.NICnumber;
            courier.AddL1 = request.AddressLine1;
            courier.AddL2 = request.AddressLine2;
            courier.AddL3 = request.AddressLine3;
            courier.VehicleNo = request.VehicleNumber;
            courier.VerificationToken = CreateCustomToken();

            _context.Couriers.Add(courier);
            await _context.SaveChangesAsync();
        }
        public async Task<TokenViewModel> Login(LoginDto request)
        {
            TokenViewModel _TokenViewModel = new();
            var loginUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            var loginFarmerUser = await _context.Farmers.FirstOrDefaultAsync(u => u.Email == request.Email);
            var loginCourierUser = await _context.Couriers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (loginUser != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, loginUser.PasswordHash))
                {
                    throw new LoginException("Email or password is incorrect");
                }
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loginUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "User")
                };
                _TokenViewModel.AccessToken = GenerateToken(authClaims);
                //_TokenViewModel.RefreshToken = GenerateRefreshToken();
                var _RefreshTokenValidityInDays = Convert.ToInt64(_configuration.GetSection("RefreshTokenValidityInDays").Value!);
                loginUser.RefreshToken = GenerateRefreshToken();
                loginUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(_RefreshTokenValidityInDays);
                await _context.SaveChangesAsync();
            }
            else if (loginFarmerUser != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, loginFarmerUser.PasswordHash))
                {
                    throw new LoginException("Email or password is incorrect");
                }
                if (loginFarmerUser.Approved == false)
                {
                    throw new LoginException("Your account has not yet been approved. Thank you for your patience.");
                }
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loginFarmerUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Role, "Farmer")
                };
                _TokenViewModel.AccessToken = GenerateToken(authClaims);
                //_TokenViewModel.RefreshToken = GenerateRefreshToken();
                var _RefreshTokenValidityInDays = Convert.ToInt64(_configuration.GetSection("RefreshTokenValidityInDays").Value!);
                loginFarmerUser.RefreshToken = GenerateRefreshToken();
                loginFarmerUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(_RefreshTokenValidityInDays);
                await _context.SaveChangesAsync();
            }
            else if (loginCourierUser != null)
            {
                if (!BCrypt.Net.BCrypt.Verify(request.Password, loginCourierUser.PasswordHash))
                {
                    throw new LoginException("Email or password is incorrect");
                }
                if (loginCourierUser.Approved == false)
                {
                    throw new LoginException("Your account has not yet been approved. Thank you for your patience.");
                }
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, loginCourierUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "Courier")
                };
                _TokenViewModel.AccessToken = GenerateToken(authClaims);
                //_TokenViewModel.RefreshToken = GenerateRefreshToken();
                var _RefreshTokenValidityInDays = Convert.ToInt64(_configuration.GetSection("RefreshTokenValidityInDays").Value!);
                loginCourierUser.RefreshToken = GenerateRefreshToken();
                loginCourierUser.RefreshTokenExpiryTime = DateTime.Now.AddDays(_RefreshTokenValidityInDays);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new LoginException("Email or password is incorrect");
            }
            return _TokenViewModel;
            //if (loginUser == null)
            //{
            //    throw new LoginException("Email or password is incorrect");
            //}
            //if (!BCrypt.Net.BCrypt.Verify(request.Password, loginUser.PasswordHash))
            //{
            //    throw new LoginException("Email or password is incorrect");
            //}
            //if (loginUser.EmailVerified == false)
            //{
            //    throw new LoginException("Email is not verified");
            //}
            //string token = CreateToken(loginUser);
            //var refreshToken = GenerateRefreshToken();
            //SetRefreshToken(refreshToken);
            //return (token);
        }
        public async Task<string> Verify(string token)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            var loginFarmeruser = await _context.Farmers.FirstOrDefaultAsync(u => u.VerificationToken == token);
            var loginCourieruser = await _context.Couriers.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (loginuser != null)
            {
                loginuser.EmailVerified = true;
                loginuser.VerifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else if (loginFarmeruser != null)
            {
                loginFarmeruser.EmailVerified = true;
                loginFarmeruser.VerifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else if (loginCourieruser != null)
            {
                loginCourieruser.EmailVerified = true;
                loginCourieruser.VerifiedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Invalid Token");
            }
            return ("Email Verified");
        }
        public async Task<string> ForgotPassword(ForgotPasswordDto request)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            var loginFarmeruser = await _context.Farmers.FirstOrDefaultAsync(u => u.Email == request.Email);
            var loginCourieruser = await _context.Couriers.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (loginuser != null)
            {
                loginuser.PasswordResetToken = CreateCustomToken();
                loginuser.ResetTokenExpireAt = DateTime.Now.AddMinutes(10);
                await _context.SaveChangesAsync();
                _emailService.passwordResetEmail(loginuser.Email, loginuser.PasswordResetToken);
            }
            else if (loginFarmeruser != null)
            {
                loginFarmeruser.PasswordResetToken = CreateCustomToken();
                loginFarmeruser.ResetTokenExpireAt = DateTime.Now.AddMinutes(10);
                await _context.SaveChangesAsync();
                _emailService.passwordResetEmail(loginFarmeruser.Email, loginFarmeruser.PasswordResetToken);
            }
            else if (loginCourieruser != null)
            {
                loginCourieruser.PasswordResetToken = CreateCustomToken();
                loginCourieruser.ResetTokenExpireAt = DateTime.Now.AddMinutes(10);
                await _context.SaveChangesAsync();
                _emailService.passwordResetEmail(loginCourieruser.Email, loginCourieruser.PasswordResetToken);
            }
            else
            {
                throw new Exception("Invalid Email!");
            }
            return ("Reset within 10 minutes");
        }
        public async Task<string> ResetPassword(ResetPasswordDto request)
        {
            var loginuser = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            var loginFarmeruser = await _context.Farmers.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            var loginCourieruser = await _context.Couriers.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (loginuser != null && loginuser.ResetTokenExpireAt > DateTime.Now)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
                loginuser.PasswordResetToken = string.Empty;
                loginuser.ResetTokenExpireAt = DateTime.MinValue.Date;
                loginuser.PasswordHash = passwordHash;
                await _context.SaveChangesAsync();
            }
            else if (loginFarmeruser != null && loginFarmeruser.ResetTokenExpireAt > DateTime.Now)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                loginFarmeruser.PasswordResetToken = string.Empty;
                loginFarmeruser.ResetTokenExpireAt = DateTime.MinValue.Date;
                loginFarmeruser.PasswordHash = passwordHash;
                await _context.SaveChangesAsync();
            }
            else if (loginCourieruser != null && loginCourieruser.ResetTokenExpireAt > DateTime.Now)
            {
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                loginCourieruser.PasswordResetToken = string.Empty;
                loginCourieruser.ResetTokenExpireAt = DateTime.MinValue.Date;
                loginCourieruser.PasswordHash = passwordHash;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Invalid Token!");
            }
            return ("Password Successfully Reset");
        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTKey:SecretKey").Value!));
            var TokenExpireTime = Convert.ToInt64(_configuration.GetSection("JWTKey:TokenExpiryTimeInHour").Value!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(TokenExpireTime),
                SigningCredentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        //public string CreateToken(User user)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Username)
        //    };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
        //        _configuration.GetSection("AppSettings:Token").Value!));
        //    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        //    var token = new JwtSecurityToken(
        //            claims: claims,
        //            expires: DateTime.Now.AddDays(1),
        //            signingCredentials: cred
        //        );
        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //    return jwt;
        //}
        public async Task<TokenViewModel> GetRefreshToken(GetRefreshTokenViewModel model)
        {
            TokenViewModel _TokenViewModel = new();
            var principal = GetPrincipalFromExpiredToken(model.AccessToken!);

            string? email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new Exception("Invalid token or refresh token");
            }
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var newAccessToken = GenerateToken(authClaims);
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newAccessToken;
            await _context.SaveChangesAsync();
            _TokenViewModel.AccessToken = newAccessToken;
            return _TokenViewModel;

        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTKey:SecretKey").Value!)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid Token");
            }
            return principal;
        }
        //public RefreshToken GenerateRefreshToken()
        //{
        //    var refreshToken = new RefreshToken
        //    {
        //        Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
        //        Expires = DateTime.Now.AddDays(1)
        //    };
        //    return refreshToken;
        //}
        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public string CreateCustomToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

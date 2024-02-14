using Microsoft.AspNetCore.Mvc;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using Microsoft.AspNetCore.Cors;
using AgrarianTradeSystemWebAPI.Services.UserServices;

namespace AgrarianTradeSystemWebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("ReactJSDomain")]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public AuthController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto request)
        {
            try
            {
                await _userServices.Register(request);
                return Ok("User created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            try
            {
                var result = await _userServices.Login(request);
                return Ok(result);
            }
            catch (LoginException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            try
            {
                var result = await _userServices.Verify(token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        //    //var loginuser = await _userServices._context.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
        //    //if (loginuser == null)
        //    //{
        //    //    return BadRequest("Invalid Token");
        //    //}
        //    //loginuser.EmailVerified = true;
        //    //loginuser.VerifiedAt = DateTime.Now;
        //    //await _userServices._context.SaveChangesAsync();
        //    //return Ok("Email Verified");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto request)
        {
            try
            {
                var result = await _userServices.ForgotPassword(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        //    //var loginuser = await _userServices._context.Users.FirstOrDefaultAsync(u => u.Email == email);
        //    //if (loginuser == null)
        //    //{
        //    //    return BadRequest("Invalid Email!");
        //    //}
        //    //loginuser.PasswordResetToken = CreateCustomToken();
        //    //loginuser.ResetTokenExpireAt = DateTime.Now.AddMinutes(10);
        //    //await _userServices._context.SaveChangesAsync();
        //    //return Ok("Reset within 10 minutes");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
        {
            try
            {
                var result = await _userServices.ResetPassword(request);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        //    var loginuser = await _userServices._context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
        //    if (loginuser == null || loginuser.ResetTokenExpireAt < DateTime.Now)
        //    {
        //        return BadRequest("Invalid Token!");
        //    }
        //    string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        //    loginuser.PasswordResetToken = null;
        //    loginuser.ResetTokenExpireAt = null;
        //    loginuser.PasswordResetToken = CreateCustomToken();
        //    loginuser.ResetTokenExpireAt = DateTime.Now.AddMinutes(10);
        //    await _userServices._context.SaveChangesAsync();
        //    return Ok("Password Successfully Reset");
        }
    }
}

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

        [HttpPost("UserRegister")]
        public async Task<IActionResult> Register(UserDto request)
        {
            try
            {
                await _userServices.UserRegister(request);
                return Ok("User created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("FarmerRegister")]
        public async Task<IActionResult> FarmerRegister(UserDto request)
        {
            try
            {
                await _userServices.UserRegister(request);
                return Ok("User created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CourierRegister")]
        public async Task<IActionResult> CourierRegister(UserDto request)
        {
            try
            {
                await _userServices.UserRegister(request);
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
        }
    }
}

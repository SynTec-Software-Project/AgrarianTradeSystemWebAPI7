using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace AgrarianTradeSystemWebAPI.Services.UserServices
{
    public interface IUserServices
    {
        public Task Register(UserDto request);
        public Task<string> Login(LoginDto request);
        public Task<string> Verify(string token);
        public Task<string> ForgotPassword(ForgotPasswordDto request);
        public Task<string> ResetPassword(ResetPasswordDto request);
        public string CreateToken(User user);
        public string CreateCustomToken();
    }
}

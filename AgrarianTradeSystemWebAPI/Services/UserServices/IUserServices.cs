using AgrarianTradeSystemWebAPI.Data;
using AgrarianTradeSystemWebAPI.Models.RefreshToken;
using AgrarianTradeSystemWebAPI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgrarianTradeSystemWebAPI.Services.UserServices
{
    public interface IUserServices
    {
        public Task UserRegister(UserDto request);
        public Task FarmerRegister(FarmerDto request);
        public Task CourierRegister(CourierDto request);
        public Task<TokenViewModel> Login(LoginDto request);
        //public Task<string> Login(LoginDto request);
        public Task<string> Verify(VerifyDto request);
        public Task<string> ForgotPassword(ForgotPasswordDto request);
        public Task<string> ResetPassword(ResetPasswordDto request);
        //public string CreateToken(User user);
        public string CreateCustomToken();
        Task<TokenViewModel> GetRefreshToken(GetRefreshTokenViewModel model);
        Task<string> GetVerifyLink(GetVerifyLinkDto request);
    }
}

using AgrarianTradeSystemWebAPI.Models.EmailModels;

namespace AgrarianTradeSystemWebAPI.Services.EmailService
{
    public interface IEmailService
    {
        void SendRegisterEmail(string to, string fname, string lname, string token);
        void passwordResetEmail(string to, string token);
        void verifyEmail(string to, string token);
    }
}

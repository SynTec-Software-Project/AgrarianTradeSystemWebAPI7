using AgrarianTradeSystemWebAPI.Models.EmailModels;

namespace AgrarianTradeSystemWebAPI.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
        void passwordResetEmail(string to, string token);
    }
}

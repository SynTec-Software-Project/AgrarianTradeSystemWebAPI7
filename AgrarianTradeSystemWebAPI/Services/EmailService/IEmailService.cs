using AgrarianTradeSystemWebAPI.Models.EmailModels;

namespace AgrarianTradeSystemWebAPI.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
    }
}

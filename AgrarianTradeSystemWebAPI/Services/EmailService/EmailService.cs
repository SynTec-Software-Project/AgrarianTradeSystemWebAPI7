using AgrarianTradeSystemWebAPI.Models.EmailModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;

namespace AgrarianTradeSystemWebAPI.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfig:EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailConfig:EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailConfig:EmailUsername").Value, _config.GetSection("EmailConfig:EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public void passwordResetEmail(string to, string token)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfig:EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Password Reset Request - Agrarian Trading System";
            email.Body = new TextPart(TextFormat.Html) 
            {
                Text = $"<html><body><p>Your password reset token is: {token}</p><p>Reset the password within 10 minutes</p></body></html>"
            };
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailConfig:EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailConfig:EmailUsername").Value, _config.GetSection("EmailConfig:EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}

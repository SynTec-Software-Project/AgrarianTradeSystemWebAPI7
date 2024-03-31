﻿using AgrarianTradeSystemWebAPI.Models.EmailModels;
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
        public void SendRegisterEmail(string to, string fname, string lname, string token)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfig:EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Successfully Registered To Agrarian Trading System";
            email.Body = new TextPart(TextFormat.Html) 
            { 
                Text = $"<html><body><h3>Hello {fname} {lname},</h3><p>You have successfully registered to the Agrarian Trading System." +
                $"One of our admin will review your application and accept withn 2-3 business days. Please use the following link to verify your email." +
                $"And it can confirm the acceptance your application</p>" +
                $"<a href='http://localhost:5173/verify-email?token={token}'></a></body></html>"
            };
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

        public void verifyEmail(string to, string token)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailConfig:EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = "Email Verification - Agrarian Trading System";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<html><body><p>Please click the following link to verify your email.</p>" +
                $"<a href='http://localhost:5173/verify-email?token={token}'>http://localhost:5173/verify-email?token={token}</a></body></html>"
            };
            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailConfig:EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailConfig:EmailUsername").Value, _config.GetSection("EmailConfig:EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

    }
}

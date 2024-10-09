using Kawkaba.BusinessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Kawkaba.BusinessLayer.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var _smtpServer = _configuration["SMTP:SmtpServer"];
                var _smtpPort = int.Parse(_configuration["SMTP:SmtpPort"]);
                var _smtpUsername = _configuration["SMTP:SmtpUsername"];
                var _smtpPassword = _configuration["SMTP:SmtpPassword"];

                // Validate email format
                if (!IsValidEmail(toEmail))
                {
                    throw new FormatException("The specified email is not in the correct format.");
                }

                var message = new MailMessage();
                message.To.Add(toEmail);
                message.Subject = subject;
                message.Body = body;
                message.From = new MailAddress("sender@example.com"); // Ensure this is a valid email

                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
                    smtpClient.EnableSsl = true;
                    await smtpClient.SendMailAsync(message);
                }
                return true;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
                }
        }

        // Helper method to validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}

using LoginTemplate.Application.Interfaces;
using LoginTemplate.Application.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace LoginTemplate.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public void SendEmail(Message msg)
        {
            var emailMessage = CreateEmailMessage(msg);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message msg)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
            emailMessage.To.AddRange(msg.To);
            emailMessage.Subject = msg.Subject;
            emailMessage.Body = new TextPart(TextFormat.Html) { Text = msg.Content };
            return emailMessage;
        }
        public void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);
                client.Send(mailMessage);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();

            }

        }
    }
}

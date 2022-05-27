using EmailSender.Interface;
using EmailSender.Model;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
using MailKit.Net.Smtp;

namespace EmailSender.Service
{

    public class EmailSenderService : IEmailSenderService
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailSenderService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<string> SendEmailAsync(string recipientEmail, string subject, string message)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_smtpSettings.SenderEmail));
            mail.To.Add(MailboxAddress.Parse(recipientEmail));
            mail.Subject = subject;

            var builder = new BodyBuilder();
            builder.HtmlBody = message;
            mail.Body = builder.ToMessageBody();

            var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, true);
                await client.AuthenticateAsync(new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password));
                await client.SendAsync(mail);
                await client.DisconnectAsync(true);
                return "Email Sent Successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}

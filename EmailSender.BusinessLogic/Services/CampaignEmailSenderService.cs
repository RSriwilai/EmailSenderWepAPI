using EmailSender.Interface;
using EmailSenderBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderBusinessLogic.Services
{
    public class CampaignEmailSenderService : ICampaignEmailSenderService
    {
        private readonly IEmailSenderService _emailSender;

        public CampaignEmailSenderService(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task<string> ExecuteSendCampaignEmail(string recipientEmail)
        {
            var subject = GetCampaignSubject();
            var body = GetCampaignBody();
            return await _emailSender.SendEmailAsync(recipientEmail, subject, body);
        }

        private string GetCampaignSubject()
        {
            var subjectText = "Hello and welcome to Test Company!";
            return subjectText;
        }

        private string GetCampaignBody()
        {
            string fileName = "CampaignMailBody.html";
            string path = Path.Combine(Environment.CurrentDirectory, @"Utilis\", fileName);
            var body = string.Empty;

            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
                reader.Close();
            };
            return body;
        }
    }
}

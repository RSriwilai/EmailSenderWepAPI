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

        public async Task<string> ExecuteSendCampaignEmail(string recipientFullName, string recipientEmail)
        {
            var subject = GetCampaignEmailSubject();
            var body = GetCampaignEmailBody(recipientFullName);
            return await _emailSender.SendEmailAsync(recipientEmail, subject, body);
        }

        private string GetCampaignEmailSubject()
        {
            var subjectText = "Hello and welcome!";
            return subjectText;
        }

        private string GetCampaignEmailBody(string recipientFullName)
        {

            string fileName = "CampaignMailBody.html";
            var backSlash = Path.DirectorySeparatorChar;
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string path = Path.Combine(solutiondir, "EmailSender" + backSlash + "EmailSender.Utilis" + backSlash + "EmailTemplates" + backSlash + fileName);

            var body = string.Empty;
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
                reader.Close();
            };

            body = body.Replace("{RecipientFullName}", recipientFullName);

            return body;
        }
    }
}

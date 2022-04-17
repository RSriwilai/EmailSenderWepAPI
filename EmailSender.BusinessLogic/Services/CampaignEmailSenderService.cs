using EmailSender.BusinessLogic.Interfaces;
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
        private readonly IContactService _contactService;

        public CampaignEmailSenderService(IEmailSenderService emailSender, IContactService contactService)
        {
            _emailSender = emailSender;
            _contactService = contactService;
        }

        public async Task<string> ExecuteSendCampaignEmail(string recipientFullName, string recipientEmail)
        {
            var subject = GetCampaignEmailSubject();
            var body = GetCampaignEmailBody(recipientFullName);

            var contacts = await _contactService.GetCollectionsOfContact();
            foreach (var contact in contacts.Where(x => recipientEmail.Contains(x.EmailAdress)))
            {
                if(string.IsNullOrWhiteSpace(contact.EmailAdress) == false)
                {
                    return await _emailSender.SendEmailAsync(contact.EmailAdress, subject, body);
                }
            }
            return "Value cannot be null or the recipient emailadress is not included in the database.";
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

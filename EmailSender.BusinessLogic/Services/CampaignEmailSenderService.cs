using EmailSender.BusinessLogic.Interfaces;
using EmailSender.DataAccess;
using EmailSender.Interface;
using EmailSender.Models;
using EmailSender.Models.CampaignEmailSender;
using EmailSenderBusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmailSender.DataAccess.Enums;

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

        public async Task<string> ExecuteSendCampaignEmail(CampaignEmailInput input)
        {
            var contacts = await _contactService.GetCollectionsOfContact();

            foreach (var contact in contacts.Where(x => input.RecipientEmail.Contains(x.EmailAdress)))
            {
                if(string.IsNullOrWhiteSpace(contact.EmailAdress) == false)
                {
                    var subject = GetCampaignEmailSubject();
                    var body = GetCampaignEmailBody(contact.FirstName);
                    return await _emailSender.SendEmailAsync(contact.EmailAdress, subject, body);
                }
            }
            return "Value cannot be null or the recipient emailadress is not included in the database.";
        }

        public async Task<string> ExecuteSendCampaignToMultipleEmails(CampaignEmailToMultipleRecipientsInput input)
        {
            var contacts = await _contactService.GetCollectionsOfContact();
            if (contacts == null)
                return "Contacts could not be found!";

            foreach (var contact in contacts.Where(x => input.RecipientsEmail.Contains(x.EmailAdress)))
            {
                if (string.IsNullOrWhiteSpace(contact.EmailAdress) == false)
                {
                    var subject = GetCampaignEmailSubject();
                    var body = GetCampaignEmailBody(contact.FirstName);
                    await _emailSender.SendEmailAsync(contact.EmailAdress, subject, body);
                }
            }
            return "Email Sent Successfully";
        }

        public async Task<string> ExecuteSendCampaignToAllEmails(CampaignEmailToAllRecipientInput input)
        {
            var contacts = await _contactService.GetCollectionsOfContact();

            if(contacts == null)
                return "Contacts could not be found!";

            foreach (var contact in contacts)
            {
                if (input.Contacts == Enums.Contacts.All)
                {
                    var subject = GetCampaignEmailSubject();
                    var body = GetCampaignEmailBody(contact.FirstName);
                    await _emailSender.SendEmailAsync(contact.EmailAdress, subject, body);
                }
            }
            return "Email Sent Successfully";
        }

        private string GetCampaignEmailSubject()
        {
            var subjectText = "Hello and welcome!";
            return subjectText;
        }

        private string GetCampaignEmailBody(string recipientFirstName)
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

            body = body.Replace("{RecipientFullName}", recipientFirstName);

            return body;
        }
    }
}

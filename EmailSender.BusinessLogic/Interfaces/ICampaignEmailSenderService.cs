using EmailSender.DataAccess;
using EmailSender.Models;
using EmailSender.Models.CampaignEmailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmailSender.DataAccess.Enums;

namespace EmailSenderBusinessLogic.Interfaces
{
    public interface ICampaignEmailSenderService
    {
        Task<string> ExecuteSendCampaignEmail(CampaignEmailInput input);
        Task<string> ExecuteSendCampaignToMultipleEmails(CampaignEmailToMultipleRecipientsInput input);
        Task<string> ExecuteSendCampaignToAllEmails(CampaignEmailToAllRecipientInput input);
    }
}

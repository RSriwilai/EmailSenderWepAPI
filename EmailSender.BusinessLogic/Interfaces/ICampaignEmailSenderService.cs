using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSenderBusinessLogic.Interfaces
{
    public interface ICampaignEmailSenderService
    {
        Task<string> ExecuteSendCampaignEmail(string recipientFullName, string recipientEmail);
    }
}

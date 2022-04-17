using EmailSender.Interface;
using EmailSenderBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EmailSender.Controllers
{
    public class CampaignEmailSenderController : Controller
    {
        private readonly ICampaignEmailSenderService _campaignEmailSenderService;

        public CampaignEmailSenderController(ICampaignEmailSenderService campaignEmailSenderService)
        {
            _campaignEmailSenderService = campaignEmailSenderService;
        }

        [HttpPost, Route("SendCampaignEmailAsync")]
        public async Task<IActionResult> SendCampaignEmailAsync(string recipientFullName, string recipientEmail)
        {
            var result = await _campaignEmailSenderService.ExecuteSendCampaignEmail(recipientFullName, recipientEmail);

            if (result is null)
            {
                return BadRequest(result);
            }

            return Ok(result);
            
        }
    }
}

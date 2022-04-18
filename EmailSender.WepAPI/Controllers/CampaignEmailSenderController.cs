using EmailSender.DataAccess;
using EmailSender.Interface;
using EmailSender.Models;
using EmailSender.Models.CampaignEmailSender;
using EmailSenderBusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using static EmailSender.DataAccess.Enums;

namespace EmailSender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignEmailSenderController : Controller
    {
        private readonly ICampaignEmailSenderService _campaignEmailSenderService;

        public CampaignEmailSenderController(ICampaignEmailSenderService campaignEmailSenderService)
        {
            _campaignEmailSenderService = campaignEmailSenderService;
        }

        [HttpPost, Route("SendCampaignEmailAsync")]
        public async Task<IActionResult> SendCampaignEmailAsync(CampaignEmailInput input)
        {
            var result = await _campaignEmailSenderService.ExecuteSendCampaignEmail(input);

            if (result is null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost, Route("SendCampaignEmailToMultipleRecipientsAsync")]
        public async Task<IActionResult> SendCampaignEmailToMultipleRecipientsAsync(CampaignEmailToMultipleRecipientsInput input)
        {
            var result = await _campaignEmailSenderService.ExecuteSendCampaignToMultipleEmails(input);

            if (result is null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost, Route("SendCampaignEmailToAllRecipientAsync")]
        public async Task<IActionResult> SendCampaignEmailToAllRecipientAsync(CampaignEmailToAllRecipientInput input)
        {
            var result = await _campaignEmailSenderService.ExecuteSendCampaignToAllEmails(input);

            if (result is null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}



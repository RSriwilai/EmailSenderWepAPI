using EmailSender.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmailSender.WepAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("contactId")]
        public async Task<IActionResult> GetContactById(int contactId)
        {
            var contact = await _contactService.GetContactById(contactId);

            if(contact is null)
            {
                return BadRequest(contact);
            }

            return Ok(contact);

        }

        [HttpGet]
        [Route("GetContacts")]
        public async Task<IActionResult> GetCollectionsOfContactById()
        {
            var contact = await _contactService.GetCollectionsOfContact();

            if (contact is null)
            {
                return BadRequest(contact);
            }

            return Ok(contact);

        }
    }
}

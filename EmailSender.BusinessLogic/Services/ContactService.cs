using EmailSender.BusinessLogic.Interfaces;
using EmailSender.DataAccess.DatabaseModels;
using EmailSender.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.BusinessLogic.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepo;

        public ContactService(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        public async Task<Contact> GetContactById(int contactId)
        {
            var contact = await _contactRepo.GetContactById(contactId);
            return contact;
        }

        public async Task<List<Contact>> GetCollectionsOfContact()
        {
            var contacts = await _contactRepo.GetCollectionsOfContact();
            return contacts.ToList();
        }

    }
}

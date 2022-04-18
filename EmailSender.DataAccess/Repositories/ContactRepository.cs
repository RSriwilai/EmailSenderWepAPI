using EmailSender.DataAccess.DatabaseModels;
using EmailSender.DataAccess.DBContext;
using EmailSender.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.DataAccess.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly EmailSenderDBContext _db;

        public ContactRepository(EmailSenderDBContext db)
        {
            _db = db;
        }

        public async Task<List<Contact>> GetCollectionsOfContact()
        {
            var contacts = await _db.Contacts.ToListAsync();
            return contacts;
        }
    }
}

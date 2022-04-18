using EmailSender.DataAccess.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.DataAccess.Interfaces
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetCollectionsOfContact();
    }
}

using EmailSender.DataAccess.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSender.BusinessLogic.Interfaces
{
    public interface IContactService
    {
        Task<List<Contact>> GetCollectionsOfContact();
    }
}

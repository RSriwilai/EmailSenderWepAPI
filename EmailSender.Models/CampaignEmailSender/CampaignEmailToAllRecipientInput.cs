using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EmailSender.DataAccess.Enums;

namespace EmailSender.Models
{
    public class CampaignEmailToAllRecipientInput
    {
        [Required]
        [EnumDataType(typeof(Contacts))]
        public Contacts Contacts { get; set; }
    }
}

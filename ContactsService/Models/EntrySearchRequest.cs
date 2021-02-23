using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Service.Models
{
    public class EntrySearchRequest
    {
        public string SearchText { get; set; }
        public Guid PhoneBookId { get; set; }
    }
}

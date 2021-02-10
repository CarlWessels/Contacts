using Contacts.Common.DataObjects;
using Contacts.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Service.Interfaces
{
    public interface IEntryDataAdapter 
    {
        public Entry Insert(Entry entry, IDatabaseConnection connection);
        public List<Entry> Search(string searchText, Guid phoneBookId, IDatabaseConnection connection);
        public Entry Get(Guid entryId, IDatabaseConnection connection);
        public List<Entry> GetByPhoneBook(Guid phoneBookId, IDatabaseConnection connection);
    }
}

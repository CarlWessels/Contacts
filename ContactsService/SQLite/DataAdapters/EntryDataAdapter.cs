using Contacts.Common.DataObjects;
using Contacts.Common.Interfaces;
using Contacts.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Service.SQLite.DataAdapters
{
    public class EntryDataAdapter : IEntryDataAdapter
    {
        public Entry Get(Guid entryId, IDatabaseConnection connection)
        {
            return connection.Get<Entry>(entryId);
        }

        public Entry Insert(Entry entry, IDatabaseConnection connection)
        {
            var phoneBook = connection.Get<PhoneBook>(entry.PhoneBookId);
            if (phoneBook == null)
            {
                throw new Exception("Invalid PhoneBookId");
            }
            entry.Insert(connection);
            return entry;
        }

        public List<Entry> Search(string searchText, Guid phoneBookId, IDatabaseConnection connection)
        {
            var results = connection.Get<Entry>("SELECT * FROM [Entry] WHERE PhoneBookId = ? AND Name LIKE ?", phoneBookId, $"{searchText}%");
            return results;
        }


        public List<Entry> GetByPhoneBook(Guid phoneBookId, IDatabaseConnection connection)
        {
            return connection.Get<Entry>("SELECT * FROM [Entry] WHERE PhoneBookId = ?", phoneBookId);
        }
    }
}

using Contacts.Common.DataObjects;
using Contacts.Common.Interfaces;
using Contacts.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Service.SQLite.DataAdapters
{
    public class PhoneBookDataAdapter : IPhoneBookDataAdapter
    {
        public PhoneBook Get(Guid id, IDatabaseConnection connection)
        {
            return connection.Get<PhoneBook>(id);
        }

        public PhoneBook GetByName(string name, IDatabaseConnection connection)
        {
            var phoneBooks = connection.Get<PhoneBook>("SELECT * FROM [PhoneBook] WHERE Name = ?", name);
            if (phoneBooks == null || phoneBooks.Count != 1)
            {
                return null;
            }
            return phoneBooks.FirstOrDefault();

        }

        public List<PhoneBook> Search(string searchText, IDatabaseConnection connection)
        {
            var results = connection.Get<PhoneBook>("SELECT * FROM [PhoneBook] WHERE Name LIKE ?", $"{searchText}%");
            return results;
        }

        public PhoneBook Insert(string name, IDatabaseConnection connection)
        {
            var existing = GetByName(name, connection);
            if (existing != null)
            {
                throw new Exception("Duplicate phonebook found");
            }

            var phoneBook = new PhoneBook()
            {
                Name = name
            };
            phoneBook.Insert(connection);
            return phoneBook;
        }

        public List<PhoneBook> GetAll(IDatabaseConnection connection)
        {
            return connection.Get<PhoneBook>("SELECT * FROM [PhoneBook]");
        }
    }
}

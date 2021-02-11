using Contacts.Common.DataObjects;
using Contacts.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Service.Interfaces
{
    public interface IPhoneBookDataAdapter
    {
        public PhoneBook GetByName(string name, IDatabaseConnection connection);
        public PhoneBook Insert(string name, IDatabaseConnection connection);
        public PhoneBook Get(Guid id, IDatabaseConnection connection);
        public List<PhoneBook> Search(string searchText, IDatabaseConnection connection);
        public List<PhoneBook> GetAll(IDatabaseConnection connection);
        public PhoneBook GetDefaultPhoneBook(IDatabaseConnection connection);
    }
}

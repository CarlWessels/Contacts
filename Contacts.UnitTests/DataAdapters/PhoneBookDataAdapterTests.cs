using Contacts.Service.SQLite;
using Contacts.Service.SQLite.DataAdapters;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Xunit;

namespace Contacts.UnitTests.DataAdapters
{
    public class PhoneBookDataAdapterTests
    {
        [Fact]
        public void CanInsertAndRetrieve()
        {
            var connection = Helpers.SetupConnection();
            var dataAdapter = new PhoneBookDataAdapter();
            var phoneBook = dataAdapter.Insert("MyPhoneBook", connection);
            var retrieved = dataAdapter.GetByName("MyPhoneBook", connection);
            Assert.Equal(phoneBook.Id, retrieved.Id);
        }

        [Fact]
        public void CantInsertDuplicates()
        {
            var connection = Helpers.SetupConnection();
            var dataAdapter = new PhoneBookDataAdapter();
            dataAdapter.Insert("MyPhoneBook", connection);
            Assert.Throws<Exception>(() => dataAdapter.Insert("MyPhoneBook", connection));
        }


        [Theory]
        [InlineData("My", 3)]
        [InlineData("MyPhone", 2)]
        [InlineData("Phone", 1)]
        [InlineData("", 5)]
        public void Search(string searchText, int expected)
        {
            var connection = Helpers.SetupConnection();
            var dataAdapter = new PhoneBookDataAdapter();
            dataAdapter.Insert("MyPhoneBook", connection);
            dataAdapter.Insert("MyPhoneBook2", connection);
            dataAdapter.Insert("MyOtherBook", connection);
            dataAdapter.Insert("PhoneBook", connection);
            dataAdapter.Insert("XXX", connection);

            var results = dataAdapter.Search(searchText, connection);
            Assert.Equal(expected, results.Count);
        }

        [Fact]
        public void GetAll()
        {

            var connection = Helpers.SetupConnection();
            var dataAdapter = new PhoneBookDataAdapter();
            dataAdapter.Insert("MyPhoneBook", connection);
            dataAdapter.Insert("MyPhoneBook2", connection);
            dataAdapter.Insert("MyOtherBook", connection);
            dataAdapter.Insert("PhoneBook", connection);
            dataAdapter.Insert("XXX", connection);

            var results = dataAdapter.GetAll(connection);
            Assert.Equal(5, results.Count);
        }
    }
}

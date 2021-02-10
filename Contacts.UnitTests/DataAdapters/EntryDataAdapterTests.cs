using Contacts.Common.DataObjects;
using Contacts.Service.SQLite.DataAdapters;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Contacts.UnitTests.DataAdapters
{
    public class EntryDataAdapterTests
    {
        [Theory]
        [InlineData("My", 2)]
        [InlineData("MY", 2)]
        [InlineData("Some", 1)]
        [InlineData("Xxx", 0)]
        [InlineData("", 3)]
        public void Search(string searchText, int expected)
        {
            var connection = Helpers.SetupConnection();

            var entryDataAdapter = new EntryDataAdapter();
            var phoneBookDataAdapter = new PhoneBookDataAdapter();
            var phoneBook = phoneBookDataAdapter.Insert("MyPhoneBook", connection);

            var myEntry = entryDataAdapter.Insert(new Entry() { Name = "MyEntry", PhoneBookId = phoneBook.Id }, connection);
            var myOtherEntry = entryDataAdapter.Insert(new Entry() { Name = "MyOtherEntry", PhoneBookId = phoneBook.Id }, connection);
            var someEntry = entryDataAdapter.Insert(new Entry() { Name = "SomeEntry", PhoneBookId = phoneBook.Id }, connection);

            var searchResult = entryDataAdapter.Search(searchText, phoneBook.Id, connection);
            Assert.Equal(expected, searchResult.Count);
        }

        [Fact]
        public void CanInsertAndRetrieve()
        {
            var connection = Helpers.SetupConnection();
            var entryDataAdapter = new EntryDataAdapter();
            var phonebookDataAdapter = new PhoneBookDataAdapter();
            var phoneBook = phonebookDataAdapter.Insert("MyPhoneBook", connection);
            var entry = entryDataAdapter.Insert(new Entry() { Name = "MyEntry", PhoneNumber = "012 555 55555", PhoneBookId = phoneBook.Id }, connection);

            var retrieved = entryDataAdapter.Get(entry.Id, connection);
            Assert.NotNull(retrieved);
        }

        [Fact]
        public void CantInsertInvalidPhoneBookId()
        {
            var connection = Helpers.SetupConnection();
            var entryDataAdapter = new EntryDataAdapter();
            Assert.Throws<Exception>(() => entryDataAdapter.Insert(new Entry() { Name = "MyEntry", PhoneNumber = "012 555 55555", PhoneBookId = new Guid("82D890A1-0685-4C3C-A964-90EA3DA58859") }, connection));
        }
    }
}

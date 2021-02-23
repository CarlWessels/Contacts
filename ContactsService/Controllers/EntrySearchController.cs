using Contacts.Common.DataObjects;
using Contacts.Common.Interfaces;
using Contacts.Service.Interfaces;
using Contacts.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Contacts.Service.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class EntrySearchController : ControllerBase
    {
        private IDatabaseConnection _connection;
        private ILogger<EntryController> _logger;
        private IEntryDataAdapter _entryDA;
        private IPhoneBookDataAdapter _phoneBookDA;

        public EntrySearchController(IDatabaseConnection connection,
             ILogger<EntryController> logger,
             IEntryDataAdapter entryDA,
             IPhoneBookDataAdapter phoneBookDA)
        {
            _connection = connection;
            _logger = logger;
            _entryDA = entryDA;
            _phoneBookDA = phoneBookDA;
        }

        [HttpPost]
        public List<Entry> Search([FromBody] EntrySearchRequest entrySearch)
        {
            if (entrySearch.PhoneBookId == Guid.Empty)
            {
                entrySearch.PhoneBookId = _phoneBookDA.GetDefaultPhoneBook(_connection).Id;
            }
            var found = _entryDA.Search(entrySearch.SearchText, entrySearch.PhoneBookId, _connection);
            return found;
        }
    }
}

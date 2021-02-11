using Contacts.Common.DataObjects;
using Contacts.Common.Interfaces;
using Contacts.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Contacts.Service.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class EntryController : ControllerBase
    {
        private IDatabaseConnection _connection;
        private ILogger<EntryController> _logger;
        private IEntryDataAdapter _entryDA;
        private IPhoneBookDataAdapter _phoneBookDA;
        public EntryController(IDatabaseConnection connection,
             ILogger<EntryController> logger,
             IEntryDataAdapter entryDA,
             IPhoneBookDataAdapter phoneBookDA)
        {
            _connection = connection;
            _logger = logger;
            _entryDA = entryDA;
            _phoneBookDA = phoneBookDA;
        }

        [HttpGet]
        public Entry Get(Guid entryId)
        {
            _logger.LogDebug("Entry {entryID} retrieved", entryId);
            return _entryDA.Get(entryId, _connection);
        }

        [HttpPost]
        public Guid Insert([FromBody]Entry entry)
        {
            if (entry.PhoneBookId == Guid.Empty)
            {
                entry.PhoneBookId = _phoneBookDA.GetDefaultPhoneBook(_connection).Id;
            }
            _logger.LogDebug("Inserting new entry {entry}", entry);
            var inserted =_entryDA.Insert(entry, _connection);
            return inserted.Id;
        }

        [HttpPost]
        public List<Entry> Search([FromBody] EntrySearch entrySearch)
        {
            if (entrySearch.PhoneBookId == Guid.Empty)
            {
                entrySearch.PhoneBookId = _phoneBookDA.GetDefaultPhoneBook(_connection).Id;
            }
            var found = _entryDA.Search(entrySearch.SearchText, entrySearch.PhoneBookId, _connection);
            return found;
        }

    }

    public class EntrySearch
    {
        public string SearchText { get; set; }
        public Guid PhoneBookId { get; set; }
    }
}

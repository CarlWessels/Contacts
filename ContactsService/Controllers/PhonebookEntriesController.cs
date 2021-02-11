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
    [Route("[controller]")]
    public class PhonebookEntriesController : ControllerBase
    {
        private IDatabaseConnection _connection;
        private ILogger<PhonebookEntriesController> _logger;
        private IEntryDataAdapter _entryDA;
        private IPhoneBookDataAdapter _phoneBookDA;
        public PhonebookEntriesController(IDatabaseConnection connection,
             ILogger<PhonebookEntriesController> logger,
             IEntryDataAdapter entryDA,
             IPhoneBookDataAdapter phoneBookDA)
        {
            _connection = connection;
            _logger = logger;
            _entryDA = entryDA;
            _phoneBookDA = phoneBookDA;
        }

        [HttpGet]
        public List<Entry> Get(Guid id)
        {
            _logger.LogDebug("Retrieving all entries for phoneBook {phoneBookId}", id);
            if (id == Guid.Empty)
            {
                id = _phoneBookDA.GetDefaultPhoneBook(_connection).Id;
            }
            return _entryDA.GetByPhoneBook(id, _connection);
        }
    }
}

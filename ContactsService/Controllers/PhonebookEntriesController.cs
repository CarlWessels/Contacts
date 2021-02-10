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
        public PhonebookEntriesController(IDatabaseConnection connection,
             ILogger<PhonebookEntriesController> logger,
             IEntryDataAdapter entryDA)
        {
            _connection = connection;
            _logger = logger;
            _entryDA = entryDA;
        }

        [HttpGet]
        public List<Entry> Get(Guid id)
        {
            _logger.LogDebug("Retrieving all entries for phoneBook {phoneBookId}", id);
            return _entryDA.GetByPhoneBook(id, _connection);
        }
    }
}

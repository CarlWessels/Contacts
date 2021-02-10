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

    public class EntryController : ControllerBase
    {
        private IDatabaseConnection _connection;
        private ILogger<EntryController> _logger;
        private IEntryDataAdapter _entryDA;
        public EntryController(IDatabaseConnection connection,
             ILogger<EntryController> logger,
             IEntryDataAdapter entryDA)
        {
            _connection = connection;
            _logger = logger;
            _entryDA = entryDA;
        }

        [HttpGet]
        public IActionResult Get(Guid entryId)
        {
            _logger.LogDebug("Entry {entryID} retrieved", entryId);
            return (IActionResult)_entryDA.Get(entryId, _connection);
        }

        [HttpPost]
        public Guid Insert([FromBody] Entry entry)
        {
            _logger.LogDebug("Inserting new entry {entry}", entry);
            var inserted =_entryDA.Insert(entry, _connection);
            return inserted.Id;
        }
    }
}

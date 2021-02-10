using Contacts.Common.DataObjects;
using Contacts.Common.Interfaces;
using Contacts.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Contacts.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneBookController : ControllerBase
    {
        private IDatabaseConnection _connection;
        private ILogger<PhoneBookController> _logger;
        private IPhoneBookDataAdapter _phoneBookDA;
        public PhoneBookController(IDatabaseConnection connection,
             ILogger<PhoneBookController> logger,
             IPhoneBookDataAdapter phoneBookDA)
        {
            _connection = connection;
            _logger = logger;
            _phoneBookDA = phoneBookDA;

        }


        [HttpPost]
        public Guid Insert([FromBody] PhoneBook phoneBook)
        {
            var inserted = _phoneBookDA.Insert(phoneBook.Name, _connection);
            return inserted.Id;
        }

        [HttpGet]
        public PhoneBook Get(Guid id)
        {

            var phoneBook = _phoneBookDA.Get(id, _connection);
            return phoneBook;
        }

    }

    
}

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

    public class PhoneBooksController : ControllerBase
    {
        private IDatabaseConnection _connection;
        private ILogger<PhoneBooksController> _logger;
        private IPhoneBookDataAdapter _phoneBookDA;
        public PhoneBooksController(IDatabaseConnection connection,
             ILogger<PhoneBooksController> logger,
             IPhoneBookDataAdapter phoneBookDA)
        {
            _connection = connection;
            _logger = logger;
            _phoneBookDA = phoneBookDA;
        }

        [HttpGet]
        public List<PhoneBook> Get()
        {
            return _phoneBookDA.GetAll(_connection);
        }

        [HttpGet]
        public PhoneBook Get(Guid id)
        {

            var phoneBook = _phoneBookDA.Get(id, _connection);
            return phoneBook;
        }

        [HttpPost]
        public Guid Insert([FromBody] PhoneBook phoneBook)
        {
            var inserted = _phoneBookDA.Insert(phoneBook.Name, _connection);
            return inserted.Id;
        }

        
    }
}

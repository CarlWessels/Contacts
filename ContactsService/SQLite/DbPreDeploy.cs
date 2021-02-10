using Contacts.Common.DataObjects;
using Contacts.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Contacts.Service.SQLite
{
    public class DbPreDeploy : IDbPreDeploy
    {
        private ILogger<DbPreDeploy> _logger { get; set; }
        public DbPreDeploy(ILogger<DbPreDeploy> logger)
        {
            _logger = logger;
        }
        public void Execute(IDatabaseConnection aConnection)
        {
            if (aConnection.CanConnect())
            {
                aConnection.CreateTable<Entry>();
                aConnection.CreateTable<PhoneBook>();
            }

        }
    }
}

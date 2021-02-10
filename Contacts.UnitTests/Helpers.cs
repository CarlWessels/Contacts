using Contacts.Service.SQLite;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Contacts.UnitTests
{
    public static class Helpers
    {
        public static SQLiteDbConnection SetupConnection()
        {
            var dbFilename = Path.GetTempFileName();
            var connection = new SQLiteDbConnection(new LoggerFactory().CreateLogger<SQLiteDbConnection>(),
                new Service.Configuration.DatabaseConfiguration() { DatabasePath = dbFilename }
                );

            new DbPreDeploy(new LoggerFactory().CreateLogger<DbPreDeploy>()).Execute(connection);
            return connection;
        }
    }
}

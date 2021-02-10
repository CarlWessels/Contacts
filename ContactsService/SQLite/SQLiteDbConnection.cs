using Contacts.Common.DataObjects;
using Contacts.Common.Interfaces;
using Contacts.Service.Configuration;
using Microsoft.Extensions.Logging;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
namespace Contacts.Service.SQLite
{
    public class SQLiteDbConnection : IDatabaseConnection
    {
        private bool _hasTransaction { get; set; }

        private SQLiteConnection _connection;
        private ILogger<SQLiteDbConnection> _logger;
        private DatabaseConfiguration _configuration;
        public SQLiteDbConnection(
            ILogger<SQLiteDbConnection> logger,
            DatabaseConfiguration configuration)
        {

            _logger = logger;
            _configuration = configuration;
            Init();
        }

        public void Init()
        {
            if (string.IsNullOrEmpty(_configuration.DatabasePath))
            {
                throw new Exception("No databasePath specified");
            }

            _logger.LogDebug("Using database at {databasePath}", _configuration.DatabasePath);
            SQLiteConnectionString databaseOptions = null;
            var lDataBaseFolder = Path.GetDirectoryName(_configuration.DatabasePath);
            if (!string.IsNullOrEmpty(lDataBaseFolder) && !Directory.Exists(lDataBaseFolder))
            {
                Directory.CreateDirectory(lDataBaseFolder);
            }
            databaseOptions = new SQLiteConnectionString(_configuration.DatabasePath, true);
            _connection = new SQLiteConnection(databaseOptions);
        }

        public void Dispose()
        {
            CommitTransaction();
            _logger.LogDebug("Disposing database");
            _connection.Close();
        }


        public bool CanConnect()
        {
            try
            {
                Get<MetaInfo>("SELECT name, sql FROM sqlite_master");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to connect to database, confirm if the specified file is a database and the correct encryption key is being used");
                return false;
            }
            return true;
        }

        public void ListAllTables()
        {
            var tables = Get<MetaInfo>("SELECT name, sql FROM sqlite_master");
            foreach (var table in tables)
            {
                _logger.LogInformation("Table {tableName}", table.Name);
            }
        }

        public List<T> Get<T>(string query, params object[] parameters) where T : class, new()
        {
            try
            {
                _logger.LogDebug("Performing query : {query}", query);
                var ret = _connection.Query<T>(query, parameters);
                _logger.LogDebug("Finished query with {recordCount} results", ret.Count);
                return ret;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during get");
                throw (ex);
            }
        }

        public T Get<T>(Guid id) where T : class, new()
        {
            _logger.LogDebug("Retrieving {dataObjectType} with Id {id}", typeof(T).Name, id);

            var query = $"SELECT * FROM [{typeof(T).Name}] WHERE [Id] = ?";
            var ret = _connection.Query<T>(query, id);
            return ret.FirstOrDefault();
        }

        private object _insertLock = new object();
        public int Insert<T>(T aDataObject) where T : BaseDataObject
        {
            lock (_insertLock)
            {
                var count = _connection.Insert(aDataObject);
                _logger.LogDebug("Inserted record of type {recordType} with Id {id}", aDataObject.GetType().Name, aDataObject.Id);
                return count;
            }
        }

        public void Update<T>(T aDataObject) where T : BaseDataObject
        {
            _logger.LogDebug("Performing update : {@dataObject}", aDataObject);
            _connection.Update(aDataObject);
        }

        public void CreateTable<T>()
        {
            _logger.LogDebug("Creating table {tableName}", typeof(T).Name);
            _connection.CreateTable<T>();
        }

        public void BeginTransaction()
        {
            while (_hasTransaction)
            {
                Thread.Sleep(1000);
            }
            _hasTransaction = true;
            _connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (_hasTransaction)
            {
                _connection.Commit();
                _hasTransaction = false;
            }
        }

        public void RollbackTransaction()
        {
            if (_hasTransaction)
            {
                _connection.Rollback();
                _hasTransaction = false;
            }
        }

        public bool TableExists(string tableName)
        {
            var tables = Get<MetaInfo>("SELECT name, sql FROM sqlite_master");
            foreach (var lTable in tables)
            {
                if (lTable.Name == tableName)
                {
                    return true;
                }
            }
            return false;
        }
    }

    class MetaInfo
    {
        public string Name { get; set; }
        public string Sql { get; set; }
    }

}

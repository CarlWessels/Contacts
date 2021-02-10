using Contacts.Common.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.Common.Interfaces
{
    public interface IDatabaseConnection : IDisposable
    {
        int Insert<T>(T aDataObject) where T : BaseDataObject;
        void Update<T>(T aDataObject) where T : BaseDataObject;
        List<T> Get<T>(string aQuery, params object[] aParams) where T : class, new();
        T Get<T>(Guid id) where T : class, new();
        void CreateTable<T>();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        bool CanConnect();
        void ListAllTables();
        bool TableExists(string tableName);

    }
}

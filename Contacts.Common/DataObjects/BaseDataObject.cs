using Contacts.Common.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contacts.Common.DataObjects
{
    public abstract class BaseDataObject
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }

        public int Insert(IDatabaseConnection connection)
        {
            var errors = InsertValidationErrors(connection);
            if (errors == null)
            {
                var inserted = connection.Insert(this);
                return inserted;
            }
            else
            {
                var message = string.Join(Environment.NewLine, errors);
                throw new Exception($"Insert validation failed : {message}");
            }
        }

        public void Update(IDatabaseConnection connection)
        {
            connection.Update(this);
        }

        public virtual List<string> InsertValidationErrors(IDatabaseConnection connection)
        {
            return null;
        }
    }
}

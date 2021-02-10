using System;

namespace Contacts.Common.DataObjects
{
    public class Entry : BaseDataObject
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Guid PhoneBookId { get; set; }
    }
}

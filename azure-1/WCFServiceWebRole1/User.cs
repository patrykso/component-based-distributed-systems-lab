using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1
{
    public class User : ITableEntity
    {
        public User() { }
        public User(string login, string haslo)
        {
            this.PartitionKey = "users";
            this.RowKey = login;
            this.haslo = haslo;
        }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public string haslo { get; set; }
        public string sessionId { get; set; }
    }
}
using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFServiceWebRole1
{
    public class Session : ITableEntity
    {
        public Session(string login, string sessionId)
        {
            this.PartitionKey = "sessions";
            this.RowKey = sessionId;
            this.login = login;
        }

        public Session() { }

        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public ETag ETag { get; set; }
        public DateTimeOffset? Timestamp {  get; set; }
        public string login {  get; set; }
    }
}
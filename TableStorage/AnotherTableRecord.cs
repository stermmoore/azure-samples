using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage
{
    internal class AnotherTableRecord : ITableEntity
    {
        public string PartitionKey { get; set; } = nameof(AnotherTableRecord);
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string TypeName { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}

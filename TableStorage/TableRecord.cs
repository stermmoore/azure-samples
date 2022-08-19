using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableStorage
{
    internal class TableRecord : ITableEntity
    {
        public string PartitionKey { get; set; } = nameof(TableRecord);
        public string RowKey { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}

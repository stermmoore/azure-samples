using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB
{
    public class Reading
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string Location { get; set; }
        public DateTime ReadingTime { get; set; } = DateTime.UtcNow;
        public bool? IsVerified { get; set; }

    }
}

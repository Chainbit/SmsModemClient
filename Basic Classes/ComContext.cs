using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SmsModemClient
{
    class ComContext : DbContext
    {
        public ComContext() : base("DbConnection") { }

        public DbSet<SmsModemBlock> activeComs { get; set; }
    }
}

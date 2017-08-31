using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsModemClient
{
    public class CommandClass
    {
        public string Destination { get; set; }
        public string Command { get; set; }
        public string[] Pars { get; set; }
    }
}

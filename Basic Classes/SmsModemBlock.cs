using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using GsmComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsModemClient
{
    /// <summary>
    /// Класс блока модема, унаследованный от <see cref="GsmCommMain"/>
    /// </summary>
    public class SmsModemBlock : GsmCommMain
    {
        public string Operator { get; set; }
        public string ICCID { get; set; }
        //public string TelNumber { get; set; }
        public GsmPhone phone;

        public SmsModemBlock(string portName, int baudRate) : base(portName, baudRate)
        {
            phone = new GsmPhone(PortName, BaudRate, Timeout);
        }

        /// <summary>
        /// GOVNO!!!!!
        /// </summary>
        public void GetICCID()
        {
            //GsmPhone phone = new GsmPhone(PortName, BaudRate, Timeout);
            lock (phone)
            {
                string input = ((IProtocol)phone).ExecAndReceiveMultiple("AT+CCID");
                string text = TrimLineBreaks(input);
                ICCID = text;
            }
        }

        private string TrimLineBreaks(string input)
        {
            return input.Trim(new char[]
            {
        '\r',
        '\n'
            });
        }
    }
}

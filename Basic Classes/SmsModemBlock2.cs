using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using GsmComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SmsModemClient
{
    /// <summary>
    /// Класс блока модема, унаследованный от <see cref="GsmPhone"/>
    /// </summary>
    public class SmsModemBlock2 : GsmPhone
    {
        public string Operator { get; set; }
        public string ICCID { get; set; }
        //public string TelNumber { get; set; }
        public IProtocol protocol;
        public GsmPhone p;
        /// <summary>
        /// Создает новый объект класса <see cref="SmsModemBlock2"/>
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="timeout"></param>
        public SmsModemBlock2(string portName, int baudRate, int timeout = 500) : base(portName, baudRate, timeout)
        {
            protocol = this;
            p = this;
        }

        /// <summary>
        /// GOVNO!!!!!
        /// </summary>
        public void GetICCID()
        {
            //GsmPhone phone = new GsmPhone(PortName, BaudRate, Timeout);
            lock (this)
            {
                string input = ((IProtocol)this).ExecAndReceiveMultiple("AT+CCID");
                string text = TrimLineBreaks(input);
                ICCID = Regex.Match(text, "\"([^\"]*)\"").Groups[1].Value;
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

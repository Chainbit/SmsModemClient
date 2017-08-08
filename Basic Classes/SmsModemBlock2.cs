using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using GsmComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using GsmComm.PduConverter.SmartMessaging;

namespace SmsModemClient
{
    /// <summary>
    /// Класс блока модема, унаследованный от <see cref="GsmPhone"/>
    /// </summary>
    public class SmsModemBlock2 : GsmPhone
    {
        public string Operator { get; set; }
        public string ICCID { get; set; }
        public string TelNumber { get; set; }

        public IProtocol protocol;
        public GsmPhone p;

        /// <summary>
        /// Создает новый объект класса <see cref="SmsModemBlock2"/>
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="timeout"></param>
        public SmsModemBlock2(string portName, int baudRate, int timeout = 5000) : base(portName, baudRate, timeout)
        {
            protocol = this;
            p = this;

            MessageReceived += new MessageReceivedEventHandler(comm_MessageReceived);
        }

        /// <summary>
        /// Получает номер сим карты
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

        public void GetOperator()
        {
            ((IProtocol)this).ExecAndReceiveMultiple("AT+COPS=3,0");

            var info = this.GetCurrentOperator();
            if (info != null)
            {
                this.Operator = info.TheOperator;
            }
        }

        public void GetNumBeeline()
        {
            ((IProtocol)this).ExecAndReceiveMultiple("ATD*110*10#");


        }

        private void comm_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var mesagelist = ListMessages(PhoneMessageStatus.ReceivedUnread);

            foreach (ShortMessageFromPhone message in mesagelist)
            {
                
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

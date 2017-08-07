using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsModemClient
{
    public class RecievedSMS
    {
        public int Index { get; private set; }
        public SMSStatus Status { get; private set; }
        public string Sender { get; private set; }
        public DateTime Date { get; private set; }
        public string Message { get; private set; }
        public string DecodedMessage { get; set; }

        /// <summary>
        /// Конструктор класса полученного сообщения
        /// </summary>
        /// <param name="index">Порядковый номер смс</param>
        /// <param name="status">Статус (“REC UNREAD” или “REC READ”)</param>
        /// <param name="sender">отправитель</param>
        /// <param name="SCTS">Service Center Time Stamp</param>
        /// <param name="message">Текст сообщения</param>
        public RecievedSMS(int index, string status, string sender, string SCTS, string message)
        {
            Index = index;
            Sender = sender;

            if (status.ToLower().Contains("unread"))
            {
                Status = SMSStatus.Unread;
            }
            else
            {
                Status = SMSStatus.Read;
            }
                        
            //парсим дату
            DateTime tempdate;
            if (!DateTime.TryParse(SCTS, out tempdate))
            {
                Date = DateTime.Now;
            }
            else
            {
                Date = tempdate;
            }

            Message = message;            
        }
    }

    public enum SMSStatus
    {
        Read,
        Unread
    }
}

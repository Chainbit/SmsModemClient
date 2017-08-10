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
using System.Threading;
using System.Windows.Forms;

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

        public static CancellationTokenSource cts = new CancellationTokenSource();
        private CancellationToken ct = cts.Token;

        public event Action NumberRecieved;

        /// <summary>
        /// Создает новый объект класса <see cref="SmsModemBlock2"/>
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="baudRate"></param>
        /// <param name="timeout"></param>
        public SmsModemBlock2(string portName, int baudRate, int timeout = 5000) : base(portName, baudRate, timeout)
        {
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

        /// <summary>
        /// Получает оператора
        /// </summary>
        public void GetOperator()
        {
            ((IProtocol)this).ExecAndReceiveMultiple("AT+COPS=3,0");

            var info = this.GetCurrentOperator();
            if (info != null)
            {
                this.Operator = info.TheOperator;
                //Provider op = (Provider)Enum.Parse(typeof(Provider), Operator);
            }
        }

        /// <summary>
        /// Запрашивет номер у Билайна
        /// </summary>
        public async Task GetNumBeeline()
        {            
            await WaitSMS();
        }

        /// <summary>
        /// Находит сообщение от оператора
        /// </summary>
        /// <param name="messagelist">список сообщений</param>
        private bool HasOperatorSms()
        {
            var messagelist = ListMessages(PhoneMessageStatus.All);
            foreach (ShortMessageFromPhone message in messagelist)
            {
                var pdu = new SmsDeliverPdu(message.Data, true, -1);
                var origin = pdu.OriginatingAddress;
                var txt = pdu.UserDataText;

                bool isNumber = txt.ToLower().Contains("ваш номер");
                bool isOperator = origin.ToLower().Contains(Operator.ToLower());
                if (isOperator && isNumber)
                {
                    var tel = txt.Substring(txt.IndexOf('9'));
                    tel = tel.Replace(".", "");
                    TelNumber = "+7" + tel;
                    //NumberRecieved();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Задача ждать смс от оператора
        /// </summary>
        /// <returns></returns>
        private Task WaitSMS()
        {
            Task t = new Task(() =>
            {
                var isCon = IsConnected();
                var hasSms = HasOperatorSms();
                if (isCon && !hasSms)
                {
                    int i = 0;
                    string resp = ((IProtocol)this).ExecAndReceiveMultiple("ATD*110*10#");
                    do
                    {
                        Thread.Sleep(5000);
                        i++;
                    } while (!HasOperatorSms() && i < 4);
                    if (i >= 4) 
                    {
                        TelNumber = "Ошибка!";
                    }
                    return;
                }
                else
                {
                    return;
                }
            }, ct);
            t.Start();
            return t;
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

    /// <summary>
    /// Оператор Сотовой Связи
    /// </summary>
    public enum Provider
    {
        Unknown,
        Beeline,
        MTS,
        MegaFon,
        Tele2
    }
}

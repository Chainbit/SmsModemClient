using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using GsmComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;

namespace SmsModemClient
{
    /// <summary>
    /// Класс блока модема, унаследованный от <see cref="GsmCommMain"/>
    /// </summary>
    public class SmsModemBlock : GsmCommMain
    {
        public string Port { get; private set; }
        public string Operator { get; set; }
        public string Id { get; set; }
        public string TelNumber { get; set; }
        public Signal SignalLevel { get; set; }
        public string MacAddress { get; set; }

        public event EventHandler NumberReceived;
        public static CancellationTokenSource cts = new CancellationTokenSource();

        private CancellationToken ct = cts.Token;

        public SmsModemBlock() : base()
        {

        }

        public SmsModemBlock(string portName, int baudRate) : base(portName, baudRate)
        {
            Port = PortName;
            Operator = Id = TelNumber = "Загрузка...";
        }

        /// <summary>
        /// Получает номер сим карты
        /// </summary>
        public void GetICCID()
        {
            //GsmPhone phone = new GsmPhone(PortName, BaudRate, Timeout);
            lock (this)
            {
                try
                {
                    string input = GetProtocol().ExecAndReceiveMultiple("AT+CCID");
                    string text = TrimLineBreaks(input);
                    Id = Regex.Match(text, "\"([^\"]*)\"").Groups[1].Value;
                }
                catch (Exception ex)
                {
                    Id = "Ошибка!";
                    var x = ex.Message;
                }
                finally
                {
                    ReleaseProtocol();
                }
            }
        }

        // <summary>
        /// Получает оператора
        /// </summary>
        public void GetOperator()
        {
            lock (this)
            {
                try
                {
                    GetProtocol().ExecAndReceiveMultiple("AT+COPS=3,0");
                    var info = this.GetCurrentOperator();
                    if (info != null)
                    {
                        this.Operator = info.TheOperator;
                        //Provider op = (Provider)Enum.Parse(typeof(Provider), Operator);
                    }
                }
                catch (Exception ex)
                {
                    Operator = "Ошибка!";
                    var x = ex.Message;
                }
                finally
                {
                    ReleaseProtocol();
                }
            }         
        }

        /// <summary>
        /// Получает текщий уровень сигнала
        /// </summary>
        public void GetSignalStrength()
        {
            int signalStrength;
            lock (this)
            {
                signalStrength = GetSignalQuality().SignalStrength;
            }
            if (signalStrength < 10)
            {
                SignalLevel = Signal.Poor;
            }
            else if (signalStrength >= 10 && signalStrength <= 14)
            {
                SignalLevel = Signal.Ok;
            }
            else if (signalStrength >= 15 && signalStrength <= 19)
            {
                SignalLevel = Signal.Good;
            }
            else if (signalStrength > 19)
            {
                SignalLevel = Signal.Excellent;
            }
            else
            {
                SignalLevel = Signal.None;
            }
        }

        /// <summary>
        /// Запрашивет номер у Билайна
        /// </summary>
        public async void GetNumBeeline()
        {
            await WaitSMSBeeline();
        }

        /// <summary>
        /// Запрашивет номер у Билайна
        /// </summary>
        public async void GetNumMTS()
        {
            await WaitSMSMTS();
        }

        /// <summary>
        /// Получает номер от Мегафона
        /// </summary>
        public void GetNumMegafon()
        {
            // тут вроде все просто
            lock (this)
            {
                try
                {
                    var nums = this.GetSubscriberNumbers();
                    TelNumber = nums.First().Number;
                    // вызываем событие, чтобы симка добавилась в БД
                    NumberReceived(this, new EventArgs());
                }
                catch (Exception ex)
                {                    
                }
            }
        }

        /// <summary>
        /// Находит сообщение от оператора
        /// </summary>
        /// <param name="messagelist">список сообщений</param>
        private bool HasBeelineSms()
        {
            var messagelist = ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

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
                    NumberReceived(this, new EventArgs());
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Находит сообщение от оператора
        /// </summary>
        /// <param name="messagelist">список сообщений</param>
        private bool HasMTSSms()
        {
            var messagelist = ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

            foreach (ShortMessageFromPhone message in messagelist)
            {
                var pdu = new SmsDeliverPdu(message.Data, true, -1);
                var origin = pdu.OriginatingAddress;
                var txt = pdu.UserDataText;

                bool isNumber = txt.ToLower().Contains("ваш номер");
                bool isOperator = origin.ToLower().Contains("111");
                if (isOperator && isNumber)
                {
                    var tel = txt.Substring(txt.IndexOf('9'));
                    tel = tel.Replace(".", "");
                    TelNumber = "+7" + tel;
                    NumberReceived(this, new EventArgs());
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Задача ждать смс от Билайна
        /// </summary>
        /// <returns></returns>
        private Task WaitSMSBeeline()
        {
            Task t = new Task(() =>
            {
                var isCon = IsConnected();
                var hasSms = HasBeelineSms();
                if (isCon && !hasSms)
                {
                    int i = 0;                    
                    string resp = GetProtocol().ExecAndReceiveMultiple("ATD*110*10#");
                    
                    do
                    {
                        Thread.Sleep(5000);
                        i++;
                    } while (!HasBeelineSms() && i < 4);
                    if (i >= 4)
                    {
                        TelNumber = "Ошибка!";
                    }
                    ReleaseProtocol();
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

        /// <summary>
        /// Задача ждать смс от МТС
        /// </summary>
        /// <returns></returns>
        private Task WaitSMSMTS()
        {
            Task t = new Task(() =>
            {
                var isCon = IsConnected();
                var hasSms = HasMTSSms();
                if (isCon && !hasSms)
                {
                    int i = 0;                    
                    string resp = GetProtocol().ExecAndReceiveMultiple("ATD*111*0887#");                    
                    do
                    {
                        Thread.Sleep(5000);
                        i++;
                    } while (!HasMTSSms() && i < 4);
                    if (i >= 4)
                    {
                        TelNumber = "Ошибка!";
                    }
                    ReleaseProtocol();
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

        /// <summary>
        /// Очищает входящие сообщения
        /// </summary>
        public void ClearInbox()
        {
            DeleteMessages(DeleteScope.All, PhoneStorageType.Sim);
        }

        private string TrimLineBreaks(string input)
        {
            return input.Trim(new char[]
            {
        '\r',
        '\n'
            });
        }

        /// <summary>
        /// Уровень сигнала
        /// </summary>
        public enum Signal
        {
            /// <summary>
            /// Нет сигнала
            /// </summary>
            None,
            /// <summary>
            /// Говно
            /// </summary>
            Poor,
            /// <summary>
            /// Сойдет
            /// </summary>
            Ok,
            /// <summary>
            /// Хорошо
            /// </summary>
            Good,
            /// <summary>
            /// Превосходно
            /// </summary>
            Excellent
        }
    }
}

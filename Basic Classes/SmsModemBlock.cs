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
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmsModemClient
{
    /// <summary>
    /// Класс блока модема, унаследованный от <see cref="GsmCommMain"/>
    /// </summary>
    [Serializable]
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class SmsModemBlock : GsmCommMain
    {
        [JsonProperty]
        public string Id { get; set; }
        public string Operator { get; set; }
        [JsonProperty]
        public string TelNumber { get; set; }
        [NotMapped]
        public Signal SignalLevel { get; set; }
        [NotMapped]
        public string MacAddress { get; set; }

        /// <summary>
        /// Максимально входящих сообщений
        /// </summary>
        [NotMapped]
        private int MaxMessages { get; set; }
        /// <summary>
        /// Количество входящих сообщений
        /// </summary>
        [NotMapped]
        private int UsedMessages { get; set; }


        public event EventHandler NumberReceived;
        public static CancellationTokenSource cts = new CancellationTokenSource();

        private CancellationToken ct = cts.Token;

        [NotMapped]
        private bool isWaiting = false;

        public SmsModemBlock() : base()
        {

        }

        public SmsModemBlock(string portName, int baudRate) : base(portName, baudRate)
        {
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

        /// <summary>
        /// Получает оператора сим карты
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
        private bool HasBeelineSms()
        {
            CheckMemoryStatus();

            for (int i = 0; i < 5; i++)
            {
                var messagelist = ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

                foreach (ShortMessageFromPhone message in messagelist)
                {
                    var pdu = new SmsDeliverPdu(message.Data, true, -1);
                    var origin = pdu.OriginatingAddress;
                    var txt = pdu.UserDataText;

                    bool isNumber = txt.ToLower().Contains("ваш номер");
                    bool isOperator = origin.ToLower().Contains("beeline");
                    if (isOperator && isNumber)
                    {
                        var tel = txt.Substring(txt.IndexOf('9'));
                        tel = tel.Replace(".", "");
                        TelNumber = "+7" + tel;
                        NumberReceived(this, new EventArgs());
                        return true;
                    }
                }
                if (MaxMessages == UsedMessages)
                {
                    ClearInbox();
                }
            }
            return false;
        }

        /// <summary>
        /// Находит сообщение от оператора
        /// </summary>
        private bool HasMTSSms()
        {
            CheckMemoryStatus();

            for (int i = 0; i < 5; i++)
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
                if (MaxMessages == UsedMessages)
                {
                    ClearInbox();
                }
            }            
            return false;
        }

        /// <summary>
        /// Проверяет наличие смс по номеру
        /// </summary>
        /// <param name="search">Номер или имя отправителя</param>
        /// <returns></returns>
        private string CheckSMSFromNumber(string search)
        {
            CheckMemoryStatus();

            for (int i = 0; i < 5; i++)
            {
                //список собщений
                var messagelist = ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

                foreach (ShortMessageFromPhone message in messagelist)
                {
                    //сырая хуета
                    var pdu = new SmsDeliverPdu(message.Data, true, -1);

                    var origin = pdu.OriginatingAddress;
                    var txt = pdu.UserDataText;

                    bool isFound = origin.ToLower().Contains(search.ToLower());
                    if (isFound)
                    {
                        return txt;
                    }
                }
                if (MaxMessages == UsedMessages)
                {
                    ClearInbox();
                }
            }
            return null;
        }

        /// <summary>
        /// Проверяет наличие смс по содержимому
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string CheckSMSWithContent(string text)
        {
            CheckMemoryStatus();

            for (int i = 0; i < 5; i++)
            {
                //список собщений
                var messagelist = ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

                foreach (ShortMessageFromPhone message in messagelist)
                {
                    //сырая хуета
                    var pdu = new SmsDeliverPdu(message.Data, true, -1);

                    var origin = pdu.OriginatingAddress;
                    var txt = pdu.UserDataText;

                    bool isFound = txt.ToLower().Contains(text.ToLower());
                    if (isFound)
                    {
                        return txt;
                    }
                }
                if (MaxMessages == UsedMessages)
                {
                    ClearInbox();
                }
            }
            return null;
        }

        #region TASKS

        /// <summary>
        /// Задача ждать смс от Билайна
        /// </summary>
        /// <returns></returns>
        private Task WaitSMSBeeline()
        {
            Task t = new Task(() =>
            {
                try
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
                }
                catch (Exception ex)
                {
                    TelNumber = "Ошибка!";
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
                try
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
                }
                catch (Exception ex)
                {
                    TelNumber = "Ошибка!";
                }                
            }, ct);
            t.Start();
            return t;
        }

        /// <summary>
        /// Задача ждать смс
        /// </summary>
        /// <param name="origin">От кого ждать смс</param>
        /// <returns></returns>
        public Task<string> WaitSMSFromNumber(string origin)
        {
            return Task.Factory.StartNew<string>(() =>
            {
                if (IsConnected())
                {
                    for (int i = 0; i < 12; i++)
                    {
                        string sms = CheckSMSFromNumber(origin);
                        if (sms!=null)
                        {
                            return sms;
                        }
                        Thread.Sleep(5000);
                    }
                }
                else
                {
                    return "ERROR";
                }
                return null;
            });
        }
        
        /// <summary>
        /// Задача ждать смс
        /// </summary>
        /// <param name="text">От кого ждать смс</param>
        /// <returns></returns>
        public Task<string> WaitSMSWithContent(string text)
        {
            return Task.Factory.StartNew<string>(() =>
            {
                if (IsConnected())
                {
                    for (int i = 0; i < 12; i++)
                    {
                        string sms = CheckSMSWithContent(text);
                        if (sms!=null)
                        {
                            return sms;
                        }
                        Thread.Sleep(5000);
                    }
                }
                else
                {
                    return "ERROR";
                }
                return null;
            });
        }

        /// <summary>
        /// Ждет любую входящую смс
        /// </summary>
        /// <returns></returns>
        public Task<string> WaitAnySMS()
        {
            return Task.Factory.StartNew<string>(() =>
            {
                //даем время
                for (int i = 0; i < 100; i++)
                {
                    //список собщений
                    var messagelist = ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

                    var messageString = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    if (messagelist.Length>0)
                    {
                        foreach (ShortMessageFromPhone message in messagelist)
                        {
                            //сырая хуета
                            var pdu = new SmsDeliverPdu(message.Data, true, -1);

                            var origin = pdu.OriginatingAddress;
                            var txt = pdu.UserDataText;
                            sb.AppendLine(string.Format("{0}: {1}", origin, txt));
                        }
                    }
                    
                    Thread.Sleep(3000);
                }
                return "Время вышло!";
            });
        }
        #endregion


        public void GetLastSMS()
        {
            do
            {
                CheckMemoryStatus();
                ClearInbox();
            }
            while (UsedMessages>0);
            
        }

        /// <summary>
        /// Очищает входящие сообщения
        /// </summary>
        public void ClearInbox()
        {
            DeleteMessages(DeleteScope.All, PhoneStorageType.Sim);
        }

        /// <summary>
        /// Возвращает смс от конкретного отправителя
        /// </summary>
        /// <param name="senderName">Имя или номер отправителя</param>
        /// <returns></returns>
        public string ReturnSMS(string senderName)
        {
            var readStorage = new MessageMemoryStatus().ReadStorage;
            var maxMessages = readStorage.Total;
            var usedMessages = readStorage.Used;

            for (int i = 0; i < 5; i++)
            {
                var messagelist = ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

                foreach (ShortMessageFromPhone message in messagelist)
                {
                    var pdu = new SmsDeliverPdu(message.Data, true, -1);
                    var origin = pdu.OriginatingAddress;
                    var txt = pdu.UserDataText;

                    bool isNeeded = origin.ToLower().Contains(senderName.ToLower());
                    if (isNeeded)
                    {
                        return txt;
                    }
                }
                if (maxMessages == usedMessages)
                {
                    ClearInbox();
                }
            }
            return null;
        }

        /// <summary>
        /// Проверяет память входящих сообщений
        /// </summary>
        private void CheckMemoryStatus()
        {
            var rs = GetMessageStorages().ReadStorages;

            var SMstorage = GetMessageMemoryStatus(rs[0]); // память сим-карты
            //var BMstorage = GetMessageMemoryStatus(rs[1]); // память устройства
            //var SRstorage = GetMessageMemoryStatus(rs[2]); // хуй знает что такое

            var readStorage = SMstorage;
            MaxMessages = readStorage.Total;
            UsedMessages = readStorage.Used;
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

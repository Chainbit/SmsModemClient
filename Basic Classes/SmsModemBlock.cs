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
using GsmComm.PduConverter.SmartMessaging;
using System.Windows.Forms;

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
        [JsonProperty]
        public string SimBankId { get; set; }

        [JsonProperty]
        [NotMapped]
        public decimal Balance { get; set; }

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
        [NotMapped]
        public int CurrentCommandID { get; set; }


        public event EventHandler NumberReceived;
        public EventHandler SmsReceived;

        public static CancellationTokenSource cts = new CancellationTokenSource();

        private CancellationToken ct = cts.Token;

        [NotMapped]
        private System.Windows.Forms.Timer timer;
        [NotMapped]
        private bool isWaiting = false;
        [NotMapped]
        private string Search;
        [NotMapped]
        public bool isReady = false;

        public SmsModemBlock() : base()
        {

        }

        public SmsModemBlock(string portName, int baudRate) : base(portName, baudRate)
        {
            Operator = Id = TelNumber = "Загрузка...";

            // создаем таймер 
            timer = new System.Windows.Forms.Timer();
            timer.Interval = (10 * 60 * 1000);
            timer.Tick += Timer_Tick;
        }

        public void OnReady()
        {
            isReady = true;
            EnableMessageNotifications();
            timer.Start();
        }

        /// <summary>
        /// Регулярная очистка инбокса
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!isWaiting)
            {
                ClearInbox();
            }
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
                    SetWaiting(false); // выключаем ожидаение
                }
                catch (Exception ex)
                {                    
                }
            }
        }

        /// <summary>
        /// Получить баланс
        /// </summary>
        public void GetBalance()
        {
            switch (this.Operator.ToLower())
            {
                case "beeline":
                    GetBalanceBeeline();
                    break;
                case "megafon":
                    //query = "#100#";
                    return;
                    break;
                case "mts rus":
                    //query = "#102#";
                    return;
                    break;
                default:
                    return;
                    break;
            }            
        }

        /// <summary>
        /// Получить баланс на Билайне
        /// </summary>
        public void GetBalanceBeeline()
        {
            lock (this)
            {
                try
                {
                    IProtocol protocol = GetProtocol();
                    string gottenString = protocol.ExecAndReceiveMultiple("at+cusd=1,#102#,15");
                    string resp = string.Empty;
                    int i = 0;
                    if (string.IsNullOrEmpty(resp))
                    {
                        do
                        {
                            protocol.Receive(out gottenString);
                            ++i;
                        } while (!(i >= 5 || gottenString.Contains("+CUSD")));
                    }
                    if (gottenString.Contains("Vash balans") && !gottenString.Contains("aktiviruyetsya"))
                    {
                        int Pos1 = gottenString.IndexOf("Vash balans") + ("Vash balans").Length;
                        int Pos2 = gottenString.IndexOf(" r");
                        string temp = gottenString.Substring(Pos1, Pos2 - Pos1).Trim();
                        temp = temp.Replace('.', ',');
                        Balance = decimal.Parse(temp);
                    }
                    else
                    {
                        //ERROR
                    }
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                }
                finally
                {
                    ReleaseProtocol();
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
                        SetWaiting(false); // выключаем ожидаение
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
                        SetWaiting(false); // выключаем ожидаение
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
                    var timestamp = pdu.SCTimestamp.ToDateTime();

                    bool isNew = timestamp + TimeSpan.FromMinutes(5) > DateTime.Now;
                    bool isFound = origin.ToLower().Contains(search.ToLower());
                    if (isFound && isNew)
                    {
                        return txt;
                    }
                }
                if (MaxMessages == UsedMessages)
                {
                    ClearInbox();
                }
                Thread.Sleep(5000);
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

        public Task WaitSMSFromNumber(string origin, int cmdId)
        {
            CurrentCommandID = cmdId;
            return Task.Factory.StartNew(() =>
            {
                if (IsConnected())
                {
                    //for (int i = 0; i < 60; i++)
                    //{
                        string sms = CheckSMSFromNumber(origin);
                        if (sms!=null)
                        {
                            SmsReceived(this, new MessageArgs() { messageText = sms });
                        }
                        else
                        {
                            Search = origin;
                            MessageReceived += new MessageReceivedEventHandler(SmsModemBlock_MessageReceived);
                        }
                    //    Thread.Sleep(1000);
                    //}
                }
                else
                {
                    //return "ERROR";
                }
            });
        }


        private void SmsModemBlock_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            string sms = CheckSMSFromNumber(Search);
            if (sms != null)
            {
                SmsReceived(this, new MessageArgs() { messageText = sms });
            }
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
            return Task.Run(() =>
            {
                //даем время
                for (int i = 0; i < 100 && isWaiting==true; i++)
                {
                    //список собщений
                    var messagelist = ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

                    #region oldShit
                    //var messageString = string.Empty;
                    //StringBuilder sb = new StringBuilder();
                    //if (messagelist.Length > 0)
                    //{
                    //    // на случай, если длинное сообщение
                    //    IList<SmsPdu> longMsg = new List<SmsPdu>();

                    //    foreach (ShortMessageFromPhone message in messagelist)
                    //    {
                    //        //сырая хуета
                    //        var pdu = new SmsDeliverPdu(message.Data, true, -1);

                    //        var origin = pdu.OriginatingAddress;
                    //        var txt = pdu.UserDataText;

                    //        bool isMultiPart = SmartMessageDecoder.IsPartOfConcatMessage(pdu);

                    //        // если у сообщения есть датахедер, то скорее всего оно длинное
                    //        if (pdu.UserDataHeaderPresent && isMultiPart)
                    //        {
                    //            if (longMsg.Count == 0 || SmartMessageDecoder.ArePartOfSameMessage(longMsg.Last(), pdu))
                    //            {
                    //                longMsg.Add(pdu);
                    //            }
                    //            if (SmartMessageDecoder.AreAllConcatPartsPresent(longMsg)) // is Complete
                    //            {
                    //                txt = SmartMessageDecoder.CombineConcatMessageText(longMsg);
                    //            }
                    //            else
                    //            {
                    //                continue;
                    //            }
                    //        }
                    //        sb.AppendFormat("{0}: {1}", origin, txt);
                    //    }
                    //    return sb.ToString();
                    //}
                    #endregion

                    Thread.Sleep(3000);
                }
                return "Время вышло!";
            });
        }

        #endregion
        
        /// <summary>
        /// Очищает входящие и ждет СМС
        /// </summary>
        public async Task GetLastSMS(int cmdId)
        {
            CurrentCommandID = cmdId;
            do
            {
                CheckMemoryStatus();
                ClearInbox();
            }
            while (UsedMessages>0);
            await WaitAnySMS();
        }

        /// <summary>
        /// Очищает входящие сообщения
        /// </summary>
        public void ClearInbox()
        {
            try
            {
                DeleteMessages(DeleteScope.All, PhoneStorageType.Sim);
            }
            catch (Exception e)
            {
                GC.Collect();
            }
        }

        /// <summary>
        /// Задать режим ожидания
        /// </summary>
        /// <param name="state"><see langword="true"/> если ожидает смс, <see langword="false"/> если нет</param>
        public void SetWaiting(bool state)
        {
            isWaiting = state;
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

        /// <summary>
        /// Обрезает переносы строк в сообщениях из порта
        /// </summary>
        /// <param name="input">Входящая строка</param>
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

        ~SmsModemBlock()
        {
            try
            {
                this.DisableMessageNotifications();
            }
            catch (Exception) {}
            try
            {
                timer.Dispose();
            }
            catch (Exception){ }
        }
    }

    public class MessageArgs : EventArgs
    {
        public string messageText;
    }
}

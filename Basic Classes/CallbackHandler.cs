using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections.Specialized;
using Microsoft.AspNet.SignalR.Client;
using System.Windows.Forms;
using Newtonsoft.Json;
using GsmComm.PduConverter;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter.SmartMessaging;

namespace SmsModemClient
{
    public class CallbackHandler
    {
        IHubProxy _hub;
        HubConnection connection;
        private ComPortManager Manager = null;
        private string previousCommand = string.Empty;
        //private List<SmsModemBlock> Subscriptions = new List<SmsModemBlock>();

        public CallbackHandler(string ServerAddress)
        {
            InitializeConnection(ServerAddress);
            Subscribe();
            Invoke();
            GetInfo();
        }

        public CallbackHandler(string ServerAddress, ComPortManager mgr)
        {
            Manager = mgr;
            InitializeConnection(ServerAddress);
            Subscribe();
            Invoke();
            GetInfo();
        }

        private void DisplayInfo(string id, string name)
        {
            MessageBox.Show(string.Format("Your ID: {0} \r\n Your Name: {1}", id, name));
        }

        /// <summary>
        /// Подключается к серверу
        /// </summary>
        /// <param name="ServerAddress">IP адрес сервера</param>
        private void InitializeConnection(string ServerAddress)
        {
            connection = new HubConnection(ServerAddress);
            //ВОТ ЗДЕСЬ БЛЯТЬ ОЧЕНЬ ВНИМАТЕЛЬНО НУЖНО НАЗВАНИЕ ХАБА НАПИСАТЬ КАК НАДО
            _hub = connection.CreateHubProxy("CommandHub");

            connection.Start().Wait();
            connection.Closed += Manager.MF.Connection_Closed;
            connection.Reconnected += Manager.MF.Connection_Reconnected;
        }

        /// <summary>
        /// Подписывается на основные события на сервере
        /// </summary>
        private void Subscribe()
        {
            _hub.On<string, string>("onConnected", (id, commName) => DisplayInfo(id, commName));
            _hub.On("broadcast", x => MessageBox.Show(x));
            _hub.On("CommandArrived", x => ParseCommand(x));
        }

        /// <summary>
        /// Вызывает методы на сервере
        /// </summary>
        private void Invoke()
        {
            string line = "Hello, Motherfucker!";
            _hub.Invoke("DetermineLength", line).Wait();
            _hub.Invoke("Connect", Manager.MacAddress).Wait();
        }

        /// <summary>
        /// Парсит входящую команду
        /// </summary>
        /// <param name="cmd">Строка с параметрами команды</param>
        private void ParseCommand(string cmd)
        {
            if (cmd==previousCommand)
            {
                return;
            }

            previousCommand = cmd;

            CommandClass command = JsonConvert.DeserializeObject<CommandClass>(cmd);

            //если команда адресована кому-то другому
            if (command.Destination.ToLower() != "all" && !Manager.activeComs.Exists(comm => comm.Id == command.Destination)) 
            {
                Console.WriteLine();
                //выходим
                return;
            }
            ExecuteCommand(command);
        }

        /// <summary>
        /// Выполняет команду с параметрами
        /// </summary>
        /// <param name="cmd">Команда</param>
        private void ExecuteCommand(CommandClass cmd)
        {
            //ServerCommand command = (ServerCommand)Enum.Parse(typeof(ServerCommand), cmd.Command);

            switch (cmd.Command)
            {
                case "GetInfo":
                    GetInfo();
                    break;
                case "WaitSms":
                    WaitSMS(cmd.Destination, cmd.Pars, cmd.Id);
                    break;
                case "SimCardMalfunction":
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Отправляет данные о всех активных номерах
        /// </summary>
        private void GetInfo()
        {
            string data = Manager.SendManagerInfo();
            _hub.Invoke("ManagerInfo", data);
        }

        /// <summary>
        /// Отключает нерабочую сим карту
        /// </summary>
        /// <param name="ICCID">Номер карты</param>
        private void TurnOffSim(string ICCID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Включает ожидание смс от определенного отправителя
        /// </summary>
        /// <param name="id">Номер сим-карты</param>
        /// <param name="pars">Строка, содержащая параметры</param>
        private async void WaitSMS(string id, string[] pars, int cmdId)
        {
            var receiver = Manager.activeComs.First(x => x.Id == id);
            var type = pars[0];
            string search;
            try
            {
                search = pars[1];
            }
            catch (Exception)
            {
                search = "";
            }            

            string sms = "";

            receiver.SetWaiting(true);

            switch (type)
            {
                case "SearchByNumber":
                    receiver.SmsReceived += new EventHandler(Receiver_SmsFromNumberReceived);
                    await receiver.WaitSMSFromNumber(search, cmdId);
                    break;
                case "SearchByContent":
                    sms = await receiver.WaitSMSWithContent(search);
                    break;
                case "ReceiveLast":
                    receiver.MessageReceived += new MessageReceivedEventHandler(Receiver_MessageReceived);
                    await receiver.GetLastSMS(cmdId);
                    //Subscriptions.Add(receiver);
                    break;
                default:
                    receiver.SetWaiting(false);
                    break;
            }
            //if (sms!=null)
            //{
            //    _hub.Invoke("SmsReceived", sms, cmdId).Wait();
            //    //_hub.Invoke("DetermineLength", sms).Wait();
            //    receiver.SetWaiting(false);
            //}
        }

        private void Receiver_SmsFromNumberReceived(object sender, EventArgs e)
        {
            var comm = sender as SmsModemBlock;
            string sms = (e as MessageArgs).messageText;
            _hub.Invoke("SmsReceived", sms, comm.CurrentCommandID).Wait();
            comm.SetWaiting(false);
        }

        private void Receiver_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var comm = sender as SmsModemBlock;
            var messagelist = comm.ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

            var messageString = string.Empty;
            StringBuilder sb = new StringBuilder();
            if (messagelist.Length > 0)
            {
                // на случай, если длинное сообщение
                IList<SmsPdu> longMsg = new List<SmsPdu>();

                foreach (ShortMessageFromPhone message in messagelist)
                {
                    //сырая хуета
                    var pdu = new SmsDeliverPdu(message.Data, true, -1);

                    var origin = pdu.OriginatingAddress;
                    var txt = pdu.UserDataText;

                    bool isMultiPart = SmartMessageDecoder.IsPartOfConcatMessage(pdu);

                    // если у сообщения есть датахедер, то скорее всего оно длинное
                    if (pdu.UserDataHeaderPresent && isMultiPart)
                    {
                        if (longMsg.Count == 0 || SmartMessageDecoder.ArePartOfSameMessage(longMsg.Last(), pdu))
                        {
                            longMsg.Add(pdu);
                        }
                        if (SmartMessageDecoder.AreAllConcatPartsPresent(longMsg)) // is Complete
                        {
                            txt = SmartMessageDecoder.CombineConcatMessageText(longMsg);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    sb.AppendFormat("{0}: {1}", origin, txt);
                }

                var sms = sb.ToString();
                _hub.Invoke("SmsReceived", sms, comm.CurrentCommandID).Wait();
                comm.SetWaiting(false);
                //Subscriptions.Remove(comm);
            }
        }


        public void Disconnect(bool stopcalled = false)
        {
            //_hub.Invoke("OnDisconnected", stopcalled);
            connection.Stop();
        }

        ~CallbackHandler()
        {
            try
            {
                //_hub.Invoke("OnDisconnected", true);
                connection.Stop();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Команда от сервера
        /// </summary>
        public enum ServerCommand
        {
            /// <summary>
            /// Выслать всю информацию по активным сим картам
            /// </summary>
            GetInfo,
            /// <summary>
            /// Ждать СМС
            /// </summary>
            WaitSms,
            /// <summary>
            /// Проблемы с сим-картой, отключить
            /// </summary>
            SimCardMalfunction
        }
    }
}

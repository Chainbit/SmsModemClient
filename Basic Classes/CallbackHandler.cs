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

namespace SmsModemClient
{
    public class CallbackHandler
    {
        IHubProxy _hub;
        HubConnection connection;
        private ComPortManager Manager = null;
        private string previousCommand = string.Empty;

        public CallbackHandler(string ServerAddress)
        {
            InitializeConnection(ServerAddress);
            Subscribe();
            Invoke();
        }

        public CallbackHandler(string ServerAddress, ComPortManager mgr)
        {
            Manager = mgr;
            InitializeConnection(ServerAddress);
            Subscribe();
            Invoke();
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
            _hub = connection.CreateHubProxy("TestHub");

            connection.Start().Wait();
        }

        /// <summary>
        /// Подписывается на основные события на сервере
        /// </summary>
        private void Subscribe()
        {
            _hub.On("ReceiveLength", x => MessageBox.Show(x));
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
            _hub.Invoke("PrintClientsId").Wait();
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
            ServerCommand command = (ServerCommand)Enum.Parse(typeof(ServerCommand), cmd.Command);
            switch (command)
            {
                case ServerCommand.GetInfo:
                    GetInfo();
                    break;
                case ServerCommand.WaitSms:
                    WaitSMS(cmd.Destination, cmd.Pars);
                    break;
                case ServerCommand.SimCardMalfunction:
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
        private async void WaitSMS(string id, string[] pars)
        {
            var receiver = Manager.activeComs.First(x => x.Id == id);
            var type = pars[0];
            var search = pars[1] ?? "";

            string sms = null;

            switch (type)
            {
                case "SearchByNumber":
                    sms = await receiver.WaitSMSFromNumber(search);
                    break;
                case "SearchByContent":
                    sms = await receiver.WaitSMSWithContent(search);
                    break;
                case "ReceiveLast":

                    break;
                default:
                    break;
            }
            if (sms!=null)
            {
                _hub.Invoke("SmsReceived", sms).Wait();
                _hub.Invoke("DetermineLength", sms).Wait();
            }
        }

        ~CallbackHandler()
        {
            connection.Stop();
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

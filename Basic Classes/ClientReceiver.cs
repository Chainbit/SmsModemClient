using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.ServiceModel;
using SmsModemClient.MyWcfService;

namespace SmsModemClient
{
    class ClientReceiver 
    {
        //адрес на сервере, который будет слушать программа
        private string ServerAddress = "http://localhost:8888/connection/";

        HttpListener listener = new HttpListener();
        ComPortManager manager;
        ClientSender sender = new ClientSender();

        ManagerClient client = new ManagerClient("BasicHttpBinding_IManager");

        public ClientReceiver(ComPortManager cpm)
        {
            manager = cpm;
        }

        public Task ListenForCommands()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    sender.SendRequest(manager.MacAddress+"_listening");
                    Thread.Sleep(3000);
                }
            });
        }

        private void ReturnCommand()
        {

        }

        private void StartListen()
        {
            listener.AuthenticationSchemes = AuthenticationSchemes.Basic;
            listener.Prefixes.Add(ServerAddress);
            listener.Start();
        }

        private async void GetResponse()
        {
            // метод GetContext блокирует текущий поток, ожидая получение запроса 
            HttpListenerContext context = await listener.GetContextAsync();
            HttpListenerRequest request = context.Request;
            var command = request.QueryString.Get("commandName");
            var parameters = request.QueryString.Get("parameters");

            if (command != null)
            {
                await ParseCommand(command);
            }

            // получаем объект ответа
            HttpListenerResponse response = context.Response;           

            // создаем ответ в виде кода html
            string responseStr = "<html><head><meta charset='utf8'></head><body>Привет мир!</body></html>";
            byte[] buffer = Encoding.Unicode.GetBytes(responseStr);
            // получаем поток ответа и пишем в него ответ
            response.ContentLength64 = buffer.Length;
            using (Stream output = response.OutputStream)
            {
                output.Write(buffer, 0, buffer.Length);
            }
        }

        private Task ParseCommand(string commandName)
        {
            return Task.Factory.StartNew(() =>
            {
                CommCommand command = (CommCommand)Enum.Parse(typeof(CommCommand), commandName);
                switch (command)
                {
                    case CommCommand.SendActiveComs:
                        break;
                    case CommCommand.ReturnSMS:
                        
                        break;
                    default:
                        break;
                }
            });
        }

        private void StopListen()
        {
            listener.Stop();
        }

        public enum CommCommand
        {
            SendActiveComs,
            ReturnSMS
        }
    }
}

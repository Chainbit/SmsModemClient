using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmsModemClient
{
    class ClientReceiver : IDisposable
    {
        /// <summary>
        /// Локальный адрес
        /// </summary>
        private IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        private int port = 8888;
        private TcpListener client = null;

        public string serverResponse = string.Empty;

        public ClientReceiver()
        {
            client = new TcpListener(localAddr, port);
            client.Start();
        }
        
        /// <summary>
        /// Слушаем что говорит сервер
        /// </summary>
        public void Listen()
        {
            // получаем входящее подключение
            TcpClient server = client.AcceptTcpClient();
            using (NetworkStream stream = server.GetStream())
            {
                while (true)
                {
                    var data = new byte[256];// буфер считывания
                    StringBuilder builder = new StringBuilder(); // строка
                    int bytes = 0;// индекс

                    while (stream.DataAvailable)
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    serverResponse = builder.ToString();
                }
            }

        }

        public void Dispose()
        {
            client.Stop();
        }

    }
}

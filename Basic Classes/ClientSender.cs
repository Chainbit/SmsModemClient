using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace SmsModemClient
{
    class ClientSender : IDisposable
    {
        // Данные для подключения
        private const int port = 8888;
        private const string server = "127.0.0.1";

        TcpClient client;
        BinaryFormatter formatter;

        public ClientSender()
        {
            client = new TcpClient(server, port);
            formatter = new BinaryFormatter();
        }

        /// <summary>
        /// Cериализует и отправляет объект на сервер
        /// </summary>
        /// <param name="comm">Объект для отправки</param>
        public void SendObject(SmsModemBlock comm)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    formatter.Serialize(stream, comm);
                    System.Windows.Forms.MessageBox.Show("Object Serialized!");
                    var data = new byte[256];// буфер считывания
                    StringBuilder response = new StringBuilder(); // строка
                    int bytes = 0;// индекс
                    //считываем ответ
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        response.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    // работаем с ответом
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
        }

        public void Dispose()
        {
            client.Close();
        }
    }
}

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

        /// <summary>
        /// Отправляет текстовое сообщение на сервер
        /// </summary>
        /// <param name="txt"></param>
        public void SendText(string txt)
        {
            using (NetworkStream stream = client.GetStream())
            {
                byte[] data = Encoding.Unicode.GetBytes(txt);
                stream.Write(data, 0, data.Length);
            }
        }

        /// <summary>
        /// Посылает на сервер сообщение об ошибке какого-либо порта
        /// </summary>
        /// <param name="comm">Объект вызвавший ошибку</param>
        public void SendObjectError(SmsModemBlock comm)
        {
            SendText(string.Format("MALFUNCTION: {0} {1}", comm.Id, comm.MacAddress));
        }

        /// <summary>
        /// Послать SQL запрос на сервер
        /// </summary>
        /// <param name="query"></param>
        public void SendSqlQuery(string query)
        {
            SendText(string.Format("SQL: {0}", query));
        }

        public void Dispose()
        {
            client.Close();
        }
    }
}

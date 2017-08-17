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

namespace SmsModemClient
{
    class ClientSender
    {
        private string ServerAddress = "http://localhost:8888/connection/";

        private NetworkCredential logoPass = new NetworkCredential("admin", "admin");

        public async void SendRequest(string reqData)
        {
            await PostRequestAsync(reqData);
        }

        public async void SendRequest(byte[] reqData)
        {
            await PostRequestAsync(reqData);
        }

        public async Task RequestWithParams(NameValueCollection pars)
        {
            using (WebClient web = new WebClient())
            {
                web.UploadValuesAsync((new UriBuilder(ServerAddress)).Uri, pars);
            } 
        }

        /// <summary>
        /// Отправляет данные пост запросом
        /// </summary>
        /// <param name="data">Строка с данными</param>
        /// <returns></returns>
        private async Task PostRequestAsync(string data)
        {
            WebRequest request = WebRequest.Create(ServerAddress);
            request.Credentials = logoPass;
            request.Method = "POST"; // для отправки используется метод Post
            // данные для отправки
            //data = "sName=Hello world!";
            //название_параметра=данные
            // преобразуем данные в массив байтов
            byte[] byteArray = Encoding.Unicode.GetBytes(data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/x-www-form-urlencoded";
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;

            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            response.Close();
        }

        /// <summary>
        /// Отправляет данные пост запросом
        /// </summary>
        /// <param name="data">Массив байтов с данными</param>
        /// <returns></returns>
        private async Task PostRequestAsync(byte[] data)
        {
            WebRequest request = WebRequest.Create(ServerAddress);
            request.Credentials = logoPass;
            request.Method = "POST"; // для отправки используется метод Post
            // данные для отправки
            //data = "sName=Hello world!";
            //название_параметра=данные

            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/x-www-form-urlencoded";
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = data.Length;

            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(data, 0, data.Length);
            }

            WebResponse response = await request.GetResponseAsync();

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            response.Close();
        }

        /// <summary>
        /// Отправляет объект на сервер
        /// </summary>
        /// <param name="obj"></param>
        private async void SendObject(object obj)
        {
            WebRequest request = WebRequest.Create(ServerAddress);
            request.Credentials = logoPass;
            request.Method = "POST"; // для отправки используется метод Post
            bool responseReceived = false;

            do
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    (new DataContractJsonSerializer(obj.GetType())).WriteObject(dataStream, obj);
                }

                // ждем ответ
                WebResponse response = await request.GetResponseAsync();
                string respText;

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        respText = reader.ReadToEnd();
                    }
                }
                response.Close();

                //обработываем результат
                if (respText.ToLower().Contains("success"))
                {
                    responseReceived = true;
                }
            }
            while (!responseReceived);

        }

        public async void GetServerResponse()
        {
            WebRequest request = WebRequest.Create(ServerAddress);
            request.Credentials = logoPass;
            request.Method = "POST"; // для отправки используется метод Post
            WebResponse response = await request.GetResponseAsync();
        }
    }
}

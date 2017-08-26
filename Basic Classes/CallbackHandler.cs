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

namespace SmsModemClient
{
    public class CallbackHandler
    {
        IHubProxy _hub;
        HubConnection connection;

        public CallbackHandler(string ServerAddress)
        {
            connection = new HubConnection(ServerAddress);
            _hub = connection.CreateHubProxy("TestHub");
            connection.Start().Wait();

            string line = "Hello, Motherfucker!";
            _hub.On("ReceiveLength", x => MessageBox.Show(x));
            _hub.On("onConnected", x => MessageBox.Show(x));
            _hub.On("broadcast", x => MessageBox.Show(x));
            _hub.Invoke("DetermineLength", line).Wait();
        }

        ~CallbackHandler()
        {
            connection.Stop();
        }
    }
}

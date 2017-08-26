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
using SmsModemClient.CallbackService;
using System.Windows.Forms;

namespace SmsModemClient
{
    class ClientReceiver : ICommunicationServiceCallback
    {
        bool waiting = false;

        public CommunicationServiceClient subscriber;

        public ClientReceiver()
        {
            subscriber = new CommunicationServiceClient(new InstanceContext(null, this));

            //create a unique callback address so multiple clients can run on one machine
            WSDualHttpBinding binding = (WSDualHttpBinding)subscriber.Endpoint.Binding;
            binding.Security.Mode = WSDualHttpSecurityMode.None;
            //binding.Security.Message = 

            //Subscribe.
        }

        private async void GetResponse()
        {
           
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

        public void CreateCommand(string dest, string cmd, string pars)
        {
            MessageBox.Show(string.Format("New Message from server to {0}, command: {1}, {2}", dest, cmd, pars));
        }

        public void NewCommandArrived(Command cmd)
        {
            throw new NotImplementedException();
        }

        public enum CommCommand
        {
            SendActiveComs,
            ReturnSMS
        }

        ~ClientReceiver()
        {
        }
    }
}

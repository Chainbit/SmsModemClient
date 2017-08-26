using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectionService
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде, SVC-файле и файле конфигурации.
    // ПРИМЕЧАНИЕ. Чтобы запустить клиент проверки WCF для тестирования службы, выберите элементы Service1.svc или Service1.svc.cs в обозревателе решений и начните отладку.
    public class Manager : IManager
    {
        public List<string> taskPool = new List<string>();

        public string GetCommand(string clientID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Возвращает номер
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetData(string value)
        {
            Thread.Sleep(11000);
            return string.Format("You entered: {0}", value);
        }

        public CommandClass GetDataUsingDataContract(CommandClass composite)
        {
            return null;
        }

        public void SendDataToServer(string data)
        {
            throw new NotImplementedException();
        }

        public string TestConnection()
        {
            return "OK";
        }

        /// <summary>
        /// Получает задание для клиента
        /// </summary>
        /// <param name="ClientId">Идентификатор клиента</param>
        /// <returns></returns>
        public string GetTaskForClient(string ClientId)
        {
            return AskForTask(ClientId);
        }

        private string AskForTask(string Id)
        {
            do
            {
                if (TaskFound(Id))
                {
                    return ReturnTask();
                }
                Thread.Sleep(1000);
            } while (true);
        }

        private bool TaskFound(string id)
        {
            if (taskPool.Contains(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void addToPool(string id)
        {
            taskPool.Add(id);
        }

        private string ReturnTask()
        {
            return "ICCID_COMMAND_PARAMS";
        }

        public List<string> ReturnPool()
        {
            return taskPool;
        }
    }
}

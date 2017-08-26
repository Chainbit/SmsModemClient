using SmsModemClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ConnectionService
{
    #region in use
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IManager
    {
        /// <summary>
        /// проверка соединения
        /// </summary>
        /// <returns> OK </returns>
        [OperationContract]
        string TestConnection();

        /// <summary>
        /// Получает команду для данного клиента
        /// </summary>
        /// <param name="clientID">ID клиента</param>
        /// <returns></returns>
        [OperationContract]
        string GetCommand(string clientID);

        /// <summary>
        /// Отправка данных на сервер
        /// </summary>
        /// <param name="data">Строка с данными</param>
        [OperationContract]
        void SendDataToServer(string data);

        [OperationContract]
        CommandClass GetDataUsingDataContract(CommandClass composite);

        /// <summary>
        /// Returns number
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [OperationContract]
        string GetData(string value);

        /// <summary>
        /// Получает задание для клиента
        /// </summary>
        /// <param name="ClientId">Идентификатор клиента</param>
        /// <returns></returns>
        [OperationContract]
        string GetTaskForClient(string ClientId);

        [OperationContract]
        void addToPool(string id);

        [OperationContract]
        List<string> ReturnPool();
    }
    

    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    [DataContract]
    public class CommandClass
    {
        [DataMember]
        public string Destination { get; private set; }
        [DataMember]
        public string Command { get; private set; }
        [DataMember]
        public string Params { get; private set; }

        /// <summary>
        /// Создание нового объекта класса <see cref="CommandClass"/>
        /// </summary>
        /// <param name="dest">Получатель команды</param>
        /// <param name="cmd">Команда</param>
        /// <param name="pars">Параметры (необязательно)</param>
        public CommandClass(string dest, string cmd, string pars="")
        {

        }
    }
    #endregion

   
}

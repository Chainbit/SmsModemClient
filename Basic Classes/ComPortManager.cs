using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using System.Net.NetworkInformation;

namespace SmsModemClient
{
    public class ComPortManager
    {

        public List<SmsModemBlock> activeComs = new List<SmsModemBlock>();
        public ConcurrentQueue<SmsModemBlock> activeComsQueue = new ConcurrentQueue<SmsModemBlock>();
        public MainForm MF;
        
        public string MacAddress { get; private set; }

        public ComPortManager(MainForm mainForm)
        {
            MacAddress = GetMacAddress().ToString();
            InitializeManager();
            MF = mainForm;
            mainForm.loadComsButton.Enabled = true;
        }

        /// <summary>
        /// Обработчик события <see cref="SmsModemBlock.NumberReceived"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComPortManager_NumberReceived(object sender, EventArgs e)
        {
            if (sender is SmsModemBlock)
            {
                AddItemToDb((SmsModemBlock)sender);
            }
        }

        /// <summary>
        /// Запуск методов, для заполнения таблицы
        /// </summary>
        public void InitializeManager()
        {
            GetModemPorts();
            OpenAllPorts();
            GetModemData();
            // дальше работаем с листом
            activeComs = activeComsQueue.ToList();
            GC.Collect();
            GetModemTels();
            //SetPortsReady(); //проверить, есть ли симки!

        }

        /// <summary>
        /// Получает порты на которых висят модемы (в несколько потоков)
        /// </summary>
        public void GetModemPorts()
        {
            //очищаем старую Queue
            activeComsQueue = new ConcurrentQueue<SmsModemBlock>();

            activeComs.Clear();

            var ports = SerialPort.GetPortNames();

            List<Thread> _threadList = new List<Thread>();

            foreach (var port in ports)
            {
                //создаем объект устройства
                SmsModemBlock com = new SmsModemBlock(port, 115200);
                //И поток
                Thread myThread = new Thread(new ParameterizedThreadStart(CheckModemConncetion));
                myThread.Name = port + " GetPort";
                _threadList.Add(myThread);
                // передаем в поток наш порт
                myThread.Start(com);
            }
            // ждем всех
            WaitForAllThreadsToComplete(_threadList);

            //activeComs = activeComsQueue.ToList();
        }

        public Task GetModemPortsAsync()
        {
            List<Thread> _threadList = new List<Thread>();
            foreach (var port in activeComs)
            {
                Thread tclose = new Thread(port.Close);
                _threadList.Add(tclose);
                tclose.Start();
            }
            WaitForAllThreadsToComplete(_threadList);

            Task t = new Task(InitializeManager);
            t.Start();
            return t;
        }

        /// <summary>
        /// Проверка на наличие нашего устройства на этом порте
        /// </summary>
        /// <param name="com"></param>
        private void CheckModemConncetion(object com)
        {
            SmsModemBlock comm = (SmsModemBlock)com;
            //открываем соединение
            using (new CommStream(comm))
            {
                if (comm.IsConnected())
                {
                    if (comm.IsSimPresent())
                    {
                        comm.SimBankId = this.MacAddress;
                        activeComsQueue.Enqueue(comm);
                    }
                }
            }
        }

        /// <summary>
        /// Получает свойства устройства
        /// </summary>
        /// <param name="com"></param>
        private void GetModemData()
        {
            List<Thread> _threadList = new List<Thread>();

            foreach (SmsModemBlock comm in activeComsQueue)
            {

                //создаем поток со всеми нужными методами
                Thread thread = new Thread(new ParameterizedThreadStart(GetModemData));
                thread.Name = comm.PortName + " GetModemData";
                _threadList.Add(thread);
                thread.Start(comm);
            }
            // ждем всех
            WaitForAllThreadsToComplete(_threadList);
        }

        private void GetModemData(object com)
        {
            SmsModemBlock comm = (SmsModemBlock)com;

            GetCurrentIMSI(comm);
            GetModemOperator(comm);
            GetSignalLevel(comm);
            GetBalance(comm);
        }

        /// <summary>
        /// Получает номера телефонов из БД либо запрашивает вручную
        /// </summary>
        private void GetModemTels()
        {
            using (ComContext db = new ComContext())
            {
                foreach (SmsModemBlock item in activeComs)
                {
                    //ищем сходства в БД
                    var temp = db.activeComs.Find(item.Id);
                    if (temp != null) // если находим, присваиваем значение
                    {
                        item.TelNumber = temp.TelNumber;
                    }
                    else // если нет посылаем запрос
                    {
                        Thread thread = new Thread(new ParameterizedThreadStart(RequestTelNumber));
                        thread.Name = item.PortName + " GetTelNumber";
                        thread.Start(item);
                    }
                }                
            }
        }

        /// <summary>
        /// Переводит порты в состояние готовности
        /// </summary>
        private void SetPortsReady()
        {
            foreach (var item in activeComs)
            {
                item.OnReady();
            }
        }

        /// <summary>
        /// Выполняет запрос номера телефона в зависимости от оператора
        /// </summary>
        /// <param name="block"></param>
        public void RequestTelNumber(object b)
        {
            SmsModemBlock block = (SmsModemBlock)b;
            block.SetWaiting(true);
            try
            {
                switch (block.Operator.ToLower())
                {
                    case "beeline":
                        block.GetNumBeeline();
                        break;
                    case "megafon":
                        block.GetNumMegafon();
                        break;
                    case "mts rus":
                        block.GetNumMTS();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, block.PortName);
            }
        }

        /// <summary>
        /// Получить значение оператора для блока
        /// <para>Необходимо выполнять при открытом порте</para>
        /// </summary>
        /// <param name="block"></param>
        public void GetModemOperator(SmsModemBlock block)
        {
            block.GetOperator();
        }

        /// <summary>
        /// Получить номер сим карты для блока
        /// </summary>
        /// <param name="block"></param>
        public void GetCurrentIMSI(SmsModemBlock block)
        {
            block.GetICCID();
        }

        /// <summary>
        /// Получает уровень сигнала
        /// </summary>
        /// <param name="comm">Модем, для которого делается запрос</param>
        private void GetSignalLevel(SmsModemBlock comm)
        {
            comm.GetSignalStrength();
        }

        /// <summary>
        /// Запрашивает баланс
        /// </summary>
        /// <param name="comm"></param>
        private void GetBalance(SmsModemBlock comm)
        {
            comm.GetBalance();
        }

        /// <summary>
        /// Открывает все порты
        /// </summary>
        private void OpenAllPorts()
        {
            foreach (var port in activeComsQueue)
            {
                if (!port.IsOpen())
                {
                    Thread openPort = new Thread(port.Open);
                    openPort.Name = port.PortName + " Open";
                    openPort.Start();
                }
            }
        }

        /// <summary>
        /// Закрывает все порты
        /// </summary>
        public void CloseAllPorts()
        {
            foreach (SmsModemBlock comm in activeComs)
            {
                if (comm.IsOpen())
                {
                    Thread t = new Thread(comm.Close);
                    t.Start();
                }
            }
        }

        /// <summary>
        /// Метод, блокирующий основной поток, пока не выполнятся заданные потоки
        /// </summary>
        /// <param name="threadList">Список потоков для ожидания</param>
        private void WaitForAllThreadsToComplete(List<Thread> threadList)
        {
            foreach (Thread thread in threadList)
            {
                thread.Join();
            }
        }

        /// <summary>
        /// Получает мак адрес компухтера
        /// </summary>
        /// <returns></returns>
        private static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Только рабочее устройство
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }

        /// <summary>
        /// Добавляет все данные в бд
        /// </summary>
        private void AddAllToDb()
        {
            using (ComContext DB = new ComContext())
            {
                foreach (var item in activeComs)
                {
                    DB.activeComs.Add(item);
                }
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// Добавляет элемент в бд
        /// </summary>
        /// <param name="comm"></param>
        public void AddItemToDb(SmsModemBlock comm)
        {
            using (ComContext DB = new ComContext())
            {
                DB.activeComs.Add(comm);
                DB.SaveChanges();
            }
        }

        /// <summary>
        /// Очищает все входящие смс у всех симок
        /// </summary>
        private void ClearAllInbox()
        {
            foreach (SmsModemBlock comm in activeComs)
            {
                comm.ClearInbox();
            }
        }

        /// <summary>
        /// Удаляет элемент из списка активных
        /// </summary>
        /// <param name="comm"></param>
        public void DeleteFromActiveComs(SmsModemBlock comm)
        {
            lock (this)
            {
                var comForm = MF.OwnedForms.First(f => ((CommForm)f).comm == comm);
                comForm.Close();
                activeComs.Remove(comm);
            }
        }

        public string SendManagerInfo()
        {
            ////ДЛЯ ТЕСТА!
            //using (ComContext db = new ComContext())
            //{
            //    activeComs = db.activeComs.ToList();
            //}
            return Newtonsoft.Json.JsonConvert.SerializeObject(activeComs);
        }
    }
}

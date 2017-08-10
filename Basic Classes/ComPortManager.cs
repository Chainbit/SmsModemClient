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

namespace SmsModemClient
{
    class ComPortManager
    {

        public List<SmsModemBlock> activeComs = new List<SmsModemBlock>();
        public ConcurrentQueue<SmsModemBlock> activeComsQueue = new ConcurrentQueue<SmsModemBlock>();

        public ComPortManager(MainForm mainForm)
        {
            InitializeManager();
            mainForm.loadComsButton.Enabled = true;
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
            GetModemTels();            
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
                    activeComsQueue.Enqueue(comm);
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
                Thread thread = new Thread(()=> 
                {
                    GetModemOperator(comm);
                    GetCurrentIMSI(comm);
                    GetSignalLevel(comm);
                });
                thread.Name = comm.PortName + " GetModemData";
                _threadList.Add(thread);
                thread.Start();
            }
            // ждем всех
            WaitForAllThreadsToComplete(_threadList);
        }

        /// <summary>
        /// Запрашивает номера телефонов
        /// </summary>
        private void GetModemTels()
        {
            foreach (SmsModemBlock item in activeComs)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(RequestTelNumber));
                thread.Name = item.PortName + " GetTelNumber";
                thread.Start(item);
            }
        }

        /// <summary>
        /// Выполняет запрос номера телефона в зависимости от оператора
        /// </summary>
        /// <param name="block"></param>
        public async void RequestTelNumber(object b)
        {
            SmsModemBlock block = (SmsModemBlock)b;
            try
            {
                switch (block.Operator.ToLower())
                {
                    case "beeline":
                        await block.GetNumBeeline();
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
        /// Получает уровень сигнала
        /// </summary>
        /// <param name="comm"></param>
        private void GetSignalLevel(SmsModemBlock comm)
        {
            var signalStrength = comm.GetSignalQuality().SignalStrength;

            if (signalStrength < 10)
            {
                comm.SignalLevel = SmsModemBlock.Signal.Poor;
            }
            else if (signalStrength >= 10 && signalStrength <= 14)
            {
                comm.SignalLevel = SmsModemBlock.Signal.Ok;
            }
            else if (signalStrength >= 15 && signalStrength <= 19)
            {
                comm.SignalLevel = SmsModemBlock.Signal.Good;
            }
            else if (signalStrength > 19)
            {
                comm.SignalLevel = SmsModemBlock.Signal.Excellent;
            }
            else
            {
                comm.SignalLevel = SmsModemBlock.Signal.None;
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
    }
}

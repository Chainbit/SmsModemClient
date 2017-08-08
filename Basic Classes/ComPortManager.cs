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

        public List<SmsModemBlock2> activeComs = new List<SmsModemBlock2>();
        private ConcurrentQueue<SmsModemBlock2> activeComsQueue = new ConcurrentQueue<SmsModemBlock2>();

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
        }

        /// <summary>
        /// Получает порты на которых висят модемы
        /// </summary>
        public void GetModemPorts()
        {
            //очищаем старую Queue
            activeComsQueue = new ConcurrentQueue<SmsModemBlock2>();
            // List тоже
            activeComs.Clear();

            var ports = SerialPort.GetPortNames();

            List<Thread> _threadList = new List<Thread>();

            foreach (var port in ports)
            {
                //создаем объект устройства
                //GsmCommMain comm = new GsmCommMain(port, 115200);
                SmsModemBlock2 com = new SmsModemBlock2(port, 115200);
                //И поток
                Thread myThread = new Thread(new ParameterizedThreadStart(GetModemData));
                myThread.Name = port + " GetPort";
                _threadList.Add(myThread);
                // передаем в поток наш порт
                myThread.Start(com);
            }
            // ждем всех
            WaitForAllThreadsToComplete(_threadList);

            activeComs = activeComsQueue.ToList();
        }

        public Task GetModemPortsAsync()
        {
            Task t = new Task(InitializeManager);
            t.Start();
            return t;
        }

        /// <summary>
        /// Проверка на наличие нашего устройства на этом порте
        /// </summary>
        /// <param name="com"></param>
        private void GetModemData(object com)
        {
            SmsModemBlock2 comm = (SmsModemBlock2)com;

            using (new CommStream(comm))
            {
                if (comm.IsConnected())
                {
                    GetModemOperator(comm);
                    GetCurrentIMSI(comm);
                    activeComsQueue.Enqueue(comm);
                }
            }

            Thread.CurrentThread.Abort();
        }

        /// <summary>
        /// Получить значение оператора для блока
        /// <para>Необходимо выполнять внутри блока <see langword="using " cref="CommStream"/></para>
        /// </summary>
        /// <param name="block"></param>
        public void GetModemOperator(SmsModemBlock2 block)
        {
            block.GetOperator();
        }

        /// <summary>
        /// Получить номер сим карты для блока
        /// </summary>
        /// <param name="block"></param>
        public void GetCurrentIMSI(SmsModemBlock2 block)
        {
            block.GetICCID();
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

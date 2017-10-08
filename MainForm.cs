using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmsModemClient
{
    public partial class MainForm : Form
    {
        public ComPortManager manager;
        private System.Windows.Forms.Timer timer;

        /// <summary>
        /// Обработчик команд сервера. Конструктор сейчас находится в методе <see cref="Connect"/>
        /// </summary>
        public CallbackHandler callback = null;
        public bool ServerConnected = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            manager = new ComPortManager(this);

            FillDatagrid();

            foreach (var port in manager.activeComs)
            {
                AddOwnedForm(new CommForm(port));
            }

            // создаем таймер 
            timer = new System.Windows.Forms.Timer();
            timer.Interval = (1 * 1000);
            timer.Tick += Timer_Tick;

            if (autoconnectCheckBox.Checked)
            {
                Connect().Wait();
                ToggleButtons();
            }
        }

        // кнопка "загрузить ком-порты"
        private async void loadComsButton_Click(object sender, EventArgs e)
        {
            (sender as Button).Enabled = false;
            //Ждкм выполнения команды
            await manager.GetModemPortsAsync();

            FillDatagrid();

            (sender as Button).Enabled = true;
        }

        /// <summary>
        /// Заполнение датагрида
        /// </summary>
        private void FillDatagrid()
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action(FillDatagrid));
                return;
            }
            var list = manager.activeComs;
            //очищаем все
            ComPortsDataGrid.Rows.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                //заполняем датагрид
                ComPortsDataGrid.Rows.Add();
                ComPortsDataGrid.Rows[i].Cells["number"].Value = i+1;
                ComPortsDataGrid.Rows[i].Cells["ComPortName"].Value = list[i].PortName;
                ComPortsDataGrid.Rows[i].Cells["SimId"].Value = list[i].Id;
                ComPortsDataGrid.Rows[i].Cells["TelNumber"].Value = list[i].TelNumber;
                ComPortsDataGrid.Rows[i].Cells["SIMoperator"].Value = list[i].Operator;
                ComPortsDataGrid.Rows[i].Cells["Balance"].Value = list[i].Balance;
                var img = Properties.Resources.none;
                switch (list[i].SignalLevel)
                {
                    case SmsModemBlock.Signal.Poor:
                        img = Properties.Resources.poor;
                        break;
                    case SmsModemBlock.Signal.Ok:
                        img = Properties.Resources.ok;
                        break;
                    case SmsModemBlock.Signal.Good:
                        img = Properties.Resources.good;
                        break;
                    case SmsModemBlock.Signal.Excellent:
                        img = Properties.Resources.excellent;
                        break;
                    default:
                        break;
                }
                ComPortsDataGrid.Rows[i].Cells["SignalTxt"].Value = list[i].SignalLevel.ToString();
                (ComPortsDataGrid.Rows[i].Cells["Signal"] as DataGridViewImageCell).Value = img;
                //CheckIfOpen(i);
            }          
        }

        /// <summary>
        /// Асинхронное заполнение датагрида
        /// </summary>
        /// <returns></returns>
        private Task FillDataGridAsync()
        {
            return Task.Run(() =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(FillDatagrid));
                }
                else
                {
                    FillDatagrid();
                }
                return;
            });
        }

        /// <summary>
        /// Нажатие на кнопку Select
        /// </summary>
        private void ComPortsDataGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0) // если кнопка
            {
                try
                {
                    //обработка выбора СОМ-порта
                    var portName = senderGrid.Rows[e.RowIndex].Cells["ComPortName"].Value.ToString();
                    var port = manager.activeComs.Find(com => com.PortName == portName);
                    //MessageBox.Show(string.Format("Выбран порт {0}", portName));

                   OwnedForms.First(f=>f.Name == "CommForm"+portName).Show();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Ничего не выбрано!");
                }
            }
        }

        /// <summary>
        /// Включено ли автообновление
        /// </summary>
        private bool isRefreshOn = false;

        /// <summary>
        /// Меняет значение <see cref="isRefreshOn"/> на противоположное
        /// </summary>
        private void ToggleRefresh()
        {
            isRefreshOn = !isRefreshOn;
        }

        private void toggleAutoUpdateBtn_Click(object sender, EventArgs e)
        {
            ToggleRefresh();

            switch (isRefreshOn)
            {
                case true:
                    timer.Start();
                    toggleAutoUpdateBtn.Text = "Автообновление: вкл.";
                    break;
                case false:
                    timer.Stop();
                    toggleAutoUpdateBtn.Text = "Автообновление: выкл.";
                    break;
            }            
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await FillDataGridAsync();
        }

        /// <summary>
        /// Действие при закрытии формы
        /// </summary>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SmsModemBlock.cts.Cancel();
            manager.CloseAllPorts();
            Disconnect();
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new EventHandler(refreshButton_Click));
                return;
            }
            refreshButton.Enabled = false;
            await GetAllSignalStrength();
            await FillDataGridAsync();
            refreshButton.Enabled = true;
        }

        private Task GetAllSignalStrength()
        {
            return Task.Run(() =>
            {
                foreach (SmsModemBlock comm in manager.activeComs)
                {
                    //создаем поток со всеми нужными методами
                    Thread thread = new Thread(comm.GetSignalStrength);
                    thread.Name = comm.PortName + " GetSignalStrength";
                    thread.Start();
                }
            });
        }

        private async void connectButon_Click(object sender, EventArgs e)
        {
            connectButon.Enabled = false;
            IPtextBox.Enabled = false;
            await Connect();
            ToggleButtons();
        }

        private async void disconnectButton_Click(object sender, EventArgs e)
        {
            await Disconnect();
            ToggleButtons();
        }

        /// <summary>
        /// Пытается подключиться к серверу
        /// </summary>
        /// <returns></returns>
        private Task Connect()
        {
            return Task.Run(() =>
            {
                string address = Properties.Settings.Default.hubIP;
                if (!string.IsNullOrEmpty(address))
                {
                    try
                    {
                        callback = new CallbackHandler(address, manager);
                        ServerConnected = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\r\n" + ex.InnerException ?? "none" + "\r\n" + ex.TargetSite);
                        ServerConnected = false;
                    }
                }
                else
                {
                    MessageBox.Show("Введите адрес сервера!");
                    ServerConnected = false;
                }
            });
        }

        /// <summary>
        /// Разрывает соединение с сервером
        /// </summary>
        /// <returns></returns>
        public Task Disconnect()
        {
            return Task.Run(()=>
            {
                callback.Disconnect(true);
                //callback = null;
                GC.Collect();
                ServerConnected = false;
            });
        }

        /// <summary>
        /// Проверяет, значение <see cref="ServerConnected"/> и вносит соответствующие изменения в форму
        /// </summary>
        public void ToggleButtons()
        {
            if (ServerConnected)
            {
                connectButon.Enabled = false;
                IPtextBox.Enabled = false;
                connectPicture.Image = Properties.Resources.connected;
                disconnectButton.Enabled = true;
            }
            else
            {
                connectButon.Enabled = true;
                IPtextBox.Enabled = true;
                connectPicture.Image = Properties.Resources.disconnected;
                disconnectButton.Enabled = false;
            }
        }

        private void autoconnectCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.autoconnect = autoconnectCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void IPtextBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.hubIP = IPtextBox.Text;
            Properties.Settings.Default.Save();
        }

        public void Connection_Reconnected()
        {

            if (this.InvokeRequired)
            {
                Invoke(new Action(Connection_Reconnected));
            }
            ServerConnected = true;
            ToggleButtons();
        }

        public void Connection_Closed()
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action(Connection_Closed));
            }
            ServerConnected = false;
            ToggleButtons();
        }

        private void ComPortsDataGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] == ComPortsDataGrid.Columns["Balance"])
            {
                var portName = senderGrid.Rows[e.RowIndex].Cells["ComPortName"].Value.ToString();
                var port = manager.activeComs.Find(com => com.PortName == portName);

                port.GetBalance();
                FillDatagrid();
            }else if (senderGrid.Columns[e.ColumnIndex] == ComPortsDataGrid.Columns["TelNumber"])
            {
                var telNumber = senderGrid.Rows[e.RowIndex].Cells["TelNumber"].Value.ToString();
                if (telNumber.ToLower().Contains("ошибка"))
                {
                    var portName = senderGrid.Rows[e.RowIndex].Cells["ComPortName"].Value.ToString();
                    var port = manager.activeComs.Find(com => com.PortName == portName);

                    port.GetTelNumber();
                    FillDatagrid();
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(telNumber.Substring(2));
                Clipboard.SetText(sb.ToString());
            }
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            if (manager.activeComs.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in manager.activeComs)
                {
                    sb.AppendLine(item.TelNumber.Substring(2));
                }

                Clipboard.SetText(sb.ToString());
                MessageBox.Show("Список скопирован!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        private void callButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(numberTextBox.Text))
            {
                if (ussdRadio.Checked)
                {
                    USSDCheckForm form = new USSDCheckForm();
                    form.Show();

                    foreach (var comm in manager.activeComs)
                    {
                        Task<bool> ussdTask = Task.Run(new Func<bool>(() => comm.SendUSSD(numberTextBox.Text)));
                        form.GetTask(ussdTask, comm.TelNumber, "USSD "+numberTextBox.Text);
                    }
                }
                else if (callRadio.Checked)
                {
                    USSDCheckForm form = new USSDCheckForm();
                    form.Show();

                    foreach (var comm in manager.activeComs)
                    {
                        Task<bool> callTask = Task.Run(new Func<bool>(() => comm.CallTo(numberTextBox.Text)));
                        form.GetTask(callTask, comm.TelNumber, "Звонок "+numberTextBox.Text);
                    }
                }
            }
        }
    }
}

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
        private ComPortManager manager;

        private System.Windows.Forms.Timer timer;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            manager = new ComPortManager(this);
            //foreach (var port in manager.activeComs)
            //{
            //    port.NumberRecieved += FillDatagrid;
            //}
            FillDatagrid();
            // создаем таймер 
            timer = new System.Windows.Forms.Timer();
            timer.Interval = (1 * 1000);
            timer.Tick += Timer_Tick;
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
            //очищаем все
            ComPortsDataGrid.Rows.Clear();
            for (int i = 0; i < manager.activeComs.Count; i++)
            {
                //заполняем датагрид
                ComPortsDataGrid.Rows.Add();
                ComPortsDataGrid.Rows[i].Cells["number"].Value = i;
                ComPortsDataGrid.Rows[i].Cells["ComPortName"].Value = manager.activeComs[i].PortName;
                ComPortsDataGrid.Rows[i].Cells["SimId"].Value = manager.activeComs[i].ICCID;
                ComPortsDataGrid.Rows[i].Cells["TelNumber"].Value = manager.activeComs[i].TelNumber;
                ComPortsDataGrid.Rows[i].Cells["SIMoperator"].Value = manager.activeComs[i].Operator;
                ComPortsDataGrid.Rows[i].Cells["isOpen"].Value = manager.activeComs[i].IsOpen();
                CheckIfOpen(i);
            }
            int j = 0;            
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
                e.RowIndex >= 0)
            {
                try
                {
                    //обработка выбора СОМ-порта
                    var portName = senderGrid.Rows[e.RowIndex].Cells["ComPortName"].Value.ToString();
                    var port = manager.activeComs.Find(com => com.PortName == portName);
                    //MessageBox.Show(string.Format("Выбран порт {0}", portName));
                    (new CommForm(port)).Show();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Ничего не выбрано!");
                }
            }
        }

        /// <summary>
        /// метод для перекраски ячеек
        /// </summary>
        /// <param name="i"> номер ячейки</param>
        private void CheckIfOpen(int i)
        {
            ComPortsDataGrid.Rows[i].Cells["isOpen"].Value = manager.activeComs[i].IsOpen();
            var x = ComPortsDataGrid.Rows[i].Cells["ComPortName"].Value;

            if (manager.activeComs[i].IsOpen() == false)
            {
                ComPortsDataGrid.Rows[i].Cells["isOpen"].Style.BackColor = Color.Red;
            }
            else
            {
                ComPortsDataGrid.Rows[i].Cells["isOpen"].Style.BackColor = Color.Green;
            }
        }

        /// <summary>
        /// метод для перекраски ячеек
        /// </summary>
        /// <param name="i"> номер ячейки</param>
        private void CheckIfOpen(SmsModemBlock2 port)
        {
            int i=0;
            foreach (DataGridViewRow row in ComPortsDataGrid.Rows)
            {
                if (row.Cells["ComPortName"].Value.ToString().Contains(port.PortName))
                {
                    i = row.Index;
                }
            }

            ComPortsDataGrid.Rows[i].Cells["isOpen"].Value = port.IsOpen();

            if (port.IsOpen() == false)
            {
                ComPortsDataGrid.Rows[i].Cells["isOpen"].Style.BackColor = Color.Red;
            }
            else
            {
                ComPortsDataGrid.Rows[i].Cells["isOpen"].Style.BackColor = Color.Green;
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
              SmsModemBlock2.cts.Cancel();
        }

        private async void refreshButton_Click(object sender, EventArgs e)
        {
            refreshButton.Enabled = false;
            await FillDataGridAsync();
            refreshButton.Enabled = true;
        }
    }
}

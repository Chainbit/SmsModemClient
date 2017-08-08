using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmsModemClient
{
    public partial class MainForm : Form
    {
        private ComPortManager manager;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            manager = new ComPortManager(this);
            foreach (var port in manager.activeComs)
            {
                port.NumberRecieved += FillDatagrid;
            }
            FillDatagrid();
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
                ComPortsDataGrid.Rows[i].Cells["SIMoperator"].Value = manager.activeComs[i].Operator;
                ComPortsDataGrid.Rows[i].Cells["isOpen"].Value = manager.activeComs[i].IsOpen();
                CheckIfOpen(i);
            }
        }

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

        private bool isAllOpen = false;
        private void ToggleAllPortsButton_Click(object sender, EventArgs e)
        {
            ToggleAllPorts();
        }
        

        public void ToggleAllPorts()
        {
            if (!isAllOpen)
            {
                foreach (var port in manager.activeComs)
                {
                    if (!port.IsOpen())
                        port.Open();
                }
                isAllOpen = true;
                FillDatagrid();
                ToggleAllPortsButton.Text = "Закрыть все";
            }
            else
            {
                foreach (var port in manager.activeComs)
                {
                    if (port.IsOpen())
                        port.Close();
                }
                isAllOpen = false;
                FillDatagrid();
                ToggleAllPortsButton.Text = "Открыть все";
            }
        }
    }
}

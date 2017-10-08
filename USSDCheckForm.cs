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
    public partial class USSDCheckForm : Form
    {
        private Dictionary<string, Task<bool>> _taskList = new Dictionary<string, Task<bool>>();

        /// <summary>
        /// Вызывается, когда задача завершена
        /// </summary>
        private event Action TaskCompleted;

        public USSDCheckForm()
        {
            InitializeComponent();
        }

        private void USSDCheckForm_Load(object sender, EventArgs e)
        {
        }

        public async void GetTask(Task<bool> task, string telNumber, string taskType)
        {
            _taskList.Add(telNumber, task);
            task.ContinueWith(t => UpdateGrid(t));

            await FillDataGridAsync(task, telNumber, taskType);
        }

        /// <summary>
        /// Асинхронный вызов Метода <see cref="FillDataGrid(Task{bool}, string, string)"/>
        /// </summary>
        private Task FillDataGridAsync(Task<bool> task, string telNumber, string taskType)
        {
            return Task.Run(() =>
            {
                FillDataGrid(task, telNumber, taskType);
            });
        }

        /// <summary>
        /// Заполняет DataGridView переданными значениями
        /// </summary>
        /// <param name="task">Задача</param>
        /// <param name="telNumber">Номер телефона</param>
        /// <param name="taskType">Тип задачи</param>
        private void FillDataGrid(Task<bool> task, string telNumber, string taskType)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action(() => FillDataGrid(task, telNumber, taskType)));
                return;
            }
            var i = dataGridView1.Rows.Add();
            dataGridView1.Rows[i].Cells["TelNumber"].Value = telNumber;
            dataGridView1.Rows[i].Cells["TaskCol"].Value = taskType;

            if (task.IsFaulted)//если что-то пошло не так
            {
                Bitmap img = Properties.Resources.disconnected;
                (dataGridView1.Rows[i].Cells["ResultImg"] as DataGridViewImageCell).Value = img;
            }
        }

        /// <summary>
        /// Обновление датагрида
        /// </summary>
        /// <param name="t">Задача</param>
        private void UpdateGrid(Task<bool> t)
        {
            var searchValue = _taskList.First(pair => pair.Value == t).Key;
            //перебираем все таски
            int rowIndex = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //если находим ряд с этой задачей, получаем его номер
                if (row.Cells["TelNumber"].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            dataGridView1.Rows[rowIndex].Cells["ResultText"].Value = t.Result;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != -1)
            {
                SetImageInDatagrid(e.RowIndex);
            }
        }

        /// <summary>
        /// Выставляет картинку для ряда
        /// </summary>
        /// <param name="rowIndex">Индекс ряда</param>
        private void SetImageInDatagrid(int rowIndex)
        {                        
            var row = dataGridView1.Rows[rowIndex];            
            if ((bool)row.Cells["ResultText"].Value) //если значение в соседней ячейке true
            {
                row.Cells["ResultImg"].Value = Properties.Resources.check;
            }
            else
            {
                row.Cells["ResultImg"].Value = Properties.Resources.stop;
            }
        }

        private void USSDCheckForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var pair in _taskList)
            {
                pair.Value.Dispose();
            }
        }
    }
}

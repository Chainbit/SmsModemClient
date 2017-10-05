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
        private bool isWaiting = false;

        public USSDCheckForm()
        {
            InitializeComponent();
        }

        private void USSDCheckForm_Load(object sender, EventArgs e)
        {
            TaskCompleted += new Action(()=>WaitForAnything());
        }

        public void GetTask(Task<bool> task, string telNumber, string taskType)
        {
            _taskList.Add(telNumber, task);
            if (!isWaiting)
            {
                WaitForAnything();
            }

            var i = dataGridView1.Rows.Add();
            dataGridView1.Rows[i].Cells["TelNumber"].Value = telNumber;
            dataGridView1.Rows[i].Cells["TaskCol"].Value = taskType;

            var state = task.Result;
            Bitmap img;
            if (task.IsCompleted)
            {
                if (task.Result == true)
                {
                    img = Properties.Resources.check;
                    (dataGridView1.Rows[i].Cells["Result"] as DataGridViewImageCell).Value = img;
                }
                else
                {
                    img = Properties.Resources.stop;
                    (dataGridView1.Rows[i].Cells["Result"] as DataGridViewImageCell).Value = img;
                }
            }
            else if (task.IsFaulted)
            {
                img = Properties.Resources.disconnected;
                (dataGridView1.Rows[i].Cells["Result"] as DataGridViewImageCell).Value = img;
            }
        }

        /// <summary>
        /// Ждет выполнения любой задачи
        /// </summary>
        private async Task WaitForAnything()
        {
            await Task.Run(async () =>
             {
                 if (!_taskList.Values.Any(t => t.IsCompleted))
                 {
                     isWaiting = true;
                     await Task.WhenAny(_taskList.Values.ToArray());
                     UpdateGrid();
                     isWaiting = false;
                     TaskCompleted();
                 }
             });
        }

        

        private void UpdateGrid()
        {
            //перебираем все таски
            foreach (var item in _taskList)
            {
                string searchValue = item.Key;
                int rowIndex = -1;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    //если находим ряд с этой задачей, получаем его номер
                    if (row.Cells[1].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }
                // ну и обновляем картинку
                var completed = item.Value.Result;
                Bitmap img;
                if (completed)
                {
                    img = Properties.Resources.check;
                    (dataGridView1.Rows[rowIndex].Cells["Result"] as DataGridViewImageCell).Value = img;
                }
            }
        }

        private Task<bool> UpdateResult()
        {
            return Task.Run(()=> { return true; });
        }
    }
}

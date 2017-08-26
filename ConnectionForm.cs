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
    public partial class ConnectionForm : Form
    {
        private MainForm MF;

        public ConnectionForm(MainForm form)
        {
            MF = form;
            InitializeComponent();
            IPtextBox.Text = Properties.Settings.Default.hubIP;
        }

        private async void connectButton_Click(object sender, EventArgs e)
        {
            connectButton.Enabled = false;
            await Connect();
            connectButton.Enabled = true;
            this.Close();
        }

        /// <summary>
        /// Обновляет адрес IP и подключается
        /// </summary>
        /// <returns></returns>
        private Task Connect()
        {
            return Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(IPtextBox.Text))
                {
                    Properties.Settings.Default.hubIP = IPtextBox.Text;
                    MF.callback = new CallbackHandler(Properties.Settings.Default.hubIP);
                    warningLabel.Hide();
                }
                else
                {
                    warningLabel.Show();
                }
            });
        }
    }
}

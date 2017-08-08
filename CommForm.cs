using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GsmComm.PduConverter;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter.SmartMessaging;

namespace SmsModemClient
{
    public partial class CommForm : Form
    {

        SmsModemBlock2 Phone;
        SmsModemBlock Comm;

        int i = 1;

        /// <summary>
        /// Создает новую форму
        /// </summary>
        /// <param name="portname">Имя порта</param>
        public CommForm(SmsModemBlock2 phone)
        {
            Phone = phone;
            Comm = new SmsModemBlock(phone.PortName, phone.BaudRate);

            InitializeComponent();
            this.Text = phone.PortName;
            this.Name = "CommForm" + phone.PortName;

            if (!Comm.IsOpen()) Comm.Open();
            //Comm.EnableMessageNotifications();
            //Comm.MessageReceived += new MessageReceivedEventHandler(comm_MessageReceived);
        }

        

        private void comm_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            IMessageIndicationObject obj = e.IndicationObject;
            MemoryLocation loc = (MemoryLocation)obj;
            DecodedShortMessage[] messages;
            messages = Comm.ReadMessages(PhoneMessageStatus.ReceivedUnread, loc.Storage);

            foreach (DecodedShortMessage message in messages)
            {
                SmsDeliverPdu data = new SmsDeliverPdu();

                SmsPdu smsrec = message.Data;
            }
        }

        private void getSMSListButton_Click(object sender, EventArgs e)
        {
            i = 1;
            GetSMSList();
        }

        /// <summary>
        /// Заполняет таблицу входящими смс
        /// </summary>
        private void GetSMSList()
        {
            SMSList.Rows.Clear();
            // отключаем кнопку
            getSMSListButton.Enabled = false;
            var temp = getSMSListButton.Text;
            getSMSListButton.Text = "Зарузка...";

            LoadSmsInbox();

            getSMSListButton.Text = temp;
            getSMSListButton.Enabled = true;
        }

        /// <summary>
        /// Загружает список входящих сообщений
        /// </summary>
        private void LoadSmsInbox()
        {

            DecodedShortMessage[] messages = Comm.ReadMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);
            var msgs = Comm.ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

            // тут начинается пиздец
            IList<SmsPdu> longMsg = new List<SmsPdu>();

            foreach (DecodedShortMessage message in messages)
            {
                i = message.Index;
                SmsDeliverPdu SMSPDU = (SmsDeliverPdu)message.Data;
                
                bool isMultiPart = SmartMessageDecoder.IsPartOfConcatMessage(SMSPDU);

                // если у сообщения есть датахедер, то скорее всего оно длинное
                if (SMSPDU.UserDataHeaderPresent && isMultiPart)
                {
                    longMsg.Add(SMSPDU);
                    if (SmartMessageDecoder.AreAllConcatPartsPresent(longMsg)) // is Complete
                    {
                        var txt = SmartMessageDecoder.CombineConcatMessageText(longMsg);
                        // создаем сообщение
                        RecievedSMS longMessage = new RecievedSMS(i, "REC_READ", SMSPDU.OriginatingAddress, SMSPDU.SCTimestamp, txt);
                        // Показываем
                        DisplayMessage(longMessage);
                        // очищаем за собой
                        longMsg.Clear();
                    }
                }
                else
                {
                    DisplayMessage(message.Data);
                }
            }
        }

        /// <summary>
        /// Отображает сообщение
        /// </summary>
        /// <param name="pdu">Сообщение</param>
        private void DisplayMessage(SmsPdu pdu)
        {
            if (pdu is SmsDeliverPdu)
            {
                SmsDeliverPdu data = (SmsDeliverPdu)pdu;
                var phoneNumber = data.OriginatingAddress;
                var msg = data.UserDataText;
                var date = string.Format("{0:dd/MM/yyyy}", data.SCTimestamp.ToDateTime());
                var time = string.Format("{0:HH:mm:ss}", data.SCTimestamp.ToDateTime());
                var timestamp = data.SCTimestamp.ToDateTime();

                //read message in datagrid
                SMSList.Rows.Add(i, phoneNumber, timestamp, msg);
            }
        }

        /// <summary>
        /// Отображает сообщение
        /// </summary>
        /// <param name="LongSMS">Большое сообщение</param>
        private void DisplayMessage(RecievedSMS LongSMS)
        {
            var phoneNumber = LongSMS.Sender;
            var msg = LongSMS.Message;
            var date = string.Format("{0:dd/MM/yyyy}", LongSMS.Date);
            var time = string.Format("{0:HH:mm:ss}", LongSMS.Date);
            var timestamp = LongSMS.Date.ToString();

            //read message in datagrid
            SMSList.Rows.Add(i, phoneNumber, timestamp, msg);
        }

        //Выбор СМСки
        private void SMSList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewColumn &&
                e.RowIndex >= 0)
            {
                try
                {
                    SMSindex.Text = senderGrid.Rows[e.RowIndex].Cells["index"].Value.ToString();
                    SMSsender.Text = senderGrid.Rows[e.RowIndex].Cells["sender"].Value.ToString();
                    SMStime.Text = senderGrid.Rows[e.RowIndex].Cells["timeStamp"].Value.ToString();
                    SMStext.Text = senderGrid.Rows[e.RowIndex].Cells["text"].Value.ToString();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Ничего не выбрано!");
                }
            }
        }


        //закрываем за собой порт
        private void CommForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Comm.IsOpen()) Comm.Close();
        }
    }
}

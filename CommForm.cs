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

        //private void ShowMessage(SmsPdu pdu)
        //{// Received message
        //    SmsDeliverPdu data = (SmsDeliverPdu)pdu;
        //    string actualtext;
        //    actualtext = data.UserDataText;
        //    SetSomeText(actualtext);

        //    return;
        //}

        //private void SetSomeText(string text)
        //{
        //    SMStext.Text = text;
        //}

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

            //ждем входящие
            //var inbox = await LoadSmsInbox();
            LoadSmsInbox();
            // заполняем таблицу
            //FillSMSTable(inbox);

            getSMSListButton.Text = temp;
            getSMSListButton.Enabled = true;
        }

        private void LoadSmsInbox()
        {

            DecodedShortMessage[] messages = Comm.ReadMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);
            var msgs = Comm.ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);

            //не нужно пока
            foreach (ShortMessageFromPhone item in msgs)
            {
                SmsDeliverPdu pdu = new SmsDeliverPdu(item.Data,true,-1);
                //byte[] ucs = item.Data
                string r = PduParts.DecodeText(pdu.UserData, pdu.DataCodingScheme);
                //MessageBox.Show(pdu.UserDataText);
            }
            // тут начинается пиздец
            IList<SmsPdu> longMsg = new List<SmsPdu>();
            SmsDeliverPdu prevMsg = null;
            foreach (DecodedShortMessage message in messages)
            {
                SmsDeliverPdu SMSPDU;
                SMSPDU = (SmsDeliverPdu)message.Data;
                bool isMultiPart = SmartMessageDecoder.IsPartOfConcatMessage(SMSPDU);
                if (SMSPDU.UserDataHeaderPresent)
                {
                    InformationElement[] info = SmartMessageDecoder.DecodeUserDataHeader(SMSPDU.GetUserDataHeader());
                    int curr = ((ConcatMessageElement8)info[0]).CurrentNumber;
                    int tot = ((ConcatMessageElement8)info[0]).TotalMessages;
                    if (curr<tot)
                    {
                        longMsg.Add(SMSPDU);
                    }
                    else
                    {
                        longMsg.Add(SMSPDU);
                        var msg = SmartMessageDecoder.CombineConcatMessage(longMsg);
                        var txt = SmartMessageDecoder.CombineConcatMessageText(longMsg);
                        var str = BitConverter.ToString(msg);
                        str = str.Replace("-", "");
                        SmsDeliverPdu big = new SmsDeliverPdu(str, true, -1);
                    } 
                }
                #region govno
                //if (isMultiPart)
                //{
                //    if (prevMsg != null)
                //    {
                //        if (SmartMessageDecoder.ArePartOfSameMessage(SMSPDU, prevMsg))
                //        {
                //            longMsg.Add(prevMsg);
                //            prevMsg = SMSPDU;
                //        }
                //        else
                //        {
                //            longMsg.Add(prevMsg);
                //            var txt = SmartMessageDecoder.CombineConcatMessageText(longMsg);
                //            prevMsg = SMSPDU;
                //            longMsg.Clear();
                //        }
                //    }
                //    else
                //    {
                //        prevMsg = SMSPDU;
                //    }
                //}
                //if (longMsg.Count>0)
                //{
                //    longMsg.Add(prevMsg);
                //    var txt = SmartMessageDecoder.CombineConcatMessageText(longMsg);
                //    longMsg.Clear();
                //}
                #endregion

                i = message.Index;
                DisplayMessage(message.Data);
            }
        }

        private void DisplayMessage(SmsPdu pdu)
        {
            if (pdu is SmsDeliverPdu)
            {
                SmsDeliverPdu data = (SmsDeliverPdu)pdu;
                var phoneNumber = data.OriginatingAddress;
                var msg = data.UserDataText;
                var date = string.Format("{0:dd/MM/yyyy}", data.SCTimestamp.ToDateTime());
                var time = string.Format("{0:HH:mm:ss}", data.SCTimestamp.ToDateTime());

                //read message in datagrid
                SMSList.Rows.Add(i, phoneNumber, date +" "+ time, msg);
            }
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

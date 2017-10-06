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

        /// <summary>
        /// Наш модем
        /// </summary>
        public SmsModemBlock comm;
        /// <summary>
        /// Режим отображения сообщений
        /// </summary>
        enum MessageDisplayMode
        {
            Popup,
            Datagrid,
            Array
        }

        private int i = 1;        
        /// <summary>
        /// Входящие сообщения
        /// </summary>
        private ShortMessageFromPhone[] inbox;
        /// <summary>
        /// Коллекция объектов <see cref="SmsPdu"/>, являющихся частями длинного сообщения
        /// </summary>
        private IList<SmsPdu> longMsg;
        /// <summary>
        /// Предыдущиее сообщение
        /// </summary>
        private SmsDeliverPdu prevMsg;
        /// <summary>
        /// Текущий способ отображения сообщений
        /// </summary>
        private MessageDisplayMode DisplayMode = MessageDisplayMode.Popup;

        /// <summary>
        /// Создает новую форму
        /// </summary>
        /// <param name="portname">Имя порта</param>
        public CommForm(SmsModemBlock phone)
        {
            comm = phone;
            //Comm = new SmsModemBlock(phone.PortName, phone.BaudRate);

            InitializeComponent();
            this.Text = comm.PortName + " " + comm.TelNumber;
            this.Name = "CommForm" + comm.PortName;
            comm.NumberReceived += Comm_NumberRecieved;
        }

        private void Comm_NumberRecieved(object sender, EventArgs e)
        {
            UpdateName();
        }

        private void CommForm_Load(object sender, EventArgs e)
        {
            comm.MessageReceived += new MessageReceivedEventHandler(comm_MessageReceived);
            comm.LoglineAdded += new LoglineAddedEventHandler(Main_LoglineAdded);
        }

        /// <summary>
        /// Обновляет имя окна
        /// </summary>
        private void UpdateName()
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action(UpdateName));
            }
            this.Text = comm.PortName + " " + comm.TelNumber;
        }

        private void Main_LoglineAdded(object sender, LoglineAddedEventArgs e)
        {
            LogMessage("{0,-10}{1}", e.Level, e.Text);
        }

        private void comm_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var messages = comm.ReadRawMessages(PhoneMessageStatus.ReceivedUnread, PhoneStorageType.Sim);
            
            foreach (ShortMessageFromPhone message in messages)
            {
                SmsDeliverPdu data = new SmsDeliverPdu(message.Data, true, -1);
                bool isMultiPart = SmartMessageDecoder.IsPartOfConcatMessage(data);
                if (isMultiPart)
                {
                    var longMessage = DecodeLongMessage(data);
                    if (longMessage != null)
                    {
                        DisplayMessage(longMessage);
                    }
                }
                else
                {
                    DisplayMessage(data);
                }
            }

        }

        private void getSMSListButton_Click(object sender, EventArgs e)
        {
            GetSMSList();
        }

        /// <summary>
        /// Заполняет таблицу входящими смс
        /// </summary>
        private async void GetSMSList()
        {
            SMSList.Rows.Clear();
            // отключаем кнопку
            getSMSListButton.Enabled = false;
            var temp = getSMSListButton.Text;
            getSMSListButton.Text = "Зарузка...";
            DisplayMode = MessageDisplayMode.Datagrid;

            await LoadInbox();

            DisplayMode = MessageDisplayMode.Popup;
            getSMSListButton.Text = temp;
            getSMSListButton.Enabled = true;
        }

        public Task LoadInbox()
        {
            return Task.Factory.StartNew(LoadSmsInbox);
        }

        /// <summary>
        /// Загружает список входящих сообщений
        /// </summary>
        private void LoadSmsInbox()
        {
            var messages = comm.ReadRawMessages(PhoneMessageStatus.All, PhoneStorageType.Sim);
            inbox = messages;

            // тут начинается пиздец
            longMsg = new List<SmsPdu>();
            prevMsg = null;
            foreach (ShortMessageFromPhone message in messages)
            {
                i = message.Index;
                SmsDeliverPdu SMSPDU = new SmsDeliverPdu(message.Data, true, -1);
                
                bool isMultiPart = SmartMessageDecoder.IsPartOfConcatMessage(SMSPDU);

                // если у сообщения есть датахедер, то скорее всего оно длинное
                if (SMSPDU.UserDataHeaderPresent && isMultiPart)
                {
                    var longMessage = DecodeLongMessage(SMSPDU);
                    if (longMessage!=null)
                    {
                        // Показываем
                        DisplayMessage(longMessage);
                    }
                }
                else
                {
                    DisplayMessage(SMSPDU);
                }
            }
        }

        /// <summary>
        /// Обрабатывает длинные сообщения
        /// </summary>
        /// <param name="SMSPDU">Само сообщение</param>
        /// <param name="longMsg">Коллекция объектов <see cref="SmsPdu"/>, являющихся частями длинного сообщения</param>
        /// <param name="prevMsg">Предыдущее сообщение</param>
        public RecievedSMS DecodeLongMessage(SmsDeliverPdu SMSPDU)
        {
            if (longMsg.Count == 0 || SmartMessageDecoder.ArePartOfSameMessage(longMsg.Last(), SMSPDU))
                longMsg.Add(SMSPDU);
            else if (prevMsg != null && SmartMessageDecoder.ArePartOfSameMessage(SMSPDU, prevMsg))
            {
                longMsg.Clear();
                longMsg.Add(SMSPDU);
            }
            prevMsg = SMSPDU;
            if (SmartMessageDecoder.AreAllConcatPartsPresent(longMsg)) // is Complete
            {
                var txt = SmartMessageDecoder.CombineConcatMessageText(longMsg);
                // создаем сообщение
                RecievedSMS longMessage = new RecievedSMS(i, "REC_READ", SMSPDU.OriginatingAddress, SMSPDU.SCTimestamp, txt);
                // очищаем за собой
                longMsg.Clear();
                return longMessage;
            }
            return null;
        }

        private delegate void ShowMessageDelegate(object msg);

        /// <summary>
        /// Отображает сообщение
        /// </summary>
        /// <param name="pdu">Сообщение</param>
        /// <param name="mode">Режим отображения</param>
        private void DisplayMessage(object pdu)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowMessageDelegate(DisplayMessage), new object[] { pdu});
                return;
            }
            //задаем переменные
            int index = 0;
            string phoneNumber = string.Empty;
            string msg = string.Empty;
            string timestamp = string.Empty;
            //парсим обьект
            if (pdu is SmsDeliverPdu)
            {
                SmsDeliverPdu data = (SmsDeliverPdu)pdu;
                phoneNumber = data.OriginatingAddress;
                msg = data.UserDataText;
                timestamp = data.SCTimestamp.ToString();
                index = i;                
            }
            else if (pdu is RecievedSMS)
            {
                RecievedSMS LongSMS = (RecievedSMS)pdu;
                phoneNumber = LongSMS.Sender;
                msg = LongSMS.Message;
                timestamp = LongSMS.Date.ToString();
                index = LongSMS.Index;
            }
            else
            {
                return;
            }

            // отображаем смс
            switch (DisplayMode)
            {
                case MessageDisplayMode.Popup:
                    MessageBox.Show(string.Format("Получено новое сообщение от {0}: \r\n {1} \r\n {2}", phoneNumber, timestamp, msg));
                    break;
                case MessageDisplayMode.Datagrid:
                    //read message in datagrid
                    SMSList.Rows.Add(index, phoneNumber, timestamp, msg);
                    break;
                case MessageDisplayMode.Array:
                    break;
                default:
                    break;
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


        private void CommForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void deleteAllButton_Click(object sender, EventArgs e)
        {
            deleteAllButton.Enabled = false;
            switch (MessageBox.Show("Удалить все входящие?", "Вы уверены?", MessageBoxButtons.YesNo))
            {               
                case DialogResult.Yes:
                    DeleteAllInbox();
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
            deleteAllButton.Enabled = true;
        }

        /// <summary>
        /// Очищает входящие
        /// </summary>
        private void DeleteAllInbox()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(comm.ClearInbox));
            }
            else
            {
                comm.ClearInbox();
            }
        }

        private void getSignalLevel_Click(object sender, EventArgs e)
        {
            var signal = comm.GetSignalQuality();
            signalLevel.Text = signal.SignalStrength.ToString();

        }

        private void LogMessage(string loggedData)
        {
            Log(loggedData);
        }

        /// <summary>
        /// Пишет ответ в лог
        /// </summary>
        /// <param name="loggedData"></param>
        /// <param name="args"></param>
        private void LogMessage(string loggedData, params object[] args )
        {
            LogMessage(string.Format(loggedData, args));
        }

        private void Log(string text)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<string>(Log), new object[] { text });
            }
            else
            {
                log.AppendText(text);
                log.AppendText(Environment.NewLine);
            }
        }

        private async void sendButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(request.Text))
            {
                await Task.Run(() =>
                {
                    lock (comm)
                    {
                        try
                        {
                            comm.GetProtocol().ExecAndReceiveMultiple(request.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            System.Threading.Thread.Sleep(2000);
                            comm.ReleaseProtocol();
                        }
                    }
                });           
            }
        }

        private void request_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                sendButton_Click(sender, e);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                lock (comm)
                {
                    string temp;
                    try
                    {
                        comm.GetProtocol().Receive(out temp);
                        Log(temp);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        System.Threading.Thread.Sleep(2000);
                        comm.ReleaseProtocol();
                    }
                }
            });
        }
    }
}

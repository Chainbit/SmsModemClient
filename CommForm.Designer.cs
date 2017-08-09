namespace SmsModemClient
{
    partial class CommForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SMSListTab = new System.Windows.Forms.TabPage();
            this.deleteAllButton = new System.Windows.Forms.Button();
            this.getSignalLevel = new System.Windows.Forms.Button();
            this.SMStext = new System.Windows.Forms.TextBox();
            this.SMStime = new System.Windows.Forms.TextBox();
            this.SMSindex = new System.Windows.Forms.TextBox();
            this.SMSsender = new System.Windows.Forms.TextBox();
            this.SMSList = new System.Windows.Forms.DataGridView();
            this.index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeStamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.getSMSListButton = new System.Windows.Forms.Button();
            this.commandsTab = new System.Windows.Forms.TabPage();
            this.getResponseButton = new System.Windows.Forms.Button();
            this.response = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.request = new System.Windows.Forms.TextBox();
            this.signalLevel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.SMSListTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SMSList)).BeginInit();
            this.commandsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SMSListTab);
            this.tabControl1.Controls.Add(this.commandsTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(636, 386);
            this.tabControl1.TabIndex = 11;
            // 
            // SMSListTab
            // 
            this.SMSListTab.Controls.Add(this.signalLevel);
            this.SMSListTab.Controls.Add(this.deleteAllButton);
            this.SMSListTab.Controls.Add(this.getSignalLevel);
            this.SMSListTab.Controls.Add(this.SMStext);
            this.SMSListTab.Controls.Add(this.SMStime);
            this.SMSListTab.Controls.Add(this.SMSindex);
            this.SMSListTab.Controls.Add(this.SMSsender);
            this.SMSListTab.Controls.Add(this.SMSList);
            this.SMSListTab.Controls.Add(this.getSMSListButton);
            this.SMSListTab.Location = new System.Drawing.Point(4, 22);
            this.SMSListTab.Name = "SMSListTab";
            this.SMSListTab.Padding = new System.Windows.Forms.Padding(3);
            this.SMSListTab.Size = new System.Drawing.Size(628, 360);
            this.SMSListTab.TabIndex = 1;
            this.SMSListTab.Text = "Получение СМС";
            this.SMSListTab.UseVisualStyleBackColor = true;
            // 
            // deleteAllButton
            // 
            this.deleteAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteAllButton.Location = new System.Drawing.Point(524, 329);
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(93, 23);
            this.deleteAllButton.TabIndex = 6;
            this.deleteAllButton.Text = "Удалить все";
            this.deleteAllButton.UseVisualStyleBackColor = true;
            this.deleteAllButton.Click += new System.EventHandler(this.deleteAllButton_Click);
            // 
            // getSignalLevel
            // 
            this.getSignalLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getSignalLevel.Location = new System.Drawing.Point(190, 329);
            this.getSignalLevel.Name = "getSignalLevel";
            this.getSignalLevel.Size = new System.Drawing.Size(103, 23);
            this.getSignalLevel.TabIndex = 6;
            this.getSignalLevel.Text = "Уровень сигнала";
            this.getSignalLevel.UseVisualStyleBackColor = true;
            this.getSignalLevel.Click += new System.EventHandler(this.getSignalLevel_Click);
            // 
            // SMStext
            // 
            this.SMStext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SMStext.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.SMStext.Location = new System.Drawing.Point(190, 32);
            this.SMStext.Multiline = true;
            this.SMStext.Name = "SMStext";
            this.SMStext.ReadOnly = true;
            this.SMStext.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SMStext.Size = new System.Drawing.Size(427, 291);
            this.SMStext.TabIndex = 4;
            // 
            // SMStime
            // 
            this.SMStime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SMStime.Location = new System.Drawing.Point(389, 7);
            this.SMStime.Name = "SMStime";
            this.SMStime.ReadOnly = true;
            this.SMStime.Size = new System.Drawing.Size(228, 20);
            this.SMStime.TabIndex = 3;
            // 
            // SMSindex
            // 
            this.SMSindex.Location = new System.Drawing.Point(190, 7);
            this.SMSindex.Name = "SMSindex";
            this.SMSindex.ReadOnly = true;
            this.SMSindex.Size = new System.Drawing.Size(23, 20);
            this.SMSindex.TabIndex = 2;
            // 
            // SMSsender
            // 
            this.SMSsender.Location = new System.Drawing.Point(219, 7);
            this.SMSsender.Name = "SMSsender";
            this.SMSsender.ReadOnly = true;
            this.SMSsender.Size = new System.Drawing.Size(164, 20);
            this.SMSsender.TabIndex = 2;
            // 
            // SMSList
            // 
            this.SMSList.AllowUserToAddRows = false;
            this.SMSList.AllowUserToDeleteRows = false;
            this.SMSList.AllowUserToResizeRows = false;
            this.SMSList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SMSList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.index,
            this.sender,
            this.timeStamp,
            this.text});
            this.SMSList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.SMSList.Location = new System.Drawing.Point(8, 6);
            this.SMSList.MultiSelect = false;
            this.SMSList.Name = "SMSList";
            this.SMSList.ReadOnly = true;
            this.SMSList.RowHeadersVisible = false;
            this.SMSList.Size = new System.Drawing.Size(175, 317);
            this.SMSList.TabIndex = 1;
            this.SMSList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SMSList_CellContentClick);
            // 
            // index
            // 
            this.index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.index.FillWeight = 30F;
            this.index.HeaderText = "№";
            this.index.Name = "index";
            this.index.ReadOnly = true;
            // 
            // sender
            // 
            this.sender.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sender.HeaderText = "Отправитель";
            this.sender.Name = "sender";
            this.sender.ReadOnly = true;
            // 
            // timeStamp
            // 
            this.timeStamp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.timeStamp.FillWeight = 70F;
            this.timeStamp.HeaderText = "Время";
            this.timeStamp.Name = "timeStamp";
            this.timeStamp.ReadOnly = true;
            // 
            // text
            // 
            this.text.HeaderText = "Текст";
            this.text.Name = "text";
            this.text.ReadOnly = true;
            this.text.Visible = false;
            // 
            // getSMSListButton
            // 
            this.getSMSListButton.Location = new System.Drawing.Point(51, 329);
            this.getSMSListButton.Name = "getSMSListButton";
            this.getSMSListButton.Size = new System.Drawing.Size(132, 23);
            this.getSMSListButton.TabIndex = 0;
            this.getSMSListButton.Text = "Получить список СМС";
            this.getSMSListButton.UseVisualStyleBackColor = true;
            this.getSMSListButton.Click += new System.EventHandler(this.getSMSListButton_Click);
            // 
            // commandsTab
            // 
            this.commandsTab.Controls.Add(this.getResponseButton);
            this.commandsTab.Controls.Add(this.response);
            this.commandsTab.Controls.Add(this.sendButton);
            this.commandsTab.Controls.Add(this.request);
            this.commandsTab.Location = new System.Drawing.Point(4, 22);
            this.commandsTab.Name = "commandsTab";
            this.commandsTab.Padding = new System.Windows.Forms.Padding(3);
            this.commandsTab.Size = new System.Drawing.Size(628, 360);
            this.commandsTab.TabIndex = 0;
            this.commandsTab.Text = "Команды";
            this.commandsTab.UseVisualStyleBackColor = true;
            // 
            // getResponseButton
            // 
            this.getResponseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.getResponseButton.Location = new System.Drawing.Point(508, 6);
            this.getResponseButton.Name = "getResponseButton";
            this.getResponseButton.Size = new System.Drawing.Size(112, 33);
            this.getResponseButton.TabIndex = 8;
            this.getResponseButton.Text = "Прочитать вручную";
            this.getResponseButton.UseVisualStyleBackColor = true;
            // 
            // response
            // 
            this.response.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.response.Location = new System.Drawing.Point(6, 48);
            this.response.Multiline = true;
            this.response.Name = "response";
            this.response.ReadOnly = true;
            this.response.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.response.Size = new System.Drawing.Size(616, 306);
            this.response.TabIndex = 7;
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(431, 6);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(71, 33);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "Отправить";
            this.sendButton.UseVisualStyleBackColor = true;
            // 
            // request
            // 
            this.request.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.request.Location = new System.Drawing.Point(8, 13);
            this.request.Name = "request";
            this.request.Size = new System.Drawing.Size(417, 20);
            this.request.TabIndex = 5;
            // 
            // signalLevel
            // 
            this.signalLevel.AutoSize = true;
            this.signalLevel.Location = new System.Drawing.Point(299, 334);
            this.signalLevel.Name = "signalLevel";
            this.signalLevel.Size = new System.Drawing.Size(16, 13);
            this.signalLevel.TabIndex = 7;
            this.signalLevel.Text = "...";
            // 
            // CommForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 386);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CommForm";
            this.Text = "CommForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CommForm_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.SMSListTab.ResumeLayout(false);
            this.SMSListTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SMSList)).EndInit();
            this.commandsTab.ResumeLayout(false);
            this.commandsTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage commandsTab;
        private System.Windows.Forms.Button getResponseButton;
        public System.Windows.Forms.TextBox response;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox request;
        private System.Windows.Forms.TabPage SMSListTab;
        private System.Windows.Forms.Button deleteAllButton;
        private System.Windows.Forms.Button getSignalLevel;
        private System.Windows.Forms.TextBox SMStext;
        private System.Windows.Forms.TextBox SMStime;
        private System.Windows.Forms.TextBox SMSindex;
        private System.Windows.Forms.TextBox SMSsender;
        private System.Windows.Forms.DataGridView SMSList;
        private System.Windows.Forms.DataGridViewTextBoxColumn index;
        private System.Windows.Forms.DataGridViewTextBoxColumn sender;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeStamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn text;
        private System.Windows.Forms.Button getSMSListButton;
        private System.Windows.Forms.Label signalLevel;
    }
}
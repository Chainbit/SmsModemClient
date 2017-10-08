namespace SmsModemClient
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.loadComsButton = new System.Windows.Forms.Button();
            this.ComPortsDataGrid = new System.Windows.Forms.DataGridView();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComPortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SimID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TelNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIMoperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Signal = new System.Windows.Forms.DataGridViewImageColumn();
            this.SignalTxt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewButtonColumn();
            this.toggleAutoUpdateBtn = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.connectPicture = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.connectButon = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.actionGroupBox = new System.Windows.Forms.GroupBox();
            this.callRadio = new System.Windows.Forms.RadioButton();
            this.ussdRadio = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.callButton = new System.Windows.Forms.Button();
            this.numberTextBox = new System.Windows.Forms.TextBox();
            this.copyButton = new System.Windows.Forms.Button();
            this.autoconnectCheckBox = new System.Windows.Forms.CheckBox();
            this.IPtextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ComPortsDataGrid)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.connectPicture)).BeginInit();
            this.panel3.SuspendLayout();
            this.actionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadComsButton
            // 
            this.loadComsButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.loadComsButton.Enabled = false;
            this.loadComsButton.Location = new System.Drawing.Point(0, 0);
            this.loadComsButton.Name = "loadComsButton";
            this.loadComsButton.Size = new System.Drawing.Size(323, 36);
            this.loadComsButton.TabIndex = 2;
            this.loadComsButton.Text = "Загрузить список портов";
            this.loadComsButton.UseVisualStyleBackColor = true;
            this.loadComsButton.Click += new System.EventHandler(this.loadComsButton_Click);
            // 
            // ComPortsDataGrid
            // 
            this.ComPortsDataGrid.AllowUserToAddRows = false;
            this.ComPortsDataGrid.AllowUserToDeleteRows = false;
            this.ComPortsDataGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ComPortsDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ComPortsDataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.ComPortsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ComPortsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.ComPortName,
            this.SimID,
            this.TelNumber,
            this.Balance,
            this.SIMoperator,
            this.Signal,
            this.SignalTxt,
            this.Select});
            this.ComPortsDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ComPortsDataGrid.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.ComPortsDataGrid.Location = new System.Drawing.Point(0, 104);
            this.ComPortsDataGrid.MultiSelect = false;
            this.ComPortsDataGrid.Name = "ComPortsDataGrid";
            this.ComPortsDataGrid.ReadOnly = true;
            this.ComPortsDataGrid.RowHeadersVisible = false;
            this.ComPortsDataGrid.ShowEditingIcon = false;
            this.ComPortsDataGrid.Size = new System.Drawing.Size(677, 321);
            this.ComPortsDataGrid.TabIndex = 3;
            this.ComPortsDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ComPortsDataGrid_CellContentClick);
            this.ComPortsDataGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ComPortsDataGrid_CellContentDoubleClick);
            // 
            // number
            // 
            this.number.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.number.FillWeight = 10F;
            this.number.HeaderText = "№";
            this.number.Name = "number";
            this.number.ReadOnly = true;
            this.number.Width = 43;
            // 
            // ComPortName
            // 
            this.ComPortName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.ComPortName.HeaderText = "ComPortName";
            this.ComPortName.Name = "ComPortName";
            this.ComPortName.ReadOnly = true;
            // 
            // SimID
            // 
            this.SimID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SimID.FillWeight = 70F;
            this.SimID.HeaderText = "SIM ID";
            this.SimID.Name = "SimID";
            this.SimID.ReadOnly = true;
            // 
            // TelNumber
            // 
            this.TelNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle1.NullValue = "Загрузка...";
            this.TelNumber.DefaultCellStyle = dataGridViewCellStyle1;
            this.TelNumber.HeaderText = "TelNumber";
            this.TelNumber.Name = "TelNumber";
            this.TelNumber.ReadOnly = true;
            this.TelNumber.Width = 84;
            // 
            // Balance
            // 
            this.Balance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
            this.Balance.FillWeight = 40F;
            this.Balance.HeaderText = "Balance";
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            this.Balance.Width = 5;
            // 
            // SIMoperator
            // 
            this.SIMoperator.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SIMoperator.HeaderText = "Operator";
            this.SIMoperator.Name = "SIMoperator";
            this.SIMoperator.ReadOnly = true;
            this.SIMoperator.Width = 73;
            // 
            // Signal
            // 
            this.Signal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Signal.FillWeight = 10F;
            this.Signal.HeaderText = "Signal";
            this.Signal.Name = "Signal";
            this.Signal.ReadOnly = true;
            this.Signal.Width = 42;
            // 
            // SignalTxt
            // 
            this.SignalTxt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.SignalTxt.HeaderText = "SignalTxt";
            this.SignalTxt.Name = "SignalTxt";
            this.SignalTxt.ReadOnly = true;
            this.SignalTxt.Width = 76;
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Select.FillWeight = 20F;
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Text = "Select";
            this.Select.UseColumnTextForButtonValue = true;
            // 
            // toggleAutoUpdateBtn
            // 
            this.toggleAutoUpdateBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toggleAutoUpdateBtn.Location = new System.Drawing.Point(323, 0);
            this.toggleAutoUpdateBtn.Name = "toggleAutoUpdateBtn";
            this.toggleAutoUpdateBtn.Size = new System.Drawing.Size(206, 36);
            this.toggleAutoUpdateBtn.TabIndex = 6;
            this.toggleAutoUpdateBtn.Text = "Автообновление: ";
            this.toggleAutoUpdateBtn.UseVisualStyleBackColor = true;
            this.toggleAutoUpdateBtn.Click += new System.EventHandler(this.toggleAutoUpdateBtn_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.refreshButton.Location = new System.Drawing.Point(529, 0);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(148, 36);
            this.refreshButton.TabIndex = 7;
            this.refreshButton.Text = "Обновить";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toggleAutoUpdateBtn);
            this.panel1.Controls.Add(this.refreshButton);
            this.panel1.Controls.Add(this.loadComsButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 425);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(677, 36);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.autoconnectCheckBox);
            this.panel2.Controls.Add(this.connectPicture);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.disconnectButton);
            this.panel2.Controls.Add(this.connectButon);
            this.panel2.Controls.Add(this.IPtextBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(677, 44);
            this.panel2.TabIndex = 9;
            // 
            // connectPicture
            // 
            this.connectPicture.Image = global::SmsModemClient.Properties.Resources.disconnected;
            this.connectPicture.ImageLocation = "";
            this.connectPicture.InitialImage = global::SmsModemClient.Properties.Resources.disconnected;
            this.connectPicture.Location = new System.Drawing.Point(60, 6);
            this.connectPicture.Name = "connectPicture";
            this.connectPicture.Size = new System.Drawing.Size(31, 30);
            this.connectPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.connectPicture.TabIndex = 10;
            this.connectPicture.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Сервер:";
            // 
            // disconnectButton
            // 
            this.disconnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(390, 10);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(96, 23);
            this.disconnectButton.TabIndex = 3;
            this.disconnectButton.Text = "Отключиться";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // connectButon
            // 
            this.connectButon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectButon.Location = new System.Drawing.Point(284, 10);
            this.connectButon.Name = "connectButon";
            this.connectButon.Size = new System.Drawing.Size(101, 23);
            this.connectButon.TabIndex = 2;
            this.connectButon.Text = "Подключиться";
            this.connectButon.UseVisualStyleBackColor = true;
            this.connectButon.Click += new System.EventHandler(this.connectButon_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.actionGroupBox);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.callButton);
            this.panel3.Controls.Add(this.numberTextBox);
            this.panel3.Controls.Add(this.copyButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 44);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(677, 56);
            this.panel3.TabIndex = 10;
            // 
            // actionGroupBox
            // 
            this.actionGroupBox.Controls.Add(this.callRadio);
            this.actionGroupBox.Controls.Add(this.ussdRadio);
            this.actionGroupBox.Location = new System.Drawing.Point(291, 3);
            this.actionGroupBox.Name = "actionGroupBox";
            this.actionGroupBox.Size = new System.Drawing.Size(121, 50);
            this.actionGroupBox.TabIndex = 6;
            this.actionGroupBox.TabStop = false;
            this.actionGroupBox.Text = "Действие";
            // 
            // callRadio
            // 
            this.callRadio.AutoSize = true;
            this.callRadio.Location = new System.Drawing.Point(6, 28);
            this.callRadio.Name = "callRadio";
            this.callRadio.Size = new System.Drawing.Size(62, 17);
            this.callRadio.TabIndex = 5;
            this.callRadio.Text = "Звонок";
            this.callRadio.UseVisualStyleBackColor = true;
            // 
            // ussdRadio
            // 
            this.ussdRadio.AutoSize = true;
            this.ussdRadio.Checked = true;
            this.ussdRadio.Location = new System.Drawing.Point(6, 12);
            this.ussdRadio.Name = "ussdRadio";
            this.ussdRadio.Size = new System.Drawing.Size(100, 17);
            this.ussdRadio.TabIndex = 4;
            this.ussdRadio.TabStop = true;
            this.ussdRadio.Text = "USSD - запрос";
            this.ussdRadio.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Enabled = false;
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(419, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Номер телефона или запрос:";
            // 
            // callButton
            // 
            this.callButton.BackColor = System.Drawing.Color.Transparent;
            this.callButton.BackgroundImage = global::SmsModemClient.Properties.Resources.call;
            this.callButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.callButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.callButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.callButton.ForeColor = System.Drawing.SystemColors.Control;
            this.callButton.Location = new System.Drawing.Point(629, 0);
            this.callButton.Name = "callButton";
            this.callButton.Size = new System.Drawing.Size(48, 56);
            this.callButton.TabIndex = 2;
            this.callButton.UseVisualStyleBackColor = false;
            this.callButton.Click += new System.EventHandler(this.callButton_Click);
            // 
            // numberTextBox
            // 
            this.numberTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numberTextBox.Location = new System.Drawing.Point(420, 22);
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.Size = new System.Drawing.Size(197, 20);
            this.numberTextBox.TabIndex = 1;
            // 
            // copyButton
            // 
            this.copyButton.Location = new System.Drawing.Point(3, 9);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(88, 39);
            this.copyButton.TabIndex = 0;
            this.copyButton.Text = "Копировать номера";
            this.copyButton.UseVisualStyleBackColor = true;
            this.copyButton.Click += new System.EventHandler(this.copyButton_Click);
            // 
            // autoconnectCheckBox
            // 
            this.autoconnectCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.autoconnectCheckBox.AutoSize = true;
            this.autoconnectCheckBox.Checked = global::SmsModemClient.Properties.Settings.Default.autoconnect;
            this.autoconnectCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SmsModemClient.Properties.Settings.Default, "autoconnect", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.autoconnectCheckBox.Location = new System.Drawing.Point(491, 12);
            this.autoconnectCheckBox.Name = "autoconnectCheckBox";
            this.autoconnectCheckBox.Size = new System.Drawing.Size(180, 17);
            this.autoconnectCheckBox.TabIndex = 11;
            this.autoconnectCheckBox.Text = "Подключаться автоматически";
            this.autoconnectCheckBox.UseVisualStyleBackColor = true;
            this.autoconnectCheckBox.CheckedChanged += new System.EventHandler(this.autoconnectCheckBox_CheckedChanged);
            // 
            // IPtextBox
            // 
            this.IPtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IPtextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.IPtextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SmsModemClient.Properties.Settings.Default, "hubIP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.IPtextBox.Location = new System.Drawing.Point(101, 11);
            this.IPtextBox.Name = "IPtextBox";
            this.IPtextBox.Size = new System.Drawing.Size(178, 20);
            this.IPtextBox.TabIndex = 1;
            this.IPtextBox.Text = global::SmsModemClient.Properties.Settings.Default.hubIP;
            this.IPtextBox.TextChanged += new System.EventHandler(this.IPtextBox_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 461);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ComPortsDataGrid);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SmsModemClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ComPortsDataGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.connectPicture)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.actionGroupBox.ResumeLayout(false);
            this.actionGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button loadComsButton;
        private System.Windows.Forms.DataGridView ComPortsDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn iCCIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn portOpenDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button toggleAutoUpdateBtn;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button connectButon;
        private System.Windows.Forms.TextBox IPtextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox connectPicture;
        private System.Windows.Forms.CheckBox autoconnectCheckBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComPortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SimID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TelNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIMoperator;
        private System.Windows.Forms.DataGridViewImageColumn Signal;
        private System.Windows.Forms.DataGridViewTextBoxColumn SignalTxt;
        private System.Windows.Forms.DataGridViewButtonColumn Select;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button copyButton;
        private System.Windows.Forms.Button callButton;
        private System.Windows.Forms.TextBox numberTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton callRadio;
        private System.Windows.Forms.RadioButton ussdRadio;
        private System.Windows.Forms.GroupBox actionGroupBox;
    }
}


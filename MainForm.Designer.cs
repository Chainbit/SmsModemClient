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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.loadComsButton = new System.Windows.Forms.Button();
            this.ComPortsDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.toggleAutoUpdateBtn = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.portNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iCCIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.telNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operatorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.logLevelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baudRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeoutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connectionCheckDelayDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.smsModemBlock2BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComPortName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SimID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TelNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIMoperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Signal = new System.Windows.Forms.DataGridViewImageColumn();
            this.isOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ComPortsDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.smsModemBlock2BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // loadComsButton
            // 
            this.loadComsButton.Enabled = false;
            this.loadComsButton.Location = new System.Drawing.Point(0, 359);
            this.loadComsButton.Name = "loadComsButton";
            this.loadComsButton.Size = new System.Drawing.Size(161, 31);
            this.loadComsButton.TabIndex = 2;
            this.loadComsButton.Text = "Обновить список портов";
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
            this.SIMoperator,
            this.Signal,
            this.isOpen,
            this.Select});
            this.ComPortsDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ComPortsDataGrid.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.ComPortsDataGrid.Location = new System.Drawing.Point(0, -1);
            this.ComPortsDataGrid.MultiSelect = false;
            this.ComPortsDataGrid.Name = "ComPortsDataGrid";
            this.ComPortsDataGrid.ReadOnly = true;
            this.ComPortsDataGrid.RowHeadersVisible = false;
            this.ComPortsDataGrid.ShowEditingIcon = false;
            this.ComPortsDataGrid.Size = new System.Drawing.Size(639, 354);
            this.ComPortsDataGrid.TabIndex = 3;
            this.ComPortsDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ComPortsDataGrid_CellContentClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.portNameDataGridViewTextBoxColumn,
            this.iCCIDDataGridViewTextBoxColumn,
            this.telNumberDataGridViewTextBoxColumn,
            this.operatorDataGridViewTextBoxColumn,
            this.logLevelDataGridViewTextBoxColumn,
            this.baudRateDataGridViewTextBoxColumn,
            this.timeoutDataGridViewTextBoxColumn,
            this.connectionCheckDelayDataGridViewTextBoxColumn,
            this.dataGridViewButtonColumn1});
            this.dataGridView1.DataSource = this.smsModemBlock2BindingSource;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dataGridView1.Location = new System.Drawing.Point(0, 161);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(639, 192);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.Visible = false;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewButtonColumn1.FillWeight = 20F;
            this.dataGridViewButtonColumn1.HeaderText = "Select";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.ReadOnly = true;
            this.dataGridViewButtonColumn1.Text = "Select";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn1.Width = 43;
            // 
            // toggleAutoUpdateBtn
            // 
            this.toggleAutoUpdateBtn.Location = new System.Drawing.Point(167, 359);
            this.toggleAutoUpdateBtn.Name = "toggleAutoUpdateBtn";
            this.toggleAutoUpdateBtn.Size = new System.Drawing.Size(151, 31);
            this.toggleAutoUpdateBtn.TabIndex = 6;
            this.toggleAutoUpdateBtn.Text = "Автообновление: ";
            this.toggleAutoUpdateBtn.UseVisualStyleBackColor = true;
            this.toggleAutoUpdateBtn.Click += new System.EventHandler(this.toggleAutoUpdateBtn_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(325, 359);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(148, 31);
            this.refreshButton.TabIndex = 7;
            this.refreshButton.Text = "Обновить";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // portNameDataGridViewTextBoxColumn
            // 
            this.portNameDataGridViewTextBoxColumn.DataPropertyName = "PortName";
            this.portNameDataGridViewTextBoxColumn.HeaderText = "PortName";
            this.portNameDataGridViewTextBoxColumn.Name = "portNameDataGridViewTextBoxColumn";
            this.portNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // iCCIDDataGridViewTextBoxColumn
            // 
            this.iCCIDDataGridViewTextBoxColumn.DataPropertyName = "ICCID";
            this.iCCIDDataGridViewTextBoxColumn.HeaderText = "ICCID";
            this.iCCIDDataGridViewTextBoxColumn.Name = "iCCIDDataGridViewTextBoxColumn";
            this.iCCIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // telNumberDataGridViewTextBoxColumn
            // 
            this.telNumberDataGridViewTextBoxColumn.DataPropertyName = "TelNumber";
            this.telNumberDataGridViewTextBoxColumn.HeaderText = "TelNumber";
            this.telNumberDataGridViewTextBoxColumn.Name = "telNumberDataGridViewTextBoxColumn";
            this.telNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // operatorDataGridViewTextBoxColumn
            // 
            this.operatorDataGridViewTextBoxColumn.DataPropertyName = "Operator";
            this.operatorDataGridViewTextBoxColumn.HeaderText = "Operator";
            this.operatorDataGridViewTextBoxColumn.Name = "operatorDataGridViewTextBoxColumn";
            this.operatorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // logLevelDataGridViewTextBoxColumn
            // 
            this.logLevelDataGridViewTextBoxColumn.DataPropertyName = "LogLevel";
            this.logLevelDataGridViewTextBoxColumn.HeaderText = "LogLevel";
            this.logLevelDataGridViewTextBoxColumn.Name = "logLevelDataGridViewTextBoxColumn";
            this.logLevelDataGridViewTextBoxColumn.ReadOnly = true;
            this.logLevelDataGridViewTextBoxColumn.Visible = false;
            // 
            // baudRateDataGridViewTextBoxColumn
            // 
            this.baudRateDataGridViewTextBoxColumn.DataPropertyName = "BaudRate";
            this.baudRateDataGridViewTextBoxColumn.HeaderText = "BaudRate";
            this.baudRateDataGridViewTextBoxColumn.Name = "baudRateDataGridViewTextBoxColumn";
            this.baudRateDataGridViewTextBoxColumn.ReadOnly = true;
            this.baudRateDataGridViewTextBoxColumn.Visible = false;
            // 
            // timeoutDataGridViewTextBoxColumn
            // 
            this.timeoutDataGridViewTextBoxColumn.DataPropertyName = "Timeout";
            this.timeoutDataGridViewTextBoxColumn.HeaderText = "Timeout";
            this.timeoutDataGridViewTextBoxColumn.Name = "timeoutDataGridViewTextBoxColumn";
            this.timeoutDataGridViewTextBoxColumn.ReadOnly = true;
            this.timeoutDataGridViewTextBoxColumn.Visible = false;
            // 
            // connectionCheckDelayDataGridViewTextBoxColumn
            // 
            this.connectionCheckDelayDataGridViewTextBoxColumn.DataPropertyName = "ConnectionCheckDelay";
            this.connectionCheckDelayDataGridViewTextBoxColumn.HeaderText = "ConnectionCheckDelay";
            this.connectionCheckDelayDataGridViewTextBoxColumn.Name = "connectionCheckDelayDataGridViewTextBoxColumn";
            this.connectionCheckDelayDataGridViewTextBoxColumn.ReadOnly = true;
            this.connectionCheckDelayDataGridViewTextBoxColumn.Visible = false;
            // 
            // smsModemBlock2BindingSource
            // 
            this.smsModemBlock2BindingSource.DataSource = typeof(SmsModemClient.SmsModemBlock2);
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
            // isOpen
            // 
            this.isOpen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.isOpen.FillWeight = 20F;
            this.isOpen.HeaderText = "isOpen";
            this.isOpen.Name = "isOpen";
            this.isOpen.ReadOnly = true;
            this.isOpen.Width = 65;
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Select.FillWeight = 20F;
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.ReadOnly = true;
            this.Select.Text = "Select";
            this.Select.UseColumnTextForButtonValue = true;
            this.Select.Width = 43;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 390);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.toggleAutoUpdateBtn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ComPortsDataGrid);
            this.Controls.Add(this.loadComsButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SmsModemClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ComPortsDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.smsModemBlock2BindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button loadComsButton;
        private System.Windows.Forms.DataGridView ComPortsDataGrid;
        private System.Windows.Forms.BindingSource smsModemBlock2BindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn portNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn iCCIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn telNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn portOpenDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn logLevelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baudRateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeoutDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn connectionCheckDelayDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.Button toggleAutoUpdateBtn;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComPortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SimID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TelNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIMoperator;
        private System.Windows.Forms.DataGridViewImageColumn Signal;
        private System.Windows.Forms.DataGridViewTextBoxColumn isOpen;
        private System.Windows.Forms.DataGridViewButtonColumn Select;
    }
}


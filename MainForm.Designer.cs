﻿namespace SmsModemClient
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
            this.SIMoperator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isOpen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Select = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ToggleAllPortsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ComPortsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // loadComsButton
            // 
            this.loadComsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.loadComsButton.Enabled = false;
            this.loadComsButton.Location = new System.Drawing.Point(0, 359);
            this.loadComsButton.Name = "loadComsButton";
            this.loadComsButton.Size = new System.Drawing.Size(603, 31);
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
            this.ComPortsDataGrid.Size = new System.Drawing.Size(603, 354);
            this.ComPortsDataGrid.TabIndex = 3;
            this.ComPortsDataGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ComPortsDataGrid_CellContentClick);
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
            // ToggleAllPortsButton
            // 
            this.ToggleAllPortsButton.Enabled = false;
            this.ToggleAllPortsButton.Location = new System.Drawing.Point(512, 359);
            this.ToggleAllPortsButton.Name = "ToggleAllPortsButton";
            this.ToggleAllPortsButton.Size = new System.Drawing.Size(91, 30);
            this.ToggleAllPortsButton.TabIndex = 4;
            this.ToggleAllPortsButton.Text = "Открыть все";
            this.ToggleAllPortsButton.UseVisualStyleBackColor = true;
            this.ToggleAllPortsButton.Visible = false;
            this.ToggleAllPortsButton.Click += new System.EventHandler(this.ToggleAllPortsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 390);
            this.Controls.Add(this.ToggleAllPortsButton);
            this.Controls.Add(this.ComPortsDataGrid);
            this.Controls.Add(this.loadComsButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "SmsModemClient";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ComPortsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button loadComsButton;
        private System.Windows.Forms.DataGridView ComPortsDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn ComPortName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SimID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TelNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIMoperator;
        private System.Windows.Forms.DataGridViewTextBoxColumn isOpen;
        private System.Windows.Forms.DataGridViewButtonColumn Select;
        private System.Windows.Forms.Button ToggleAllPortsButton;
    }
}


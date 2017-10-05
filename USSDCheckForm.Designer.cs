namespace SmsModemClient
{
    partial class USSDCheckForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TelNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TaskCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TelNumber,
            this.TaskCol,
            this.Result});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(412, 321);
            this.dataGridView1.TabIndex = 0;
            // 
            // TelNumber
            // 
            this.TelNumber.HeaderText = "Номер телефона";
            this.TelNumber.Name = "TelNumber";
            this.TelNumber.ReadOnly = true;
            // 
            // TaskCol
            // 
            this.TaskCol.HeaderText = "Задача";
            this.TaskCol.Name = "TaskCol";
            this.TaskCol.ReadOnly = true;
            // 
            // Result
            // 
            this.Result.HeaderText = "Результат";
            this.Result.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            // 
            // USSDCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 321);
            this.Controls.Add(this.dataGridView1);
            this.Name = "USSDCheckForm";
            this.Text = "USSDCheckForm";
            this.Load += new System.EventHandler(this.USSDCheckForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TelNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TaskCol;
        private System.Windows.Forms.DataGridViewImageColumn Result;
    }
}
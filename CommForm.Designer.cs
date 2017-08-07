﻿namespace SmsModemClient
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
            this.commandsTab = new System.Windows.Forms.TabPage();
            this.getResponseButton = new System.Windows.Forms.Button();
            this.response = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.request = new System.Windows.Forms.TextBox();
            this.SMSListTab = new System.Windows.Forms.TabPage();
            this.deleteAllButton = new System.Windows.Forms.Button();
            this.deleteMessageButton = new System.Windows.Forms.Button();
            this.DecodeButton = new System.Windows.Forms.Button();
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
            this.tabControl1.SuspendLayout();
            this.commandsTab.SuspendLayout();
            this.SMSListTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SMSList)).BeginInit();
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
            // SMSListTab
            // 
            this.SMSListTab.Controls.Add(this.deleteAllButton);
            this.SMSListTab.Controls.Add(this.deleteMessageButton);
            this.SMSListTab.Controls.Add(this.DecodeButton);
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
            this.deleteAllButton.Location = new System.Drawing.Point(443, 329);
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(93, 23);
            this.deleteAllButton.TabIndex = 6;
            this.deleteAllButton.Text = "Удалить все";
            this.deleteAllButton.UseVisualStyleBackColor = true;
            // 
            // deleteMessageButton
            // 
            this.deleteMessageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteMessageButton.Location = new System.Drawing.Point(542, 329);
            this.deleteMessageButton.Name = "deleteMessageButton";
            this.deleteMessageButton.Size = new System.Drawing.Size(75, 23);
            this.deleteMessageButton.TabIndex = 6;
            this.deleteMessageButton.Text = "Удалить";
            this.deleteMessageButton.UseVisualStyleBackColor = true;
            // 
            // DecodeButton
            // 
            this.DecodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DecodeButton.Location = new System.Drawing.Point(542, 6);
            this.DecodeButton.Name = "DecodeButton";
            this.DecodeButton.Size = new System.Drawing.Size(75, 22);
            this.DecodeButton.TabIndex = 5;
            this.DecodeButton.Text = "Decode";
            this.DecodeButton.UseVisualStyleBackColor = true;
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
            this.SMStime.Location = new System.Drawing.Point(369, 7);
            this.SMStime.Name = "SMStime";
            this.SMStime.ReadOnly = true;
            this.SMStime.Size = new System.Drawing.Size(167, 20);
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
            this.SMSsender.Size = new System.Drawing.Size(144, 20);
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
            this.tabControl1.ResumeLayout(false);
            this.commandsTab.ResumeLayout(false);
            this.commandsTab.PerformLayout();
            this.SMSListTab.ResumeLayout(false);
            this.SMSListTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SMSList)).EndInit();
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
        private System.Windows.Forms.Button deleteMessageButton;
        private System.Windows.Forms.Button DecodeButton;
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
    }
}
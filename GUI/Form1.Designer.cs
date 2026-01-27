namespace GUI
{
    partial class GUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            chatWindow = new RichTextBox();
            messageBox = new TextBox();
            sendButton = new Button();
            usernameTextBox = new TextBox();
            connectOrDisconnectButton = new Button();
            ipaddressbox = new TextBox();
            portbox = new TextBox();
            navigationList = new ListBox();
            chatWindowTitle = new Label();
            dropPanel = new Panel();
            dropPanelTitle = new Label();
            clientsList = new ListBox();
            filesListToSend = new ListView();
            Filename = new ColumnHeader();
            Size = new ColumnHeader();
            label1 = new Label();
            filesOnTheServer = new ListView();
            Plik = new ColumnHeader();
            rozmiar = new ColumnHeader();
            contextMenuStrip1 = new ContextMenuStrip(components);
            downloadToolStripMenuItem = new ToolStripMenuItem();
            delateToolStripMenuItem = new ToolStripMenuItem();
            cancelToolStripMenuItem = new ToolStripMenuItem();
            label2 = new Label();
            uploadButton = new Button();
            clearFilesList = new Button();
            label3 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblValDisc = new Label();
            lblValFreq = new Label();
            lblValRam = new Label();
            lblValLoad = new Label();
            lblValTemp = new Label();
            label9 = new Label();
            lblValNet = new Label();
            label5 = new Label();
            label4 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            dropPanel.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // chatWindow
            // 
            chatWindow.BorderStyle = BorderStyle.FixedSingle;
            chatWindow.Font = new Font("Microsoft YaHei UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chatWindow.Location = new Point(917, 48);
            chatWindow.Name = "chatWindow";
            chatWindow.Size = new Size(616, 497);
            chatWindow.TabIndex = 0;
            chatWindow.Text = "";
            // 
            // messageBox
            // 
            messageBox.BorderStyle = BorderStyle.FixedSingle;
            messageBox.Font = new Font("Microsoft YaHei UI", 10.2F);
            messageBox.Location = new Point(1001, 551);
            messageBox.Multiline = true;
            messageBox.Name = "messageBox";
            messageBox.Size = new Size(532, 44);
            messageBox.TabIndex = 1;
            // 
            // sendButton
            // 
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.Font = new Font("Microsoft YaHei UI", 10.2F);
            sendButton.Location = new Point(917, 551);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(78, 44);
            sendButton.TabIndex = 2;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // usernameTextBox
            // 
            usernameTextBox.BorderStyle = BorderStyle.FixedSingle;
            usernameTextBox.Font = new Font("Microsoft YaHei UI Light", 10.2F);
            usernameTextBox.Location = new Point(3, 12);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(252, 30);
            usernameTextBox.TabIndex = 4;
            usernameTextBox.Text = "Username";
            // 
            // connectOrDisconnectButton
            // 
            connectOrDisconnectButton.FlatStyle = FlatStyle.Flat;
            connectOrDisconnectButton.Font = new Font("Microsoft YaHei UI", 10.2F);
            connectOrDisconnectButton.Location = new Point(3, 550);
            connectOrDisconnectButton.Name = "connectOrDisconnectButton";
            connectOrDisconnectButton.Size = new Size(252, 45);
            connectOrDisconnectButton.TabIndex = 5;
            connectOrDisconnectButton.Text = "Connect";
            connectOrDisconnectButton.UseVisualStyleBackColor = true;
            connectOrDisconnectButton.Click += connectOrDisconnectButton_Click;
            // 
            // ipaddressbox
            // 
            ipaddressbox.BorderStyle = BorderStyle.FixedSingle;
            ipaddressbox.Font = new Font("Microsoft YaHei UI Light", 10.2F);
            ipaddressbox.Location = new Point(3, 48);
            ipaddressbox.Name = "ipaddressbox";
            ipaddressbox.Size = new Size(252, 30);
            ipaddressbox.TabIndex = 9;
            ipaddressbox.Text = "IP Address";
            // 
            // portbox
            // 
            portbox.BorderStyle = BorderStyle.FixedSingle;
            portbox.Font = new Font("Microsoft YaHei UI Light", 10.2F);
            portbox.Location = new Point(3, 84);
            portbox.Name = "portbox";
            portbox.Size = new Size(252, 30);
            portbox.TabIndex = 10;
            portbox.Text = "Port";
            // 
            // navigationList
            // 
            navigationList.BorderStyle = BorderStyle.FixedSingle;
            navigationList.Font = new Font("Microsoft YaHei UI Light", 10.2F);
            navigationList.FormattingEnabled = true;
            navigationList.Items.AddRange(new object[] { "Broadcast", "Unicast" });
            navigationList.Location = new Point(3, 120);
            navigationList.Name = "navigationList";
            navigationList.Size = new Size(252, 48);
            navigationList.TabIndex = 11;
            navigationList.SelectedIndexChanged += navigationList_SelectedIndexChanged;
            // 
            // chatWindowTitle
            // 
            chatWindowTitle.AutoSize = true;
            chatWindowTitle.FlatStyle = FlatStyle.Flat;
            chatWindowTitle.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 238);
            chatWindowTitle.Location = new Point(917, 5);
            chatWindowTitle.Name = "chatWindowTitle";
            chatWindowTitle.Size = new Size(211, 40);
            chatWindowTitle.TabIndex = 12;
            chatWindowTitle.Text = "ChatWindow";
            // 
            // dropPanel
            // 
            dropPanel.AllowDrop = true;
            dropPanel.BackColor = Color.White;
            dropPanel.BorderStyle = BorderStyle.FixedSingle;
            dropPanel.Controls.Add(dropPanelTitle);
            dropPanel.Location = new Point(461, 550);
            dropPanel.Name = "dropPanel";
            dropPanel.Size = new Size(450, 45);
            dropPanel.TabIndex = 13;
            dropPanel.DragDrop += dropPanel_DragDrop;
            dropPanel.DragEnter += dropPanel_DragEnter;
            // 
            // dropPanelTitle
            // 
            dropPanelTitle.AutoSize = true;
            dropPanelTitle.Font = new Font("Microsoft YaHei UI Light", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            dropPanelTitle.Location = new Point(3, 18);
            dropPanelTitle.Name = "dropPanelTitle";
            dropPanelTitle.Size = new Size(144, 27);
            dropPanelTitle.TabIndex = 0;
            dropPanelTitle.Text = "Drop files here";
            // 
            // clientsList
            // 
            clientsList.BorderStyle = BorderStyle.FixedSingle;
            clientsList.Font = new Font("Microsoft YaHei UI Light", 10.2F);
            clientsList.FormattingEnabled = true;
            clientsList.Location = new Point(3, 174);
            clientsList.Name = "clientsList";
            clientsList.Size = new Size(252, 370);
            clientsList.TabIndex = 14;
            clientsList.SelectedIndexChanged += clientsList_SelectedIndexChanged;
            // 
            // filesListToSend
            // 
            filesListToSend.BorderStyle = BorderStyle.FixedSingle;
            filesListToSend.Columns.AddRange(new ColumnHeader[] { Filename, Size });
            filesListToSend.Font = new Font("Microsoft YaHei UI Light", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 238);
            filesListToSend.Location = new Point(261, 369);
            filesListToSend.Name = "filesListToSend";
            filesListToSend.Size = new Size(648, 175);
            filesListToSend.TabIndex = 15;
            filesListToSend.UseCompatibleStateImageBehavior = false;
            filesListToSend.View = View.Details;
            // 
            // Filename
            // 
            Filename.Text = "Filename";
            Filename.Width = 200;
            // 
            // Size
            // 
            Size.Text = "Size";
            Size.Width = 100;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.Location = new Point(261, 336);
            label1.Name = "label1";
            label1.Size = new Size(201, 27);
            label1.TabIndex = 16;
            label1.Text = "Send on the server";
            // 
            // filesOnTheServer
            // 
            filesOnTheServer.BorderStyle = BorderStyle.FixedSingle;
            filesOnTheServer.Columns.AddRange(new ColumnHeader[] { Plik, rozmiar });
            filesOnTheServer.ContextMenuStrip = contextMenuStrip1;
            filesOnTheServer.Font = new Font("Microsoft YaHei UI Light", 10.2F);
            filesOnTheServer.Location = new Point(261, 171);
            filesOnTheServer.Name = "filesOnTheServer";
            filesOnTheServer.Size = new Size(648, 162);
            filesOnTheServer.TabIndex = 17;
            filesOnTheServer.UseCompatibleStateImageBehavior = false;
            filesOnTheServer.View = View.Details;
            // 
            // Plik
            // 
            Plik.Text = "Filename";
            Plik.Width = 200;
            // 
            // rozmiar
            // 
            rozmiar.Text = "Size";
            rozmiar.Width = 100;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { downloadToolStripMenuItem, delateToolStripMenuItem, cancelToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(148, 76);
            // 
            // downloadToolStripMenuItem
            // 
            downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            downloadToolStripMenuItem.Size = new Size(147, 24);
            downloadToolStripMenuItem.Text = "Download";
            downloadToolStripMenuItem.Click += downloadToolStripMenuItem_Click;
            // 
            // delateToolStripMenuItem
            // 
            delateToolStripMenuItem.Name = "delateToolStripMenuItem";
            delateToolStripMenuItem.Size = new Size(147, 24);
            delateToolStripMenuItem.Text = "Delate";
            delateToolStripMenuItem.Click += delateToolStripMenuItem_Click;
            // 
            // cancelToolStripMenuItem
            // 
            cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            cancelToolStripMenuItem.Size = new Size(147, 24);
            cancelToolStripMenuItem.Text = "Cancel";
            cancelToolStripMenuItem.Click += cancelToolStripMenuItem_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label2.Location = new Point(261, 141);
            label2.Name = "label2";
            label2.Size = new Size(280, 27);
            label2.TabIndex = 18;
            label2.Text = "Download From the server";
            // 
            // uploadButton
            // 
            uploadButton.FlatStyle = FlatStyle.Flat;
            uploadButton.Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            uploadButton.Location = new Point(361, 550);
            uploadButton.Name = "uploadButton";
            uploadButton.Size = new Size(94, 45);
            uploadButton.TabIndex = 19;
            uploadButton.Text = "Upload";
            uploadButton.UseVisualStyleBackColor = true;
            uploadButton.Click += uploadButton_Click;
            // 
            // clearFilesList
            // 
            clearFilesList.FlatStyle = FlatStyle.Flat;
            clearFilesList.Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            clearFilesList.Location = new Point(261, 550);
            clearFilesList.Name = "clearFilesList";
            clearFilesList.Size = new Size(94, 45);
            clearFilesList.TabIndex = 20;
            clearFilesList.Text = "Clear";
            clearFilesList.UseVisualStyleBackColor = true;
            clearFilesList.Click += clearFilesList_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label3.Location = new Point(261, 12);
            label3.Name = "label3";
            label3.Size = new Size(222, 27);
            label3.TabIndex = 21;
            label3.Text = "HARDWARE STATUS";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.7241383F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.907121F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.3250771F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.666666F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.2068958F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.1379318F));
            tableLayoutPanel1.Controls.Add(lblValDisc, 5, 1);
            tableLayoutPanel1.Controls.Add(lblValFreq, 4, 1);
            tableLayoutPanel1.Controls.Add(lblValRam, 3, 1);
            tableLayoutPanel1.Controls.Add(lblValLoad, 2, 1);
            tableLayoutPanel1.Controls.Add(lblValTemp, 1, 1);
            tableLayoutPanel1.Controls.Add(label9, 2, 0);
            tableLayoutPanel1.Controls.Add(lblValNet, 0, 1);
            tableLayoutPanel1.Controls.Add(label5, 1, 0);
            tableLayoutPanel1.Controls.Add(label4, 0, 0);
            tableLayoutPanel1.Controls.Add(label8, 5, 0);
            tableLayoutPanel1.Controls.Add(label7, 4, 0);
            tableLayoutPanel1.Controls.Add(label6, 3, 0);
            tableLayoutPanel1.Font = new Font("Microsoft YaHei UI Light", 10.2F);
            tableLayoutPanel1.Location = new Point(263, 48);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 23.7623768F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 76.2376251F));
            tableLayoutPanel1.Size = new Size(648, 87);
            tableLayoutPanel1.TabIndex = 22;
            // 
            // lblValDisc
            // 
            lblValDisc.AutoSize = true;
            lblValDisc.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblValDisc.Location = new Point(544, 23);
            lblValDisc.Name = "lblValDisc";
            lblValDisc.Size = new Size(15, 19);
            lblValDisc.TabIndex = 11;
            lblValDisc.Text = "-";
            // 
            // lblValFreq
            // 
            lblValFreq.AutoSize = true;
            lblValFreq.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblValFreq.Location = new Point(427, 23);
            lblValFreq.Name = "lblValFreq";
            lblValFreq.Size = new Size(15, 19);
            lblValFreq.TabIndex = 10;
            lblValFreq.Text = "-";
            // 
            // lblValRam
            // 
            lblValRam.AutoSize = true;
            lblValRam.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblValRam.Location = new Point(320, 23);
            lblValRam.Name = "lblValRam";
            lblValRam.Size = new Size(15, 19);
            lblValRam.TabIndex = 9;
            lblValRam.Text = "-";
            // 
            // lblValLoad
            // 
            lblValLoad.AutoSize = true;
            lblValLoad.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblValLoad.Location = new Point(221, 23);
            lblValLoad.Name = "lblValLoad";
            lblValLoad.Size = new Size(15, 19);
            lblValLoad.TabIndex = 8;
            lblValLoad.Text = "-";
            // 
            // lblValTemp
            // 
            lblValTemp.AutoSize = true;
            lblValTemp.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblValTemp.Location = new Point(157, 23);
            lblValTemp.Name = "lblValTemp";
            lblValTemp.Size = new Size(15, 19);
            lblValTemp.TabIndex = 7;
            lblValTemp.Text = "-";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label9.Location = new Point(221, 2);
            label9.Name = "label9";
            label9.Size = new Size(68, 19);
            label9.TabIndex = 6;
            label9.Text = "CPU Load";
            // 
            // lblValNet
            // 
            lblValNet.AutoSize = true;
            lblValNet.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            lblValNet.Location = new Point(5, 23);
            lblValNet.Name = "lblValNet";
            lblValNet.Size = new Size(15, 19);
            lblValNet.TabIndex = 5;
            lblValNet.Text = "-";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label5.Location = new Point(157, 2);
            label5.Name = "label5";
            label5.Size = new Size(43, 19);
            label5.TabIndex = 1;
            label5.Text = "Temp";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label4.Location = new Point(5, 2);
            label4.Name = "label4";
            label4.Size = new Size(59, 19);
            label4.TabIndex = 0;
            label4.Text = "Network";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label8.Location = new Point(544, 2);
            label8.Name = "label8";
            label8.Size = new Size(37, 19);
            label8.TabIndex = 4;
            label8.Text = "DISC";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label7.Location = new Point(427, 2);
            label7.Name = "label7";
            label7.Size = new Size(66, 19);
            label7.TabIndex = 3;
            label7.Text = "CPU Freq";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft YaHei UI Light", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label6.Location = new Point(320, 2);
            label6.Name = "label6";
            label6.Size = new Size(38, 19);
            label6.TabIndex = 2;
            label6.Text = "RAM";
            // 
            // GUI
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(1539, 603);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label3);
            Controls.Add(clearFilesList);
            Controls.Add(uploadButton);
            Controls.Add(label2);
            Controls.Add(filesOnTheServer);
            Controls.Add(label1);
            Controls.Add(filesListToSend);
            Controls.Add(clientsList);
            Controls.Add(dropPanel);
            Controls.Add(chatWindowTitle);
            Controls.Add(messageBox);
            Controls.Add(chatWindow);
            Controls.Add(sendButton);
            Controls.Add(navigationList);
            Controls.Add(portbox);
            Controls.Add(ipaddressbox);
            Controls.Add(connectOrDisconnectButton);
            Controls.Add(usernameTextBox);
            Font = new Font("Corbel", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GUI";
            Text = "CommunicationSystems";
            dropPanel.ResumeLayout(false);
            dropPanel.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox chatWindow;
        private TextBox messageBox;
        private Button sendButton;
        private TextBox usernameTextBox;
        private Button connectOrDisconnectButton;
        private TextBox ipaddressbox;
        private TextBox portbox;
        private ListBox navigationList;
        private Label chatWindowTitle;
        private Panel dropPanel;
        private Label dropPanelTitle;
        private ListBox clientsList;
        private ListView filesListToSend;
        private Label label1;
        private ListView filesOnTheServer;
        private Label label2;
        private ColumnHeader Filename;
        private ColumnHeader Size;
        private Button uploadButton;
        private Button clearFilesList;
        private ColumnHeader Plik;
        private ColumnHeader rozmiar;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem downloadToolStripMenuItem;
        private ToolStripMenuItem delateToolStripMenuItem;
        private ToolStripMenuItem cancelToolStripMenuItem;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label4;
        private Label label5;
        private Label lblValNet;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label lblValTemp;
        private Label label9;
        private Label lblValDisc;
        private Label lblValFreq;
        private Label lblValRam;
        private Label lblValLoad;
    }
}

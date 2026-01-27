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
            dropPanel.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // chatWindow
            // 
            chatWindow.BorderStyle = BorderStyle.FixedSingle;
            chatWindow.Font = new Font("Microsoft YaHei UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chatWindow.Location = new Point(739, 54);
            chatWindow.Name = "chatWindow";
            chatWindow.Size = new Size(556, 506);
            chatWindow.TabIndex = 0;
            chatWindow.Text = "";
            // 
            // messageBox
            // 
            messageBox.BorderStyle = BorderStyle.FixedSingle;
            messageBox.Font = new Font("Microsoft YaHei UI", 10.2F);
            messageBox.Location = new Point(842, 566);
            messageBox.Multiline = true;
            messageBox.Name = "messageBox";
            messageBox.Size = new Size(453, 45);
            messageBox.TabIndex = 1;
            // 
            // sendButton
            // 
            sendButton.FlatStyle = FlatStyle.Flat;
            sendButton.Font = new Font("Microsoft YaHei UI", 10.2F);
            sendButton.Location = new Point(739, 566);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(94, 45);
            sendButton.TabIndex = 2;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // usernameTextBox
            // 
            usernameTextBox.BorderStyle = BorderStyle.FixedSingle;
            usernameTextBox.Font = new Font("Microsoft YaHei UI", 10.2F);
            usernameTextBox.Location = new Point(25, 10);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(252, 29);
            usernameTextBox.TabIndex = 4;
            usernameTextBox.Text = "Username";
            // 
            // connectOrDisconnectButton
            // 
            connectOrDisconnectButton.FlatStyle = FlatStyle.Flat;
            connectOrDisconnectButton.Font = new Font("Microsoft YaHei UI", 10.2F);
            connectOrDisconnectButton.Location = new Point(25, 566);
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
            ipaddressbox.Font = new Font("Microsoft YaHei UI", 10.2F);
            ipaddressbox.Location = new Point(25, 531);
            ipaddressbox.Name = "ipaddressbox";
            ipaddressbox.Size = new Size(165, 29);
            ipaddressbox.TabIndex = 9;
            ipaddressbox.Text = "IP Address";
            // 
            // portbox
            // 
            portbox.BorderStyle = BorderStyle.FixedSingle;
            portbox.Font = new Font("Microsoft YaHei UI", 10.2F);
            portbox.Location = new Point(196, 531);
            portbox.Name = "portbox";
            portbox.Size = new Size(81, 29);
            portbox.TabIndex = 10;
            portbox.Text = "Port";
            // 
            // navigationList
            // 
            navigationList.BorderStyle = BorderStyle.FixedSingle;
            navigationList.FormattingEnabled = true;
            navigationList.Items.AddRange(new object[] { "Broadcast", "Unicast" });
            navigationList.Location = new Point(25, 54);
            navigationList.Name = "navigationList";
            navigationList.Size = new Size(252, 44);
            navigationList.TabIndex = 11;
            navigationList.SelectedIndexChanged += navigationList_SelectedIndexChanged;
            // 
            // chatWindowTitle
            // 
            chatWindowTitle.AutoSize = true;
            chatWindowTitle.FlatStyle = FlatStyle.Flat;
            chatWindowTitle.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 238);
            chatWindowTitle.Location = new Point(739, 9);
            chatWindowTitle.Name = "chatWindowTitle";
            chatWindowTitle.Size = new Size(211, 40);
            chatWindowTitle.TabIndex = 12;
            chatWindowTitle.Text = "ChatWindow";
            // 
            // dropPanel
            // 
            dropPanel.AllowDrop = true;
            dropPanel.BackColor = Color.White;
            dropPanel.Controls.Add(dropPanelTitle);
            dropPanel.Location = new Point(365, 531);
            dropPanel.Name = "dropPanel";
            dropPanel.Size = new Size(368, 81);
            dropPanel.TabIndex = 13;
            dropPanel.DragDrop += dropPanel_DragDrop;
            dropPanel.DragEnter += dropPanel_DragEnter;
            // 
            // dropPanelTitle
            // 
            dropPanelTitle.AutoSize = true;
            dropPanelTitle.Font = new Font("Microsoft YaHei UI Light", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            dropPanelTitle.Location = new Point(3, 2);
            dropPanelTitle.Name = "dropPanelTitle";
            dropPanelTitle.Size = new Size(144, 27);
            dropPanelTitle.TabIndex = 0;
            dropPanelTitle.Text = "Drop files here";
            // 
            // clientsList
            // 
            clientsList.BorderStyle = BorderStyle.FixedSingle;
            clientsList.FormattingEnabled = true;
            clientsList.Location = new Point(25, 115);
            clientsList.Name = "clientsList";
            clientsList.Size = new Size(252, 401);
            clientsList.TabIndex = 14;
            clientsList.SelectedIndexChanged += clientsList_SelectedIndexChanged;
            // 
            // filesListToSend
            // 
            filesListToSend.BorderStyle = BorderStyle.FixedSingle;
            filesListToSend.Columns.AddRange(new ColumnHeader[] { Filename, Size });
            filesListToSend.Font = new Font("Microsoft YaHei UI Light", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 238);
            filesListToSend.Location = new Point(283, 341);
            filesListToSend.Name = "filesListToSend";
            filesListToSend.Size = new Size(450, 175);
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
            label1.Location = new Point(283, 311);
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
            filesOnTheServer.Location = new Point(283, 145);
            filesOnTheServer.Name = "filesOnTheServer";
            filesOnTheServer.Size = new Size(450, 163);
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
            contextMenuStrip1.Size = new Size(211, 104);
            // 
            // downloadToolStripMenuItem
            // 
            downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            downloadToolStripMenuItem.Size = new Size(210, 24);
            downloadToolStripMenuItem.Text = "Download";
            downloadToolStripMenuItem.Click += downloadToolStripMenuItem_Click;
            // 
            // delateToolStripMenuItem
            // 
            delateToolStripMenuItem.Name = "delateToolStripMenuItem";
            delateToolStripMenuItem.Size = new Size(210, 24);
            delateToolStripMenuItem.Text = "Delate";
            delateToolStripMenuItem.Click += delateToolStripMenuItem_Click;
            // 
            // cancelToolStripMenuItem
            // 
            cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            cancelToolStripMenuItem.Size = new Size(210, 24);
            cancelToolStripMenuItem.Text = "Cancel";
            cancelToolStripMenuItem.Click += cancelToolStripMenuItem_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Microsoft YaHei UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label2.Location = new Point(283, 115);
            label2.Name = "label2";
            label2.Size = new Size(280, 27);
            label2.TabIndex = 18;
            label2.Text = "Download From the server";
            // 
            // uploadButton
            // 
            uploadButton.FlatStyle = FlatStyle.Flat;
            uploadButton.Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            uploadButton.Location = new Point(283, 531);
            uploadButton.Name = "uploadButton";
            uploadButton.Size = new Size(76, 39);
            uploadButton.TabIndex = 19;
            uploadButton.Text = "Upload";
            uploadButton.UseVisualStyleBackColor = true;
            uploadButton.Click += uploadButton_Click;
            // 
            // clearFilesList
            // 
            clearFilesList.FlatStyle = FlatStyle.Flat;
            clearFilesList.Font = new Font("Microsoft YaHei UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            clearFilesList.Location = new Point(283, 576);
            clearFilesList.Name = "clearFilesList";
            clearFilesList.Size = new Size(76, 36);
            clearFilesList.TabIndex = 20;
            clearFilesList.Text = "Clear";
            clearFilesList.UseVisualStyleBackColor = true;
            clearFilesList.Click += clearFilesList_Click;
            // 
            // GUI
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(1307, 624);
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
    }
}

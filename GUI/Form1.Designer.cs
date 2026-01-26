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
            dropPanel.SuspendLayout();
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
            usernameTextBox.Size = new Size(295, 29);
            usernameTextBox.TabIndex = 4;
            usernameTextBox.Text = "Username";
            // 
            // connectOrDisconnectButton
            // 
            connectOrDisconnectButton.FlatStyle = FlatStyle.Flat;
            connectOrDisconnectButton.Font = new Font("Microsoft YaHei UI", 10.2F);
            connectOrDisconnectButton.Location = new Point(25, 566);
            connectOrDisconnectButton.Name = "connectOrDisconnectButton";
            connectOrDisconnectButton.Size = new Size(296, 45);
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
            ipaddressbox.Size = new Size(153, 29);
            ipaddressbox.TabIndex = 9;
            ipaddressbox.Text = "IP Address";
            // 
            // portbox
            // 
            portbox.BorderStyle = BorderStyle.FixedSingle;
            portbox.Font = new Font("Microsoft YaHei UI", 10.2F);
            portbox.Location = new Point(187, 531);
            portbox.Name = "portbox";
            portbox.Size = new Size(133, 29);
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
            navigationList.Size = new Size(295, 44);
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
            dropPanel.Location = new Point(327, 566);
            dropPanel.Name = "dropPanel";
            dropPanel.Size = new Size(406, 46);
            dropPanel.TabIndex = 13;
            dropPanel.DragDrop += dropPanel_DragDrop;
            dropPanel.DragEnter += dropPanel_DragEnter;
            // 
            // dropPanelTitle
            // 
            dropPanelTitle.AutoSize = true;
            dropPanelTitle.Font = new Font("Microsoft YaHei UI Light", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            dropPanelTitle.Location = new Point(130, 8);
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
            clientsList.Size = new Size(295, 401);
            clientsList.TabIndex = 14;
            clientsList.SelectedIndexChanged += clientsList_SelectedIndexChanged;
            // 
            // GUI
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(1307, 624);
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
    }
}

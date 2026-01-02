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
            paintButton = new Button();
            Features = new Label();
            playButton = new Button();
            ipaddressbox = new TextBox();
            portbox = new TextBox();
            SuspendLayout();
            // 
            // chatWindow
            // 
            chatWindow.Font = new Font("Microsoft YaHei UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chatWindow.Location = new Point(326, 10);
            chatWindow.Name = "chatWindow";
            chatWindow.Size = new Size(562, 398);
            chatWindow.TabIndex = 0;
            chatWindow.Text = "";
            // 
            // messageBox
            // 
            messageBox.Font = new Font("Microsoft YaHei UI", 10.2F);
            messageBox.Location = new Point(326, 414);
            messageBox.Multiline = true;
            messageBox.Name = "messageBox";
            messageBox.Size = new Size(562, 46);
            messageBox.TabIndex = 1;
            // 
            // sendButton
            // 
            sendButton.Font = new Font("Microsoft YaHei UI", 10.2F);
            sendButton.Location = new Point(186, 414);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(134, 45);
            sendButton.TabIndex = 2;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // usernameTextBox
            // 
            usernameTextBox.Font = new Font("Microsoft YaHei UI", 10.2F);
            usernameTextBox.Location = new Point(24, 10);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(296, 29);
            usernameTextBox.TabIndex = 4;
            usernameTextBox.Text = "Username";
            // 
            // connectOrDisconnectButton
            // 
            connectOrDisconnectButton.Font = new Font("Microsoft YaHei UI", 10.2F);
            connectOrDisconnectButton.Location = new Point(24, 414);
            connectOrDisconnectButton.Name = "connectOrDisconnectButton";
            connectOrDisconnectButton.Size = new Size(156, 45);
            connectOrDisconnectButton.TabIndex = 5;
            connectOrDisconnectButton.Text = "Connect";
            connectOrDisconnectButton.UseVisualStyleBackColor = true;
            connectOrDisconnectButton.Click += connectOrDisconnectButton_Click;
            // 
            // paintButton
            // 
            paintButton.Font = new Font("Microsoft YaHei UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 238);
            paintButton.Location = new Point(24, 136);
            paintButton.Name = "paintButton";
            paintButton.Size = new Size(156, 169);
            paintButton.TabIndex = 6;
            paintButton.Text = "Paint Mode";
            paintButton.UseVisualStyleBackColor = true;
            paintButton.Click += paintButton_Click;
            // 
            // Features
            // 
            Features.BackColor = Color.Azure;
            Features.Font = new Font("Microsoft YaHei UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 238);
            Features.ForeColor = Color.Black;
            Features.Location = new Point(25, 58);
            Features.Name = "Features";
            Features.Size = new Size(295, 52);
            Features.TabIndex = 7;
            Features.Text = "Features";
            Features.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // playButton
            // 
            playButton.Font = new Font("Microsoft YaHei UI", 10.2F);
            playButton.Location = new Point(187, 136);
            playButton.Name = "playButton";
            playButton.Size = new Size(134, 169);
            playButton.TabIndex = 8;
            playButton.Text = "Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += playButton_Click;
            // 
            // ipaddressbox
            // 
            ipaddressbox.Font = new Font("Microsoft YaHei UI", 10.2F);
            ipaddressbox.Location = new Point(24, 379);
            ipaddressbox.Name = "ipaddressbox";
            ipaddressbox.Size = new Size(155, 29);
            ipaddressbox.TabIndex = 9;
            ipaddressbox.Text = "IP Address";
            // 
            // portbox
            // 
            portbox.Font = new Font("Microsoft YaHei UI", 10.2F);
            portbox.Location = new Point(187, 379);
            portbox.Name = "portbox";
            portbox.Size = new Size(135, 29);
            portbox.TabIndex = 10;
            portbox.Text = "Port";
            // 
            // GUI
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(900, 472);
            Controls.Add(portbox);
            Controls.Add(ipaddressbox);
            Controls.Add(playButton);
            Controls.Add(Features);
            Controls.Add(paintButton);
            Controls.Add(connectOrDisconnectButton);
            Controls.Add(usernameTextBox);
            Controls.Add(sendButton);
            Controls.Add(messageBox);
            Controls.Add(chatWindow);
            Font = new Font("Corbel", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GUI";
            Text = "CommunicationSystems";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox chatWindow;
        private TextBox messageBox;
        private Button sendButton;
        private TextBox usernameTextBox;
        private Button connectOrDisconnectButton;
        private Button paintButton;
        private Label Features;
        private Button playButton;
        private TextBox ipaddressbox;
        private TextBox portbox;
    }
}

using ChatLogic;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUI : Form
    {
        public Client client = new Client();
        public GUI()
        {
            InitializeComponent();
            client.OnMessageReceived += HandleMessage;
            client.OnLog += HandleLog;
            connectOrDisconnectButton.Enabled = true;
        }
        private void connectOrDisconnectButton_Click(object sender, EventArgs e)
        {
            if (client.isClientConnected)
            {
                 client.SendTcp("DISCONNECT");
            }
            else
            {
                string ip = ipaddressbox.Text;
                int port = int.Parse(portbox.Text);
                string username = usernameTextBox.Text;

                if (string.IsNullOrWhiteSpace(username))
                {
                    MessageBox.Show("Give your name first!");
                    return;
                }
                client.connectToServer(ip, port);
                client.SendTcp(username);
                usernameTextBox.Enabled = false;
                connectOrDisconnectButton.Text = "Disconnect";
            }
        }
        private void sendButton_Click(object sender, EventArgs e)
        {
            navigationList.BeginUpdate();
            navigationList.EndUpdate();

            if (navigationList.SelectedItem.ToString() == "Broadcast")
            {
                string msg = "BROADCAST" + messageBox.Text;
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    client.SendTcp(msg);
                    messageBox.Clear();
                }
            }
            else if (navigationList.SelectedItem.ToString() == "Unicast")
            {
                if(clientsList.SelectedIndex != -1)
                {
                    string msg = "UNICAST " + clientsList.SelectedItem.ToString() + " " + messageBox.Text;
                    if (!string.IsNullOrWhiteSpace(msg))
                    {
                        client.SendTcp(msg);
                        messageBox.Clear();
                    }
                }              
            }
        }
        private void HandleMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => HandleMessage(message)));
                return;
            }
            
            if (message.Contains("[CLIENTSLIST UPDATE]"))
            {
                handleClientsList(message);
            }
            else if(message.Contains(usernameTextBox.Text) && message.Contains("has disconnected"))
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                chatWindow.AppendText($"[{timestamp}] {message}{Environment.NewLine}");

                client.disconnect();

                usernameTextBox.Enabled = true;
                connectOrDisconnectButton.Text = "Connect";
                connectOrDisconnectButton.Enabled = true;

                clientsList.Items.Clear();
            }
            else
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                chatWindow.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
                chatWindow.SelectionStart = chatWindow.Text.Length;
                chatWindow.ScrollToCaret();
            }  
        }
        private void HandleLog(string log)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => HandleLog(log)));
                return;
            }
            chatWindow.AppendText($"[SYSTEM]: {log}{Environment.NewLine}");
        }
        private void dropPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void dropPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files != null && files.Length > 0)
            {
                string filePath = files[0];
                //Thread t = new Thread(() => SendFileToServer(filePath));
                //t.Start();
            }
        }

        private void navigationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (navigationList != null)
            {
                string choosenOption = navigationList.SelectedItem.ToString();
            }
        }

        private void clientsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (navigationList != null)
            {
                string choosenClient = clientsList.SelectedItem.ToString();
            }
        }
        private void handleClientsList(string message)
        {          
            clientsList.Items.Clear();

            int spaceIndex = message.IndexOf(' ', 26);
            string onlyNames = message.Substring(spaceIndex+1);
            string[] users = onlyNames.Split(',');

            foreach (string user in users) 
            {
                if (user != usernameTextBox.Text)
                {
                    clientsList.Items.Add(user);
                }
            }
        }
    }
}

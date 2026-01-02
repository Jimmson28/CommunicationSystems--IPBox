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
                client.SendTcp("Bye!");
                usernameTextBox.Enabled = true;
                connectOrDisconnectButton.Text = "Connect";
                client.disconnect();
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
            string msg = messageBox.Text;
            if (!string.IsNullOrWhiteSpace(msg))
            {
                client.SendTcp(msg);
                messageBox.Clear();
            }
        }
        private void HandleMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => HandleMessage(message)));
                return;
            }

            string timestamp = DateTime.Now.ToString("HH:mm:ss");
            chatWindow.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
            chatWindow.SelectionStart = chatWindow.Text.Length;
            chatWindow.ScrollToCaret();
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

        private void paintButton_Click(object sender, EventArgs e)
        {

        }

        private void playButton_Click(object sender, EventArgs e)
        {

        }
    }
}

using ChatLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class GUI : Form
    {
        public Client client = new Client();
        private Queue<ListViewItem> uploadQueue = new Queue<ListViewItem>(); //kolejka na pliki do wysylania
        private Queue<ListViewItem> downloadQueue = new Queue<ListViewItem>(); //kolejka na pliki do pobierania
        private string currentFileInBase64;

        // ZMIENNE DO POBIERANIA
        private bool isDownloading = false;
        private StringBuilder downloadBuffer = new StringBuilder();
        private long currentDownloadingFileSize;
        private string currentDownloadingFileName = "";

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
                if (clientsList.SelectedIndex != -1)
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

        //funckcja obslugujaca przychodzace wiadomosci z servera
        private void HandleMessage(string rawMessage)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => HandleMessage(rawMessage)));
                return;
            }
            string[] packets = rawMessage.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string message in packets)
            {
                if (isDownloading)
                {
                    if (message.StartsWith("[DOWNLOADING FILE FROM SERVER]|"))
                    {
                 
                        StartNewDownload(message);
                    }
                    else
                    {
                        downloadBuffer.Append(message);
                        CheckIfDownloadFinished();
                    }
                    continue;
                }
                if (message.StartsWith("[DOWNLOADING FILE FROM SERVER]|"))
                {
                    StartNewDownload(message);
                }
                else if (message.Contains("[CLIENTSLIST UPDATE]"))
                {
                    handleClientsList(message);
                }
                else if (message.Contains(usernameTextBox.Text) && message.Contains("has disconnected"))
                {
                    string timestamp = DateTime.Now.ToString("HH:mm:ss");
                    chatWindow.AppendText($"[{timestamp}] {message}{Environment.NewLine}");
                    client.disconnect();
                    usernameTextBox.Enabled = true;
                    connectOrDisconnectButton.Text = "Connect";
                    connectOrDisconnectButton.Enabled = true;
                    clientsList.Items.Clear();
                }
                else if (message.Contains("[FILE TRANSFER ACCEPTED]"))
                {
                    if (uploadQueue.Count > 0)
                    {
                        client.SendTcp($"FILEDATA|{currentFileInBase64}");
                        ListViewItem finishedItem = uploadQueue.Dequeue();
                        currentFileInBase64 = "";
                        sendFileDataOnServer();
                    }
                }
                else if (message.Contains("[FILES ON THE SERVER]"))
                {
                    handleFilesList(message);
                }
                else if (message.StartsWith("[HARDWARE STATUS]"))
                {
                    string[] parts = message.Split('|');
                    if (parts.Length >= 7)
                    {
                        lblValNet.Text = parts[1];
                        lblValTemp.Text = parts[2];
                        lblValLoad.Text = parts[3];
                        lblValRam.Text = parts[4];
                        lblValFreq.Text = parts[5];
                        lblValDisc.Text = parts[6];
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        string timestamp = DateTime.Now.ToString("HH:mm:ss");
                        chatWindow.AppendText($"[{timestamp}] [TCP]: {message}{Environment.NewLine}");
                        chatWindow.SelectionStart = chatWindow.Text.Length;
                        chatWindow.ScrollToCaret();
                    }
                }
            }
        }
        private void StartNewDownload(string message)
        {
            try
            {
                string headerPrefix = "[DOWNLOADING FILE FROM SERVER]|";
                string[] parts = message.Split('|');

                if (parts.Length >= 3)
                {
                    currentDownloadingFileSize = long.Parse(parts[1]);
                    currentDownloadingFileName = parts[2];
                    string fullHeader = headerPrefix + parts[1] + "|" + parts[2] + "|";

                    downloadBuffer.Clear();
                    isDownloading = true;

                    if (message.Length >= fullHeader.Length)
                    {
                        string data = message.Substring(fullHeader.Length);
                        downloadBuffer.Append(data);
                        CheckIfDownloadFinished();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("B³¹d startu pobierania: " + ex.Message);
                isDownloading = false;
            }
        }

        private void CheckIfDownloadFinished()
        {
            if (downloadBuffer.Length >= currentDownloadingFileSize)
            {
                FinishDownloading();
            }
        }
        //funckja obslugi logow systemowych
        private void HandleLog(string log)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => HandleLog(log)));
                return;
            }
            chatWindow.AppendText($"[SYSTEM]: {log}{Environment.NewLine}");
        }
        private void FinishDownloading()
        {
            isDownloading = false;
            try
            {
                string finalBase64 = downloadBuffer.ToString();

                finalBase64 = finalBase64.Trim().Replace("\r", "").Replace("\n", "").Replace(" ", "");

                byte[] fileBytes = Convert.FromBase64String(finalBase64);

                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string downloadsFolder = Path.Combine(userProfile, "Downloads");
                string savePath = Path.Combine(downloadsFolder, "Pobrane_" + currentDownloadingFileName);

                File.WriteAllBytes(savePath, fileBytes);

                MessageBox.Show($"File {currentDownloadingFileName} has been downloaded to:\n{savePath}");

                if (downloadQueue.Count > 0) downloadQueue.Dequeue();
                sendInformationToTheServerAboutFilesToDownload();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failure during saving file {currentDownloadingFileName}: {e.Message}");
                if (downloadQueue.Count > 0) downloadQueue.Dequeue();
                isDownloading = false;
                downloadBuffer.Clear();
            }
        }
        //drag an drop panel
        private void dropPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void dropPanel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                addFileToTheFileList(file);
            }
        }

        private void addFileToTheFileList(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            ListViewItem item = new ListViewItem(fileInfo.FullName);
            item.SubItems.Add($"{fileInfo.Length / 1024.0:F3} kB");
            filesListToSend.Items.Add(item);
        }

        private void uploadButton_Click(object sender, EventArgs e)
        {
            if (filesListToSend.Items.Count == 0) return;

            uploadButton.Enabled = false;
            uploadQueue.Clear();
            foreach (ListViewItem item in filesListToSend.Items)
            {
                uploadQueue.Enqueue(item);
            }

            sendFileDataOnServer();
            filesListToSend.Items.Clear();
        }

        private void clearFilesList_Click(object sender, EventArgs e)
        {
            filesListToSend.Items.Clear();
        }

        private void sendFileDataOnServer()
        {
            if (uploadQueue.Count > 0)
            {
                try
                {
                    ListViewItem firstItem = uploadQueue.Peek();
                    string filePath = firstItem.Text;
                    string fileName = Path.GetFileName(filePath);
                    byte[] base64FileContent = File.ReadAllBytes(filePath);
                    currentFileInBase64 = Convert.ToBase64String(base64FileContent);
                    int size = currentFileInBase64.Length;

                    client.SendTcp($"FILEHEADERS|{size}|{fileName}|");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failure during uploading files on the server: " + ex.Message);
                    uploadQueue.Dequeue();
                    sendFileDataOnServer();
                }
            }
            else
            {
                uploadButton.Enabled = true;
                MessageBox.Show("Files have been uploaded on the server.");
            }
        }

        //update listy plikow na dysku serwera w gui
        private void handleFilesList(string message)
        {
            filesOnTheServer.Items.Clear();

            if (message.Length < 21) return;
            string onlyFileNames = message.Substring(21);

            string[] files = onlyFileNames.Split(',');

            for (int i = 0; i < files.Length - 1; i += 2)
            {
                string fileName = files[i];
                string size = files[i + 1];
                if (string.IsNullOrWhiteSpace(fileName)) continue;

                ListViewItem item = new ListViewItem(fileName);
                size = size.Replace(".", ",");

                item.SubItems.Add(size + " KB");
                filesOnTheServer.Items.Add(item);
            }
        }
        private void sendInformationToTheServerAboutFilesToDownload()
        {
            if (downloadQueue.Count > 0)
            {
                try
                {
                    ListViewItem firstItem = downloadQueue.Peek();
                    currentDownloadingFileName = firstItem.Text;

                    client.SendTcp($"DOWNLOADFILE|{currentDownloadingFileName}|");

                    downloadBuffer.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failure during init downloading: " + ex.Message);
                    downloadQueue.Dequeue();
                    sendInformationToTheServerAboutFilesToDownload();
                }
            }
            else
            {
                MessageBox.Show("All files processed from queue.");
            }
        }
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesOnTheServer.SelectedItems.Count == 0) return;

            foreach (ListViewItem file in filesOnTheServer.SelectedItems)
            {
                downloadQueue.Enqueue(file);
            }
            if (!isDownloading)
            {
                sendInformationToTheServerAboutFilesToDownload();
            }
        }

        private void delateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesOnTheServer.SelectedItems.Count > 0)
            {
                string fileName = "";
                foreach (ListViewItem file in filesOnTheServer.SelectedItems) 
                {
                    fileName += file.Text + ",";
                }
                fileName = fileName.TrimEnd(',');
                var res = MessageBox.Show($"Delete {fileName}?", "Confirm", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    client.SendTcp($"DELETE|{fileName}");
                }
            }
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            return;
        }

        private void navigationList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (navigationList != null) { /* ... */ }
        }

        private void clientsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (navigationList != null) { /* ... */ }
        }

        //update listy polaczonych z serverem klientow
        private void handleClientsList(string message)
        {
            clientsList.Items.Clear();
            if (message.Length < 21) return;

            string onlyNames = message.Substring(21);
            string[] users = onlyNames.Split(',');

            foreach (string user in users)
            {
                if (!string.IsNullOrWhiteSpace(user) && user != usernameTextBox.Text)
                {
                    clientsList.Items.Add(user.Trim());
                }
            }
        }
    }
}
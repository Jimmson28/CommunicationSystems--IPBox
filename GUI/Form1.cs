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
        private long currentDownloadingFileSize; // ZMIANA: long zamiast double (liczymy znaki, nie KB)
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

        // --- G£ÓWNA FUNKCJA OBS£UGI WIADOMOŒCI ---
        private void HandleMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => HandleMessage(message)));
                return;
            }

            // 1. LOGIKA POBIERANIA (PRIORYTET)
            if (isDownloading)
            {
                // Sprawdzamy czy w paczce jest nag³ówek (start pobierania w trakcie)
                string headerPrefix = "[DOWNLOADING FILE FROM SERVER]|";
                if (message.StartsWith(headerPrefix))
                {
                    // Format: [DOWNLOADING FILE FROM SERVER]|ROZMIAR|NAZWA|...DANE...
                    string[] parts = message.Split('|');
                    if (parts.Length >= 3)
                    {
                        currentDownloadingFileSize = long.Parse(parts[1]); // Rozmiar Base64
                        currentDownloadingFileName = parts[2];
                        downloadBuffer.Clear();

                        // Obliczamy d³ugoœæ nag³ówka, ¿eby wzi¹æ resztê (dane)
                        string fullHeader = parts[0] + "|" + parts[1] + "|" + parts[2] + "|";

                        if (message.Length > fullHeader.Length)
                        {
                            downloadBuffer.Append(message.Substring(fullHeader.Length));
                        }
                    }
                }
                else
                {
                    // Zwyk³a paczka danych - doklejamy
                    downloadBuffer.Append(message);
                }

                // Sprawdzamy czy mamy ju¿ ca³oœæ
                if (downloadBuffer.Length >= currentDownloadingFileSize)
                {
                    FinishDownloading();
                }
                return; // Nie pokazujemy danych pliku na czacie!
            }

            // 2. LOGIKA CZATU I LIST (jeœli nie pobieramy)

            // Start pobierania (Nag³ówek z³apany gdy isDownloading = false)
            if (message.StartsWith("[DOWNLOADING FILE FROM SERVER]|"))
            {
                isDownloading = true;
                HandleMessage(message); // Rekurencja, ¿eby wpad³o w if(isDownloading)
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
            else
            {
                // Zwyk³a wiadomoœæ czatu
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                // Dodajemy [TCP] wizualnie tutaj, bo usunêliœmy z biblioteki
                chatWindow.AppendText($"[{timestamp}] [TCP]: {message}{Environment.NewLine}");
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

        // --- FUNKCJA KOÑCZ¥CA POBIERANIE ---
        private void FinishDownloading()
        {
            isDownloading = false;
            try
            {
                string finalBase64 = downloadBuffer.ToString();
                byte[] fileBytes = Convert.FromBase64String(finalBase64);

                // Œcie¿ka do folderu Pobrane
                string userProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string downloadsFolder = Path.Combine(userProfile, "Downloads");
                string savePath = Path.Combine(downloadsFolder, "Pobrane_" + currentDownloadingFileName);

                File.WriteAllBytes(savePath, fileBytes);

                MessageBox.Show($"File {currentDownloadingFileName} has been downloaded to:\n{savePath}");

                // Usuwamy z kolejki dopiero po sukcesie
                if (downloadQueue.Count > 0) downloadQueue.Dequeue();

                // Pobieramy nastêpny plik z kolejki
                sendInformationToTheServerAboutFilesToDownload();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Failure during saving file {currentDownloadingFileName}: {e.Message}");
                // Nawet jak b³¹d, usuwamy z kolejki ¿eby nie zablokowaæ programu
                if (downloadQueue.Count > 0) downloadQueue.Dequeue();
                isDownloading = false;
                downloadBuffer.Clear();
            }
        }

        // --- OBS£UGA DRAG & DROP (UPLOAD) ---
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

        // --- AKTUALIZACJA LISTY PLIKÓW ---
        private void handleFilesList(string message)
        {
            filesOnTheServer.Items.Clear();

            // Poprawka: Usunêliœmy [TCP]:, wiêc nag³ówek ma dok³adnie 21 znaków
            if (message.Length < 21) return;
            string onlyFileNames = message.Substring(21);

            string[] files = onlyFileNames.Split(',');

            for (int i = 0; i < files.Length - 1; i += 2)
            {
                string fileName = files[i];
                string size = files[i + 1];
                if (string.IsNullOrWhiteSpace(fileName)) continue;

                ListViewItem item = new ListViewItem(fileName);
                size = size.Replace(".", ","); // Kosmetyka dla polskiego systemu

                item.SubItems.Add(size + " KB");
                filesOnTheServer.Items.Add(item);
            }
        }

        // --- START POBIERANIA (INIT) ---
        private void sendInformationToTheServerAboutFilesToDownload()
        {
            if (downloadQueue.Count > 0)
            {
                try
                {
                    ListViewItem firstItem = downloadQueue.Peek();
                    currentDownloadingFileName = firstItem.Text;

                    // Serwer (C++) wyœle nam w nag³ówku dok³adny rozmiar,
                    // wiêc tutaj tylko wysy³amy ¿¹danie. 
                    // Rozmiar w GUI (kB) jest tylko dla oka, nie u¿ywamy go do logiki pêtli.

                    client.SendTcp($"DOWNLOADFILE|{currentDownloadingFileName}|");

                    // Resetujemy bufor na wszelki wypadek
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

        // --- MENU KONTEKSTOWE (Prawy Przycisk) ---
        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filesOnTheServer.SelectedItems.Count == 0) return;

            foreach (ListViewItem file in filesOnTheServer.SelectedItems)
            {
                downloadQueue.Enqueue(file);
            }
            // Jeœli nic siê aktualnie nie pobiera, startujemy kolejkê
            if (!isDownloading)
            {
                sendInformationToTheServerAboutFilesToDownload();
            }
        }

        private void delateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Logika usuwania (wymaga obs³ugi po stronie serwera nag³ówka DELETE)
            if (filesOnTheServer.SelectedItems.Count > 0)
            {
                string fileName = filesOnTheServer.SelectedItems[0].Text;
                var res = MessageBox.Show($"Delete {fileName}?", "Confirm", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    client.SendTcp($"DELETE|{fileName}"); // Wymaga obs³ugi w C++
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

        // --- AKTUALIZACJA LISTY KLIENTÓW ---
        private void handleClientsList(string message)
        {
            clientsList.Items.Clear();

            // Poprawka: Nag³ówek "[CLIENTSLIST UPDATE] " ma 21 znaków
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
/**
 * Remote System Monitor - Concurrent Server (Multithreaded)
 * Obsługa WIELU klientów jednocześnie.
 */
#include <stdio.h>
#include <cstdio>
#include "base64.h"
#include <filesystem>
#include <iostream>
#include <string>
#include <fstream>
#include <cstring>
#include <unistd.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <netdb.h>
#include <thread>
#include <vector>
#include <mutex>
#include <algorithm>
#include <map>
#include <bits/stdc++.h>
#include <sys/statvfs.h>

using namespace std;
namespace fs = std::filesystem;

#define PORT 8080
#define TEMP_PATH "/sys/class/thermal/thermal_zone0/temp"
#define PROCESSOR_PATH "/proc/loadavg"
#define RAM_INFO "/proc/meminfo"
#define FREQ_INFO "/sys/devices/system/cpu/cpu0/cpufreq/scaling_cur_freq"
#define RX_PATH "/sys/class/net/wlan0/statistics/rx_bytes"
#define TX_PATH "/sys/class/net/wlan0/statistics/tx_bytes"

vector<int> clients;    //lista polaczonych do servera clientow
map<string, string> filesOnTheServer;   //mapa z sciazkami do plikow na serverze i nazwami plkow
string lastListOfFilesOnTheServer = ""; //ostatnia lista plikow na serwerze
map<int, string> clientsWithUsernames;  //mapa soketow z odpowiednimi usernames
mutex clientMutex;  //klient ma swoj watek

//funkcja do odczytu temperatury
string getCpuTemp() {
    ifstream file(TEMP_PATH);
    if (!file.is_open()) return "ERR";
    string line;
    getline(file, line);
    try {
        double temp = stod(line) / 1000.0;
        return to_string(temp).substr(0, 4);
    } catch (...) {
        return "N/A";
    }
}
//funkcja do doczytu obciazenai procesora
string getLoadOfTheProcesor()
{
    ifstream file(PROCESSOR_PATH);
    if(!file.is_open()){return "ERROR";}
    string line;
    getline(file, line);
    try {        
        return line;
    } catch (...) {
        return "N/A";
    }
}
//funkcja do pomiaru ilosci wykorzystywanego ramu
string getRamInfo()
{
    ifstream file("/proc/meminfo");
    string line, key;
    long value;
    string unit;
    
    long totalMem = 0;
    long availableMem = 0;

    if (!file.is_open()) return "ERR";

    while (getline(file, line)) 
    {
        stringstream ss(line);
        ss >> key >> value >> unit;
        
        if (key == "MemTotal:") 
        {
            totalMem = value;
        }
        else if (key == "MemAvailable:") 
        {
            availableMem = value;
        }
        if (totalMem > 0 && availableMem > 0) break;
    }

    if (totalMem == 0) return "0%";
    double usedMem = (double)(totalMem - availableMem);
    double percent = (usedMem / (double)totalMem) * 100.0;
    char buffer[20];
    snprintf(buffer, sizeof(buffer), "%.1f%%", percent);

    return string(buffer);
}
string getProcesorFreqInfo()
{
    ifstream file(FREQ_INFO);
    string line;
    long value;
    string unit;

    if (getline(file, line)) {
        try {
            long freqKHz = stol(line);
            long freqMHz = freqKHz / 1000;
            return to_string(freqMHz) + " MHz";
        } catch (...) {
            return "ERR";
        }
    }
    return "N/A";
}
//funckja zwracajaca odebrane bajty danych z sieci
unsigned long long get_net_bytes(string path) {
    ifstream file(path);
    string line;
    if (getline(file, line)) {
        try { return stoull(line); } catch (...) { return 0; }
    }
    return 0;
}
//funkcja zwracajaca szybkosc transmisji UL i DL
string getNetworkSpeed() {
    
    string interface = "wlan0";
    if (!fs::exists(RX_PATH)) {
        interface = "eth0";
    }

    unsigned long long rxStart = get_net_bytes(RX_PATH);
    unsigned long long txStart = get_net_bytes(TX_PATH);

    this_thread::sleep_for(chrono::seconds(1));

    unsigned long long rxEnd = get_net_bytes(RX_PATH);
    unsigned long long txEnd = get_net_bytes(TX_PATH);

    unsigned long long rxSpeed = (rxEnd > rxStart) ? (rxEnd - rxStart) : 0;
    unsigned long long txSpeed = (txEnd > txStart) ? (txEnd - txStart) : 0;

    double dl_mb = rxSpeed / 1024.0 / 1024.0;
    double up_mb = txSpeed / 1024.0 / 1024.0;

    char buffer[100];
    snprintf(buffer, sizeof(buffer), "DL: %.2f MB/s ; UP: %.2f MB/s", dl_mb, up_mb);

    return string(buffer);
}
//funkcja do pomiaru dysku ilosci dostepnego miejsca
string getDiskInfo() 
{
    struct statvfs stat;
    if (statvfs("/", &stat) != 0) return "0.0";
    if (stat.f_blocks == 0) return "0.0";

    double fraction = 1.0 - ((double)stat.f_bavail / (double)stat.f_blocks);
    double percent = fraction * 100.0;
    char buffer[16];
    snprintf(buffer, sizeof(buffer), "%.1f", percent);

    return string(buffer);
}

//funkcja do wysylania wiadomosci w trybie broadcast
void sendBroadcastMessage(string message)
{
    lock_guard<mutex> lock(clientMutex);

    message += "\n";

    for(int sock: clients)
    {
        send(sock, message.c_str(), message.size(), MSG_NOSIGNAL);
    }
}

//funkcja wywowylana w oddzielnym watku monitorHardware do monitorowania stanu servera
void hardwareMonitorThread()
{
    while(true)
    {
        string netSpeed = getNetworkSpeed(); 
        string cpuTemp = getCpuTemp();
        string cpuLoad = getLoadOfTheProcesor();
        string ramInfo = getRamInfo();
        string cpuFreq = getProcesorFreqInfo();
        string diskInfo = getDiskInfo();

        stringstream ss;
        ss << "[HARDWARE STATUS]|" << netSpeed << "|" << cpuTemp << "|" << cpuLoad << "|" << ramInfo << "|" << cpuFreq << "|" << diskInfo;
        string finalMessage = ss.str();
        sendBroadcastMessage(finalMessage);
        this_thread::sleep_for(chrono::seconds(5));
    }
}

//funkcja do wysylania wiadomosci w trybie unicast
void sendUnicastMessage(string message, int receiverSocket, int senderSocket)
{
    message += "\n";

    send(receiverSocket, message.c_str(), message.size(), MSG_NOSIGNAL);
    send(senderSocket, message.c_str(), message.size(), MSG_NOSIGNAL);
}

//funckja do aktualizowania listy aktywnych klientow
void clientsListUpdate(map<int, string> clientsList)
{
    string clientsNames = "";
    for(auto client:clientsList)
    {
        clientsNames = clientsNames + client.second + ',';
    }
    string finalMessage = "[CLIENTSLIST UPDATE] " + clientsNames;
    sendBroadcastMessage(finalMessage);
}
//funkcja do zapisaywania plikow na serwerze
void saveFileOnDisc(string fileName, string base64Data)
{
    try
    {
        string decodedBytes = base64_decode(base64Data);
        string folderName = "uploads";
        if(!fs::exists(folderName))
        {
            fs::create_directory(folderName);
        }

        fs::path fullPath = fs::path(folderName) / fileName;
        ofstream file(fullPath, ios::binary);

        if(file.is_open())
        {
            file.write(decodedBytes.data(), decodedBytes.size());
            file.close();
        }
    filesOnTheServer.insert({fullPath, fileName});
    }
    catch (const exception& e) {
        cerr << "[DISK CRITICAL ERROR] " << e.what() << endl;
    }
}
//funkcja do zakodowania pliku do base64 po filepath
string getBase64String(string filePath)
{
    ifstream file(filePath, ios::binary | ios::ate);
    if(!file.is_open()){return "";}

    streamsize size = file.tellg();
    file.seekg(0, ios::beg);

    vector<char> buffor(size);
    if(file.read(buffor.data(), size))
    {
        string encodedData = base64_encode((const unsigned char*)buffor.data(), buffor.size());
        return encodedData;
    }
    return "";
}
//funkcja do aktualizowania listy plikow na serwerze wysylajaca dane do klienta-gui
void updateFilesList()
{
    filesOnTheServer.clear();
    string directoryPath = "uploads";
    for(const auto& entry:fs::directory_iterator(directoryPath))
    {
        if(entry.is_regular_file())
        {
            string fullPath = entry.path().string();
            string fullName = entry.path().filename().string();
            filesOnTheServer.insert({fullPath, fullName});
        }
    }
}
//funckja wysylajaca dane do klienta z lista plikow korzystajaca z innego watku w celu unikniecia bledow w przypadku usuniecia plikow z servera przez administratora
void sendUpdatedFilesList()
{
    while(true)
    {
        this_thread::sleep_for(chrono::seconds(10));
        {
            lock_guard<mutex> lock(clientMutex);
            updateFilesList();
        }

        string currentList = "";
        for(auto file : filesOnTheServer)
        {
            double fileSize = fs::file_size(file.first) / 1024.0;
            string size = to_string(fileSize);
            currentList = currentList + file.second + ',' + size + ',';
        }

        if(currentList != lastListOfFilesOnTheServer)
        {
            cout << "[SERVER INFO] list of files on the server has changed" << endl;
            lastListOfFilesOnTheServer = currentList;
            string finalMessage = "[FILES ON THE SERVER]" + currentList;
            cout << finalMessage << endl;
            sendBroadcastMessage(finalMessage);
        }

    }
}
//funckja do jednorazowej aktualizacji fileListOnTheServer bez petli
void sendCurrentListWithoutThread()
{
    {
        lock_guard<mutex> lock(clientMutex);
        updateFilesList();
    }
    string list = "";
    for(auto file : filesOnTheServer)
    {
        double fileSize = fs::file_size(file.first) / 1024.0;
        string size = to_string(fileSize);
        list = list + file.second + ',' + size + ',';
        lastListOfFilesOnTheServer = list;
    }
    string finalMessage = "[FILES ON THE SERVER]" + list;
    cout << finalMessage << endl;
    sendBroadcastMessage(finalMessage);
}
//funckja do usuwania pliku z servera
void deleteFileFromTheServer(string fileName, string username)
{
    string folderName = "uploads";
    for(auto file : filesOnTheServer)
    {
        if(file.second == fileName)
        {
            string filePath = file.first;
            int status = remove(filePath.c_str());
            if(status != 0)
            {
                cout << "[" << username << "] tried to delete file:" << filePath << " but something went wrong" << endl;
            }
            else
            {
                cout << "[" << username << "] deleted file:" << filePath << endl;
            }
        }
    }
}
//funkcja oblsugujaca pojedynczego klienta
void handle_client(int clientSocket, string ipAddress)
{
    cout << "[SERVER INFO] New Client has joined: " << ipAddress << endl;

    {
        lock_guard<mutex> lock(clientMutex);
        clients.push_back(clientSocket);
    }

    string broadcastPrefix = "BROADCAST";
    string unicastPrefix = "UNICAST";
    string disconnectPrefix = "DISCONNECT";
    string fileTransferHeadersPrefix = "FILEHEADERS|";
    string fileTransferDataPrefix = "FILEDATA|";
    string fileDownloadedHeadersPrefix = "DOWNLOADFILE|";
    string fileDeletePrefix = "DELETE|";

    string fileName = "";
    string username = "";
    int dataSize = 0;
    char buffer[4096];
    int messageCount = 0;

    while (true) {
        memset(buffer, 0, 4096);
        int bytesReceived = recv(clientSocket, buffer, 4096, 0);

        if(bytesReceived <= 0)
        {           
            if(username.empty()) {
                cout << "[SERVER_INFO] Unknown client (" << ipAddress << ") disconnected before login." << endl;
            } else {
                cout << "[SERVER_INFO] Client " << username << " (" << ipAddress << ") has disconnected." << endl;
            }
            {
                lock_guard<mutex> lock(clientMutex);
                clients.erase(remove(clients.begin(), clients.end(), clientSocket), clients.end());
                if (!username.empty()) {
                    clientsWithUsernames.erase(clientSocket);
                }
            }
            if (!username.empty()) 
            {
                string finalMessage = "[" + username + "] has disconnected";
                sendBroadcastMessage(finalMessage);
                {
                    lock_guard<mutex> lock(clientMutex);
                    clientsListUpdate(clientsWithUsernames); 
                }
            }

            close(clientSocket);
            break;
        }

        string msg(buffer, bytesReceived);

        if (messageCount == 0) {
            username = msg;
            clientsWithUsernames.insert({clientSocket, username});
            cout << "[SERVER_INFO] " << username << " has connected to the server" << endl;
            string finalMessage = "[" + username + "]: has connected to the server";
            sendBroadcastMessage(finalMessage);
            clientsListUpdate(clientsWithUsernames);
            this_thread::sleep_for(chrono::milliseconds(100));
            sendCurrentListWithoutThread();
        }
        else if(msg.rfind(broadcastPrefix,0) == 0)
        {
            cout << "[BROADCAST] message from " << username << ":" << msg.substr(broadcastPrefix.length()) << endl;
            string finalMessage = "[" + username + "] send: " + msg.substr(broadcastPrefix.length());
            sendBroadcastMessage(finalMessage);
        }
        else if(msg.rfind(unicastPrefix) == 0)
        {
            for (auto receiverUser : clientsWithUsernames)
            {
                if(msg.find(receiverUser.second, unicastPrefix.length() + 1) == unicastPrefix.length() + 1)
                {
                    cout << "[UNICAST] message from " << username << " to " << receiverUser.second << " : " << msg.substr(unicastPrefix.length() + receiverUser.second.length() + 2) << endl;
                    string finalMessage = "[" + username + "] send to " + receiverUser.second + ": " + msg.substr(unicastPrefix.length() + receiverUser.second.length() + 2);
                    sendUnicastMessage(finalMessage, receiverUser.first, clientSocket);
                    break;
                }
            }
        }
        else if(msg.rfind(fileTransferHeadersPrefix, 0) == 0)
        {
            msg = msg.substr(fileTransferHeadersPrefix.length());
            int firstWall = msg.find("|");
            string sizeOfData = msg.substr(0, firstWall);
            dataSize = stoi(sizeOfData);

            int secondWall = msg.find("|", firstWall + 1);
            fileName = msg.substr(firstWall + 1, secondWall - firstWall - 1);
            cout << "[SERVER INFO] TRANSFER FILE: Size:" << dataSize << " kB" << " filename:" << fileName << endl;
            string handShakeMessage = "[FILE TRANSFER ACCEPTED]\n"; 
            send(clientSocket, handShakeMessage.c_str(), handShakeMessage.size(), MSG_NOSIGNAL);
        }
        else if(msg.rfind(fileTransferDataPrefix, 0) == 0)
        {
            string base64DataFile = msg.substr(fileTransferDataPrefix.length());
            int currentSize = base64DataFile.length();

            while(currentSize < dataSize)
            {
                memset(buffer, 0, 4096);
                int receivedBytes = recv(clientSocket, buffer, 4096, 0);
                if(receivedBytes <= 0)
                {
                    break;
                }
                base64DataFile.append(buffer, receivedBytes);
                currentSize += receivedBytes;
            }
            if(currentSize >= dataSize)
            {
                cout << "[SERVER INFO] File has been succesfully uploaded" << endl;
                saveFileOnDisc(fileName, base64DataFile);
            }
        }
        else if(msg.rfind(fileDownloadedHeadersPrefix, 0) == 0)
        {
            string fileName = msg.substr(fileDownloadedHeadersPrefix.length());
            if (!fileName.empty() && fileName.back() == '|') 
            {
                fileName.pop_back();
            }
            string filePath = "";

            for(auto file : filesOnTheServer)
            {
                if(file.second == fileName)
                {
                    filePath = file.first;
                }
            }

            if(fs::exists(filePath))
            {
                {
                    lock_guard<mutex> lock(clientMutex);
                    string dataToSend = getBase64String(filePath);
                    if(dataToSend.length() == 0) 
                    {
                        cout << "[ERROR] File empty or read error: " << fileName << endl;
                        continue;
                    }

                    long base64Size = dataToSend.length();

                    // [DOWNLOADING FILE FROM SERVER]|ROZMIAR|NAZWA|
                    string header = "[DOWNLOADING FILE FROM SERVER]|" + to_string(base64Size) + "|" + fileName + "|";
                    string finalMessage = header + dataToSend + "\n"; 

                    cout << "[SERVER INFO] Sending file: " << fileName << " (Size: " << base64Size << " bytes)" << endl;
                    send(clientSocket, finalMessage.c_str(), finalMessage.size(), MSG_NOSIGNAL);
                }
            }
            else
            {
                string errorMsg = "[ERROR] File not found: " + fileName;
                cout << errorMsg << endl;
            }
        }
        else if(msg.rfind(fileDeletePrefix, 0) == 0)
        {
            string fileNames = msg.substr(fileDeletePrefix.length());
            stringstream ss(fileNames);
            string fileName;
            char del = ',';
            while(getline(ss, fileName, del))
            {
                deleteFileFromTheServer(fileName, username);
            }
        }
        else if(msg.rfind(disconnectPrefix) == 0 || bytesReceived <= 0)
        {
            string finalMessage = "[" + username + "] Client " + ipAddress + " has disconnected";
            cout << "[SERVER_INFO] Client " << ipAddress << " " << username << " has disconnected" << endl;
            sendBroadcastMessage(finalMessage);
            {
                lock_guard<mutex> lock(clientMutex);
                clientsWithUsernames.erase(clientSocket);
                clients.erase(remove(clients.begin(), clients.end(), clientSocket), clients.end());
            }

            clientsListUpdate(clientsWithUsernames);
            close(clientSocket);
            break;
        }
        messageCount++;
    }
    close(clientSocket);
    {
        lock_guard<mutex> lock(clientMutex);
        clients.erase(remove(clients.begin(), clients.end(), clientSocket), clients.end());
    }
}

int main() {

    int listening = socket(AF_INET, SOCK_STREAM, 0);
    if (listening == -1)

    { cerr << "Socket error" << endl;
        return -1;
    }

    int opt = 1;
    setsockopt(listening, SOL_SOCKET, SO_REUSEADDR | SO_REUSEPORT, &opt, sizeof(opt));

    sockaddr_in hint;
    hint.sin_family = AF_INET;
    hint.sin_port = htons(PORT);
    hint.sin_addr.s_addr = INADDR_ANY;

    if (bind(listening, (sockaddr*)&hint, sizeof(hint)) == -1)
    {
        cerr << "Bind error" << endl;
        return -1;
    }
    if (listen(listening, SOMAXCONN) == -1)
    {
        cerr << "Listen error" << endl;
        return -1;
    }

    cout << "[SERVER INFO] MULTITHREAD SERVER STARTS ON PORT " << PORT << endl;

    thread filesMonitor(sendUpdatedFilesList);
    filesMonitor.detach();

    thread hardwareMonitor(hardwareMonitorThread);
    hardwareMonitor.detach();

    while (true) {
        sockaddr_in client;
        socklen_t clientSize = sizeof(client);

        int clientSocket = accept(listening, (sockaddr*)&client, &clientSize);
        if (clientSocket == -1) {
            cerr << "Błąd accept" << endl;
            continue;
        }
        char host[NI_MAXHOST] = {0};
        inet_ntop(AF_INET, &client.sin_addr, host, NI_MAXHOST);
        string clientIP(host);
        thread t(handle_client, clientSocket, clientIP);
        t.detach();
    }

    close(listening);
    return 0;
}
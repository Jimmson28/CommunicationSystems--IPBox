/**
 * Remote System Monitor - Concurrent Server (Multithreaded)
 * Obsługa WIELU klientów jednocześnie.
 */

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

using namespace std;

#define PORT 8080
#define TEMP_PATH "/sys/class/thermal/thermal_zone0/temp"

vector<int> clients;	//lista polaczonych do servera clientow
map<int, string> clientsWithUsernames;
mutex clientMutex;

// Funkcja pomocnicza do odczytu temperatury
string get_cpu_temp() {
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

void sendBroadcastMessage(string message)
{
	lock_guard<mutex> lock(clientMutex);

	for(int sock: clients)
	{
		send(sock, message.c_str(), message.size(), MSG_NOSIGNAL);
	}
}

void sendUnicastMessage(string message, int receiverSocket, int senderSocket)
{
	send(receiverSocket, message.c_str(), message.size(), MSG_NOSIGNAL);
	send(senderSocket, message.c_str(), message.size(), MSG_NOSIGNAL);
}

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
    string username;
    char buffer[4096];
    int messageCount = 0;

    while (true) {
    	memset(buffer, 0, 4096);
        int bytesReceived = recv(clientSocket, buffer, 4096, 0);
        string msg(buffer, bytesReceived);

        if (messageCount == 0) {
            username = msg;
            clientsWithUsernames.insert({clientSocket, username});
            cout << "[SERVER_INFO] " << username << " has connected to the server" << endl;
            string finalMessage = "[" + username + "]: has connected to the server";
            sendBroadcastMessage(finalMessage);
	    clientsListUpdate(clientsWithUsernames);
        }
        else if(msg.rfind(broadcastPrefix,0) == 0)
	{
            cout << "[BROADCAST] message from " << username << " " << msg.substr(broadcastPrefix.length()) << endl;
            string finalMessage = "[" + username + "] send: " + msg.substr(broadcastPrefix.length());
            sendBroadcastMessage(finalMessage);
        }
	else if(msg.rfind(unicastPrefix) == 0)
	{
		for (auto receiverUser : clientsWithUsernames)
		{
			int userNameLength = receiverUser.second.length();
			int unicastPrefixLength = unicastPrefix.length();
			if(msg.find(receiverUser.second, unicastPrefixLength + 1) == unicastPrefixLength + 1)
			{
	                        cout << "[UNICAST] message from " << username << " to " << receiverUser.second << " : " << msg.substr(unicastPrefixLength + userNameLength + 2) << endl;
				string finalMessage = "[" + username + "] send to " + receiverUser.second + ": " + msg.substr(unicastPrefixLength + userNameLength + 2);
				sendUnicastMessage(finalMessage, receiverUser.first, clientSocket);
				break;
			}
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

    while (true) {
        sockaddr_in client;
        socklen_t clientSize = sizeof(client);

        int clientSocket = accept(listening, (sockaddr*)&client, &clientSize);
        if (clientSocket == -1) {
            cerr << "Błąd accept" << endl;
            continue;
        }

        // Pobranie IP klienta (dla logów)
        char host[NI_MAXHOST] = {0};
        inet_ntop(AF_INET, &client.sin_addr, host, NI_MAXHOST);
        string clientIP(host);

        // Tworzymy nowy wątek (thread), przekazujemy mu funkcję handle_client 
        // oraz argumenty (socket i IP).
        thread t(handle_client, clientSocket, clientIP);
        
        // .detach() oznacza: "Leć wolno, nie będę na ciebie czekać w main()".
        // Dzięki temu wątek działa w tle, a pętla while wraca do początku czekać na kolejnego.
        t.detach(); 
    }

    close(listening);
    return 0;
}

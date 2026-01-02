using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace ChatLogic
{
    public class Server
    {
        private TcpListener tcpListener = null;
        private bool isServerRunning = false;
        private List<TcpClientState> listOfTcpClients = new List<TcpClientState>();

        public void startServer(string ipAdress, int port)
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse(ipAdress), port);
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(onAcceptTcpClient, tcpListener);             
                isServerRunning = true;

                while (isServerRunning)
                {
                    sendToAllViaTCP();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void onAcceptTcpClient(IAsyncResult result)
        {
            TcpListener tcpListener = (TcpListener)result.AsyncState;
            tcpListener.BeginAcceptTcpClient(onAcceptTcpClient, tcpListener);

            TcpClient client = tcpListener.EndAcceptTcpClient(result);
            NetworkStream networkStream = client.GetStream();
            var EndPoint = client.Client.RemoteEndPoint as IPEndPoint;

            TcpClientState clientState = new TcpClientState();
            lock (listOfTcpClients) 
            {
                clientState.TcpClient = client;
                clientState.NetworkStrem = networkStream;
                clientState.Buffor = new byte[4096];
                clientState.ClientID = listOfTcpClients.Count;
                clientState.Connected = true;
                listOfTcpClients.Add(clientState);
            }          
            Console.WriteLine("[SYSTEM MESSAGE] There is new Client");
            networkStream.BeginRead(clientState.Buffor, 0, clientState.Buffor.Length, onReceivingDataViaTCP, clientState);            
        }
        private void onReceivingDataViaTCP(IAsyncResult result) 
        {
            TcpClientState clientState = (TcpClientState)result.AsyncState;
            try
            {
                if (clientState.TcpClient == null || !clientState.TcpClient.Connected) return;
                int bytesRead = clientState.NetworkStrem.EndRead(result);
                if (bytesRead > 0)
                {
                    byte[] data = new byte[bytesRead];
                    Array.Copy(clientState.Buffor, data, bytesRead);
                    string Message = Encoding.UTF8.GetString(data);

                    if (clientState.messagesCount == 0)
                    {
                        clientState.Username = Message;
                        string messageToSend = "[System Message] Client " + clientState.Username + " joined chat";
                        Console.WriteLine(messageToSend);
                        clientState.messagesCount++;
                        BroadcastTcpSendMessage(messageToSend);
                    }
                    else if (clientState.messagesCount > 0)
                    {
                        string messageToSend = "[System Message] " + clientState.Username + " send " + Message;
                        Console.WriteLine(messageToSend);
                        clientState.messagesCount++;
                        BroadcastTcpSendMessage(messageToSend);
                    }
                    clientState.NetworkStrem.BeginRead(clientState.Buffor, 0, clientState.Buffor.Length, onReceivingDataViaTCP, clientState);
                }
                else
                {
                    disconnectClient(clientState);
                }
            }
            catch (Exception ex)
            {
                disconnectClient(clientState);
            }
        }
        private void BroadcastTcpSendMessage(string message)
        {
            byte[] dataToSend = Encoding.UTF8.GetBytes(message);
            lock (listOfTcpClients)
            {
                foreach (var client in listOfTcpClients)
                {
                    try
                    {
                        client.NetworkStrem.Write(dataToSend, 0, dataToSend.Length);
                    }
                    catch
                    {
                        //
                    }
                }
            }           
        }
        private void disconnectClient(TcpClientState clientState)
        {
            lock (listOfTcpClients)
            {
                listOfTcpClients.RemoveAll(x => x.ClientID == clientState.ClientID);

                Console.WriteLine($"Client {clientState.Username} has disconnected");           
                if (clientState.TcpClient != null)
                {
                    clientState.Connected = false;
                    clientState.TcpClient.Close();
                }               
            }
        }
        private void sendToAllViaTCP()
        {        
            string msg = Console.ReadLine();
            BroadcastTcpSendMessage("SERVER: " + msg);
        }
    }
}

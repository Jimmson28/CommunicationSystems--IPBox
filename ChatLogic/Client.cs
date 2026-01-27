using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatLogic
{
    public class Client
    {
        private TcpClient tcpClient = null;
        private NetworkStream networkStream = null;
        private byte[] receivingBuffer = null;
        public event Action<string> OnMessageReceived;
        public event Action<string> OnLog;
        public bool isClientConnected = false;

        public void connectToServer(string ip, int port)
        {
            tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);
            networkStream = tcpClient.GetStream();
            receivingBuffer = new byte[4096];
            networkStream.BeginRead(receivingBuffer, 0, receivingBuffer.Length, OnReceivingDataViaTCP, null);
            OnLog?.Invoke($"Connected to server {ip}:{port}");
            isClientConnected = true;
        }
        public void disconnect()
        {
            try
            {
                tcpClient.Close();
                isClientConnected = false;
            }
            catch (Exception ex)
            {
                OnLog?.Invoke("Disconnecting error: " + ex.Message);
            }
        }
        public void SendTcp(string message)
        {
            if (tcpClient == null || !tcpClient.Connected)
            {
                OnLog?.Invoke("Connection Error TCP!");
                return;
            }
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                networkStream.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                OnLog?.Invoke("Sending Error TCP: " + ex.Message);
            }
        }
        private void OnReceivingDataViaTCP(IAsyncResult result)
        {
            try
            {
                if (networkStream == null) return;

                int bytesRead = networkStream.EndRead(result);
                if (bytesRead > 0)
                {
                    byte[] data = new byte[bytesRead];
                    Array.Copy(receivingBuffer, data, bytesRead);
                    string message = Encoding.UTF8.GetString(data);
                    
                    OnMessageReceived?.Invoke($"{message}");
                    networkStream.BeginRead(receivingBuffer, 0, receivingBuffer.Length, OnReceivingDataViaTCP, null);
                }
                else
                {
                    OnLog?.Invoke("Server has lost connection via TCP.");
                    disconnect();
                }
            }
            catch (Exception ex)
            {
                if (networkStream != null && networkStream.CanRead)
                {
                    OnLog?.Invoke("Receiving Error TCP: " + ex.Message);
                }
            }
        }
    }
}

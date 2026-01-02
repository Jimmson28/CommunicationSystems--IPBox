using System.Net.Sockets;
using System.Net;

namespace ChatLogic
{
    public class TcpClientState
    {
        public int ClientID {get; set;}
        public NetworkStream NetworkStrem {get; set;}
        public byte[] Buffor { get; set;}
        public TcpClient TcpClient {get; set;}
        public IPEndPoint EndPoint {get; set;}
        public string Username {get; set;}

        public int messagesCount = 0;
        public bool Connected {get; set;}
    }
}

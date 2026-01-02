using ChatLogic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("IPCHAT SERVER");
        Console.WriteLine("IP:...");
        string ipAddress = Console.ReadLine();
        Console.WriteLine("PORT:...");
        int port = int.Parse(Console.ReadLine());
        Server server = new Server();
        server.startServer(ipAddress, port);
        Console.ReadLine();
    }
}
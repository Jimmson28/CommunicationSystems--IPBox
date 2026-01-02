using ChatLogic;
using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("IPCHAT - Client");
        RunClient();

    }

    public static void RunClient()
    {
        Client client = new Client();
        client.OnLog += (msg) => Console.WriteLine($"[LOG]: {msg}");
        client.OnMessageReceived += (msg) => Console.WriteLine(msg);
        client.connectToServer("192.168.18.13", 10000);

        Console.WriteLine("Give your username");
        string msg = Console.ReadLine();
        client.SendTcp(msg);
        bool running = true;
        Console.WriteLine("You are connected!");
        while (running)
        {
            msg = Console.ReadLine();
            client.SendTcp(msg);

        }
    }
}
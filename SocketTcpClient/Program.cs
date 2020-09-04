using System;
using System.Dynamic;

namespace SocketTcpClient
{
    class Program
    {
        static int port = 8005;
        static string host = "127.0.0.1";

        static void Main(string[] args)
        {
            Client client = new Client(port, host);
            client.Start();
            Console.ReadKey();
        }
    }
}

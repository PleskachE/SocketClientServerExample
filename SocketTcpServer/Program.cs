using System;

namespace SocketTcpServer
{
    class Program
    {
        static int port = 8005;
        static string host = "127.0.0.1";

        static void Main(string[] args)
        {
            MainServer server = new MainServer(port, host);
            server.Start();
            Console.ReadKey();
        }
    }
}

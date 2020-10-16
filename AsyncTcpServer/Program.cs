using System;

namespace AsyncTcpServer
{
    class Program
    {
        static int port = 8005;
        static string host = "127.0.0.1";

        static void Main(string[] args)
        {
            var server = new AsyncServer(port, host);
            server.ListenSocket();
            Console.ReadKey();
        }
    }
}

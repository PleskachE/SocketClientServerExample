using System;
using System.Net;
using System.Text;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            string Host = Dns.GetHostName();
            string IP = Dns.GetHostEntry(Host).AddressList[1].ToString();
            Console.WriteLine(Host);
            Console.WriteLine(IP);
            string host = "DESKTOP-AETH2KS";
            string ip = "";
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketClientServerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IPHostEntry host1 = Dns.GetHostEntry("www.microsoft.com");
            Console.WriteLine(host1.HostName);
            foreach (IPAddress ip in host1.AddressList)
                Console.WriteLine(ip.ToString());

            Console.WriteLine();

            IPHostEntry host2 = Dns.GetHostEntry("google.com");
            Console.WriteLine(host2.HostName);
            foreach (IPAddress ip in host2.AddressList)
                Console.WriteLine(ip.ToString());
            Console.ReadKey();
        }
    }
}

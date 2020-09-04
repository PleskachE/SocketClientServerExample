using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTcpClient
{ 
    public class Client
    {
        private IPEndPoint _ipPoint;

        public Client(int port, string host)
        {
            _ipPoint = new IPEndPoint(IPAddress.Parse(host), port);
        }

        private void Connect(Socket socket)
        {
            try
            {
                socket.Connect(_ipPoint);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("Введите сообщение:");
        }

        private void SendMessage(Socket socket)
        {
            string message = Console.ReadLine();
            byte[] data = Encoding.Unicode.GetBytes(message);
            try 
            {
                socket.Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void GetResponce(Socket socket)
        {
            byte[] data = new byte[256];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                try
                {
                    bytes = socket.Receive(data, data.Length, 0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (socket.Available > 0);
            Console.WriteLine("ответ сервера: " + builder.ToString());
        }

        public void Start()
        {
            while(true)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Connect(socket);
                SendMessage(socket);
                GetResponce(socket);
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}

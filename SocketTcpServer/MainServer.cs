using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketTcpServer
{
    public class MainServer
    {
        private IPEndPoint _ipPoint;
        private Socket _listenSocket;

        public MainServer(int port, string host)
        {
            this._ipPoint = new IPEndPoint(IPAddress.Parse(host), port);
            this._listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._listenSocket.Bind(_ipPoint);
        }

        private Socket ListenSocket()
        {
            _listenSocket.Listen(10);//!!!!!!!
            Socket handler = _listenSocket.Accept();
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            byte[] data = new byte[256]; 
            do
            {
                try
                {
                    bytes = handler.Receive(data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            while (handler.Available > 0);
            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
            return handler;
        }

        private void Response(Socket handler)
        {
            string message = "ваше сообщение доставлено";
            byte[] data = Encoding.Unicode.GetBytes(message);
            handler.Send(data);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }

        public void Start()
        {
            Console.WriteLine("Сервер запущен. Ожидание подключений...");
            while (true)
            {
                Response(ListenSocket());
            }
        }
    }
}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketTcpServer
{
    public class AsyncServer
    {
        private IPEndPoint _ipPoint;
        private static ManualResetEvent _allDone;
        private Socket _listenSocket;

        public AsyncServer(int port, string host)
        {
            _ipPoint = new IPEndPoint(IPAddress.Parse(host), port);
            _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listenSocket.Bind(_ipPoint);
            _allDone = new ManualResetEvent(false);
        }

        public void ListenSocket()
        {
            Console.WriteLine("Сервер запущен. Ожидание подключений...");
            _listenSocket.Listen(10);
            while (true)
            {
                _allDone.Reset();
                _listenSocket.BeginAccept(new AsyncCallback(AcceptCallback), _listenSocket);
                _allDone.WaitOne();
            }
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            _allDone.Set();
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            StateObject state = new StateObject();
            state.socket = handler;
            handler.BeginReceive(state.date, 0, StateObject.BytesCounter, 0, new AsyncCallback(ReadCallback), state);
        }

        private static void ReadCallback(IAsyncResult ar)
        {
            String message = String.Empty;
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.socket;
            int bytes = handler.EndReceive(ar);
            if (bytes > 0)
            {
                do
                {
                    state.builder.Append(Encoding.Unicode.GetString(state.date, 0, bytes));
                    message = state.builder.ToString();
                }
                while (handler.Available > 0);
                Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + message);
                Send(handler);
            }
        }

        private static void Send(Socket handler)
        {
            string message = "ваше сообщение доставлено";
            byte[] byteData = Encoding.Unicode.GetBytes(message);  
            handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            Socket handler = (Socket)ar.AsyncState;
            handler.EndSend(ar);
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
    }
}
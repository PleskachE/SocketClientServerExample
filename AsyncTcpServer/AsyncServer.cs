using Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AsyncTcpServer
{
    public class AsyncServer
    {
        private readonly IPEndPoint _ipPoint;
        private static ManualResetEvent _allStream;
        private readonly Socket _listenSocket;
        private static string _allMessage;
        private static SystemMessage _systemMessage;

        public AsyncServer(int port, string host)
        {
            _systemMessage = new SystemMessage();
            _ipPoint = new IPEndPoint(IPAddress.Parse(host), port);
            _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _listenSocket.Bind(_ipPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            _allStream = new ManualResetEvent(false);
        }

        public void ListenSocket()
        {
            Console.WriteLine(_systemMessage.SystemMessages[0]);
            _listenSocket.Listen(10);
            while (true)
            {
                _allStream.Reset();
                _listenSocket.BeginAccept(new AsyncCallback(AcceptCallback), _listenSocket);
                _allStream.WaitOne();
            }
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            _allStream.Set();
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            StateObject state = new StateObject();
            state.socket = handler;
            // что такое 0
            handler.BeginReceive(state.date, 0, StateObject.BytesCounter, 0, new AsyncCallback(ReadCallback), state);
        }

        private static void ReadCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.socket;
            int bytes = 0;
            try
            {
                bytes = handler.EndReceive(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (bytes > 0)
            {
                string message;
                do
                {
                    state.builder.Append(Encoding.Unicode.GetString(state.date, 0, bytes));
                    message = state.builder.ToString();
                }
                while (handler.Available > 0);
                // очень плохое решение, так лучше не делать
                if (message != _systemMessage.SystemMessages[1])
                {
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + message);
                    _allMessage += DateTime.Now.ToShortTimeString() + ": " + message + "\n";
                }
                Send(handler);
            }
        }

        private static void Send(Socket handler)
        {
            try
            {
                byte[] byteData = Encoding.Unicode.GetBytes(_allMessage);
                handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
            }
            catch
            {
                byte[] byteData = Encoding.Unicode.GetBytes(_systemMessage.SystemMessages[3]);
                handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
            }
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

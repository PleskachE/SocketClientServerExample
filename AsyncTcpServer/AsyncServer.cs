using Common;
using Model;
using Repositoryes;
using Repositoryes.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace AsyncTcpServer
{
    public class AsyncServer
    {
        private const int _Connectionlimit = 10;

        private IPEndPoint _ipPoint;
        private static ManualResetEvent _allStream;
        private Socket _listenSocket;
        private static IGenericRepository<Message> _allMessage;

        public AsyncServer(int port, string host)
        {
            _allMessage = new GenericRepository<Message>(new List<Message>());
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
            Console.WriteLine(SystemMessage.ServerIsRunning());
            _listenSocket.Listen(_Connectionlimit);
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
            var listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            int bytes = handler.Available;
            var state = new StateObject(bytes);
            state.socket = handler;
            handler.BeginReceive(state.date, 0, bytes, 0, new AsyncCallback(ReadCallback), state);
        }

        private static void ReadCallback(IAsyncResult ar)
        {
            var state = (StateObject)ar.AsyncState;
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
                var message = new Message();
                Object obj = ConverterBytes.ByteArrayToObject(state.date);
                message = (Message)obj;
                if (message.MessageType == MessageTypes.Message)
                {
                    Console.WriteLine(message.User.Name + " : " + message.DateTime + " : " + message.Text);
                    _allMessage.Create(message);
                }
                Send(handler);
            }
        }

        private static void Send(Socket handler)
        {
            try
            {
                byte[] byteData = ConverterBytes.ObjectToByteArray(_allMessage);
                handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
            }
            catch
            {
                byte[] byteData = ConverterBytes.ObjectToByteArray(new Message(new User("server"), 
                    SystemMessage.NoConnection()));
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
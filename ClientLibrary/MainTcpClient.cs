using ClientLibrary.Interfaces;
using Common;
using System;
using System.Net;
using System.Net.Sockets;
using Model;
using Repositoryes.Interfaces;
using Repositoryes;
using System.Collections.Generic;

namespace ClientLibrary
{
    public class MainTcpClient : IClient
    {
        private const int _port = 8005;
        private const string _host = "127.0.0.1";

        private IPEndPoint _ipPoint;
        private Socket _socket;
        private IGenericRepository<Message> _serverMessages;

        public MainTcpClient()
        {
            _serverMessages = new GenericRepository<Message>(new List<Message>());
            _ipPoint = new IPEndPoint(IPAddress.Parse(_host), _port);
        }

        public IGenericRepository<Message> Start(Message userMessage)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _socket.Connect(_ipPoint);
            }
            catch
            {
                _serverMessages.Create(new Message(new User("server"), SystemMessage.NoConnection()));
                return _serverMessages;
            }
            SendMessage(userMessage);
            GetCallback();
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            return _serverMessages;
        }

        private void SendMessage(Message userMessage)
        {
            byte[] data = ConverterBytes.ObjectToByteArray(userMessage);
            try
            {
                _socket.Send(data);
            }
            catch (Exception ex)
            {
                _serverMessages.Create(new Message(new User("server"), ex.Message));
            }
        }

        private void GetCallback()
        {
            byte[] data = null;
            try
            {
                _socket.Receive(data = new byte[_socket.Available], _socket.Available, 0);
                _socket.Receive(data = new byte[_socket.Available], _socket.Available, 0);
                Object obj = ConverterBytes.ByteArrayToObject(data);
                _serverMessages = (GenericRepository<Message>)obj;
            }
            catch (Exception ex)
            {
                _serverMessages.Create(new Message(new User("server"), ex.Message));
            }
        }
    }
}

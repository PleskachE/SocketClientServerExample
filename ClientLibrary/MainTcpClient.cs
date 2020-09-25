using Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientLibrary
{
    public class MainTcpClient
    {
        private const int _port = 8005;
        private const string _host = "127.0.0.1";

        private IPEndPoint _ipPoint;
        private User _user;
        private Socket _socket;
        private string _serverMessage;
        private SystemMessage _systemMessage;

        public MainTcpClient(User User)
        {
            _systemMessage = new SystemMessage();
            _user = User;
            _ipPoint = new IPEndPoint(IPAddress.Parse(_host), _port);
        }

        public string Start(string clientMessage)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                _socket.Connect(_ipPoint);
            }
            catch
            {
                return _systemMessage.SystemMessages[2];
            }
            SendMessage(clientMessage);
            GetCallback();
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            return _serverMessage;
        }

        private void SendMessage(string clientMessage)
        {
            string message;
            byte[] data;
            if (clientMessage == _systemMessage.SystemMessages[1])
                data = Encoding.Unicode.GetBytes(clientMessage);
            else
            {
                message = _user.Name + " : " + clientMessage;
                data = Encoding.Unicode.GetBytes(message);
            }
            try
            {
                _socket.Send(data);
            }
            catch (Exception ex)
            {
                _serverMessage = ex.Message;
            }
        }

        private void GetCallback()
        {
            byte[] data = new byte[256];
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                try
                {
                    bytes = _socket.Receive(data, data.Length, 0);
                }
                catch (Exception ex)
                {
                    _serverMessage = ex.Message;
                }
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            }
            while (_socket.Available > 0);
            _serverMessage = builder.ToString();
        }
    }
}

using System.Net.Sockets;

namespace AsyncTcpServer
{
    public class StateObject
    {
        public int BytesCounter;
        public byte[] date;
        public Socket socket = null;
       
        public StateObject(int bytesCounter)
        {
            this.BytesCounter = bytesCounter;
            date = new byte[BytesCounter];
        }
    }
}

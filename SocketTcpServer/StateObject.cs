using System.Net.Sockets;
using System.Text;

namespace SocketTcpServer
{
    public class StateObject
    {
        public const int BytesCounter = 256;  
        public byte[] date = new byte[BytesCounter];
        public StringBuilder builder = new StringBuilder();
        public Socket socket = null;
    }
}

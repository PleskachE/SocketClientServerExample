using ClientLibrary;
using ClientLibrary.Interfaces;
using Common;
using Service.Intefaces;

namespace Service
{
    public class ClientService : IClientService
    {
        private IClient _client;

        public ClientService(User User)
        {
            _client = new MainTcpClient(User);
        }

        public string Listen(string message)
        {
            return _client.Start(message);
        }
    }
}

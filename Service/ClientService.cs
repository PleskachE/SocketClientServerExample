using ClientLibrary;
using ClientLibrary.Interfaces;
using Model;
using Repositoryes.Interfaces;
using Service.Intefaces;

namespace Service
{
    public class ClientService : IClientService
    {
        private IClient _client;

        public ClientService()
        {
            _client = new MainTcpClient();
        }

        public string Listen(Message message)
        {
            string serverMessage = null;
            IGenericRepository<Message> reposMessage = _client.Start(message);
            foreach (Message item in reposMessage.Get())
                serverMessage += item.User.Name + " - " + item.Text + "\n";
            return serverMessage;
        }
    }
}

using ClientLibrary;
using ClientLibrary.Interfaces;
using Model;
using Repositoryes.Interfaces;
using Service.Intefaces;
using System.Collections;

namespace Service
{
    public class ClientService : IClientService
    {
        private IClient _client;

        public ClientService()
        {
            _client = new MainTcpClient();
        }

        public IEnumerable Listen(Message message)
        {
            IGenericRepository<Message> messageRepository = _client.Start(message);
            return messageRepository.Get();
        }
    }
}

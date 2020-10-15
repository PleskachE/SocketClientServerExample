using Model;
using Repositoryes.Interfaces;

namespace ClientLibrary.Interfaces
{
    public interface IClient
    {
        IGenericRepository<Message> Start(Message userMessage);
    }
}

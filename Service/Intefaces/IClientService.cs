using Model;
using Repositoryes.Interfaces;
using System.Collections;

namespace Service.Intefaces
{
    public interface IClientService
    {
        IGenericRepository<Message> Listen(Message message);
    }
}

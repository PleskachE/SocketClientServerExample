using Model;
using System.Collections;

namespace Service.Intefaces
{
    public interface IClientService
    {
        IEnumerable Listen(Message message);
    }
}

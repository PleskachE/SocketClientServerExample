using System;
using System.Collections.Generic;

namespace Repositoryes.Interfaces
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> Get();
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Create(T item);
        void Remove(T item);
    }
}

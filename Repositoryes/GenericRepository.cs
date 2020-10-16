using Repositoryes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositoryes
{
    [Serializable]
    public class GenericRepository<T> : IGenericRepository<T>
    {
        private ICollection<T> _collection { get; set; }

        public GenericRepository(ICollection<T> collection)
        {
            _collection = collection;
        }

        public IEnumerable<T> Get()
        {
            return _collection;
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return _collection.Where(predicate).ToList();
        }

        public void Create(T item)
        {
            _collection.Add(item);
        }
        
        public void Remove(T item)
        {
            _collection.Remove(item);
        }
    }
}

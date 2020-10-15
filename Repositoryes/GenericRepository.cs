using Repositoryes.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repositoryes
{
    [Serializable]
    public class GenericRepository<T> : IGenericRepository<T>
    {
        private ObservableCollection<T> _collection { get; set; }
        public GenericRepository()
        {
            _collection = new ObservableCollection<T>();
        }

        public GenericRepository(ObservableCollection<T> collection)
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

        public T FindById(int id)
        {
            return _collection[id];
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

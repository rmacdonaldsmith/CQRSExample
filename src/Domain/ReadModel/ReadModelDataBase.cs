using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace CQRSSample.Domain.ReadModel
{
    public interface IReadModelDataBase
    {
        IQueryable<KeyValuePair<string, IMessage>> Queryable();
        IMessage Get(string key);
        void Insert(string key, IMessage item);
        void Save(string key, IMessage item);
    }

    public sealed class ReadModelDataBase : IReadModelDataBase
    {
        private Dictionary<string, IMessage> _dataBase = new Dictionary<string, IMessage>();

        public IQueryable<KeyValuePair<string, IMessage>> Queryable()
        {
            return _dataBase.AsQueryable();
        }

        public IMessage Get(string key)
        {
            return _dataBase[key];
        }

        public void Insert(string key, IMessage item)
        {
            IMessage dto;
            if(_dataBase.TryGetValue(key, out dto))
                throw new InvalidOperationException(string.Format("The key '{0}' already exists, insert fails", key));

            _dataBase[key] = item;
        }

        public void Save(string key, IMessage item)
        {
            _dataBase[key] = item;
        }

        public void Delete(string key)
        {
            if (_dataBase.ContainsKey(key))
            {
                _dataBase.Remove(key);
            }
        }
    }
}

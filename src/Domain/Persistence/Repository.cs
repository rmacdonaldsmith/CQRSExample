using System;
using Domain;

namespace CQRSSample.Domain.Persistence
{
    public interface IRepository<out T> where T : AggregateRoot, new()
    {
        void Save(AggregateRoot aggregate, int expectedVersion);
        T GetById(Guid id);
    }

    public class Repository<T> : IRepository<T> where T : AggregateRoot, new() //new here means that we can instantiate the agg instance using a default constructor
    {
        private readonly IEventStore _storage;

        public Repository(IEventStore storage)
        {
            _storage = storage;
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            _storage.Save(aggregate.Id, aggregate.GetUncommittedEvents, expectedVersion);
        }

        public T GetById(Guid id)
        {
            var obj = new T();
            var events = _storage.GetForAggregate(id);
            obj.LoadsFromHistory(events);
            return obj;
        }
    }
}

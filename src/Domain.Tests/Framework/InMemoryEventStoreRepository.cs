using System;
using System.Collections.Generic;
using CQRSSample.Domain.Persistence;
using Contracts;
using Domain;

namespace CQRSSample.Domain.Tests.Framework
{
    public sealed class InMemoryEventStoreRepository<T> : ITestableRepository<T> where T : AggregateRoot, new()
    {
        private readonly IEventStore _inmemoryEventStore;
        private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

        public InMemoryEventStoreRepository(IEnumerable<IEvent> seedEvents)
        {
            _inmemoryEventStore = new FakeEventStore(seedEvents);
        }

        public void Save(AggregateRoot aggregate, int expectedVersion)
        {
            //while testing, don't persist events to the event store, we will just keep track of them here
            _uncommittedEvents.AddRange(aggregate.GetUncommittedEvents);
            aggregate.ClearUncommittedEvents();
        }

        public T GetById(Guid id)
        {
            var aggregate = new T(); //this is why we have the "where T : new()" restrictions - so that we can new one up here without the need for a factory. Feel free to use factory, whatever instead.
            aggregate.LoadsFromHistory(_inmemoryEventStore.GetForAggregate(id));
            return aggregate;
        }

        public IEnumerable<IEvent> SavedEvents
        {
            get
            {
                return _uncommittedEvents;
            }
        }
    }
}

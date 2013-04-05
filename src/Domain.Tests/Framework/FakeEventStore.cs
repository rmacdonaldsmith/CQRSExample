using System;
using System.Collections.Generic;
using CQRSSample.Domain.Persistence;
using Contracts;

namespace CQRSSample.Domain.Tests.Framework
{
    public sealed class FakeEventStore : IEventStore
    {
        private readonly List<IEvent> _events = new List<IEvent>();  //assumes that all events are for the same aggregate

        public FakeEventStore(IEnumerable<IEvent> seedEvents)
        {
            _events.AddRange(seedEvents);
        }

        public void Save(Guid aggregateId, IEnumerable<IEvent> events, int expectedRevision)
        {
            _events.AddRange(events);
        }

        public IEnumerable<IEvent> GetForAggregate(Guid aggregateId)
        {
            return _events;
        }
    }
}

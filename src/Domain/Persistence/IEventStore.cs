using System;
using System.Collections.Generic;
using Contracts;

namespace CQRSSample.Domain.Persistence
{
    public interface IEventStore
    {
        void Save(Guid aggregateId, IEnumerable<IEvent> events, int expectedRevision);

        IEnumerable<IEvent> GetForAggregate(Guid aggregateId);
    }
}

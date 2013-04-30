using System;
using System.Collections.Generic;
using System.Linq;
using CQRSSample.Domain.ServiceBus;
using Contracts;

namespace CQRSSample.Domain.Persistence
{
    public class EventStore : IEventStore
    {
        private readonly IPublishEvents _serviceBus;

        private struct EventInfo
        {

            public readonly IEvent EventData;
            public readonly Guid Id;

            public EventInfo(Guid id, IEvent eventData)
            {
                EventData = eventData;
                Id = id;
            }
        }

        public EventStore(IPublishEvents serviceBus)
        {
            _serviceBus = serviceBus;
        }

        private readonly Dictionary<Guid, List<EventInfo>> _events = new Dictionary<Guid, List<EventInfo>>();

        public void Save(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion)
        {
            List<EventInfo> eventDescriptors;
            if (!_events.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<EventInfo>();
                _events.Add(aggregateId, eventDescriptors);
            }

            foreach (var @event in events)
            {
                eventDescriptors.Add(new EventInfo(aggregateId, @event));
                _serviceBus.Publish(@event);
            }
        }

        public IEnumerable<IEvent> GetForAggregate(Guid aggregateId)
        {
            List<EventInfo> eventInfos;
            if (!_events.TryGetValue(aggregateId, out eventInfos))
            {
                throw new AggregateNotFoundException();
            }
            return eventInfos.Select(info => info.EventData).ToList();
        }
    }
}

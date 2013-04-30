using System;
using System.Collections.Generic;
using System.Linq;
using CQRSSample.Domain.ServiceBus;
using Contracts;

namespace CQRSSample.Domain.Persistence
{
    /// <summary>
    /// This event store implementation is pretty close to Greg Youngs SimplestPossibleThing on GitHub(https://github.com/gregoryyoung/m-r/tree/master/SimpleCQRS);
    /// There are several OSS event stores out there that you can use, or you can build your own!
    /// https://github.com/joliver/EventStore
    /// http://geteventstore.com/
    /// </summary>
    public class EventStore : IEventStore
    {
        private readonly IPublishEvents _serviceBus;

        /// <summary>
        /// This struct acts as an envelope for event.
        /// The Id and Version data is used as meta data by the event store.
        /// Real event store implementations contain headers where meta data is stored,
        /// and you can add your own data to the headers too. Useful for recording the user that
        /// caused the event, or the actual command that caused the event, or other data that is 
        /// not related directly with the event.
        /// </summary>
        private struct EventInfo
        {
            public readonly IEvent EventData;
            public readonly Guid Id;
            public readonly int Version;

            public EventInfo(Guid id, IEvent eventData, int version)
            {
                EventData = eventData;
                Id = id;
                Version = version;
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
            else if (eventDescriptors[eventDescriptors.Count - 1].Version != expectedVersion && expectedVersion != -1)
            {
                //this prevents us from trying to duplicate the same event to the event store
                //this could happen in a multi-threaded environment or if you are 
                //using a messageing framework with at-least-once delivery semantics
                throw new ConcurrencyException();
            }

            var i = expectedVersion;
            foreach (var @event in events)
            {
                i++;
                eventDescriptors.Add(new EventInfo(aggregateId, @event, i));
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

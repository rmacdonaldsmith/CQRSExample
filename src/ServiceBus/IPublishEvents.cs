using Contracts;

namespace ServiceBus
{
    public interface IPublishEvents
    {
        void Publish<TEvent>(TEvent evnt) where TEvent : IEvent;
    }
}

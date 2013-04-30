using Contracts;

namespace CQRSSample.Domain.ServiceBus
{
    public interface IPublishEvents
    {
        void Publish<TEvent>(TEvent evnt) where TEvent : class, IEvent;
    }
}

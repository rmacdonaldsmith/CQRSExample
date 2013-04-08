using Contracts;

namespace CQRSSample.Domain.EventHandlers
{
    public interface Handles<in TEvent> where TEvent : IEvent
    {
        void Handle(TEvent evnt);
    }
}

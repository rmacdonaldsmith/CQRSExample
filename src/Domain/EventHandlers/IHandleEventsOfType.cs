using Contracts;

namespace CQRSSample.Domain.EventHandlers
{
    public interface IHandleEventsOfType<in TEvent> where TEvent : IEvent
    {
        void Handle(TEvent evnt);
    }
}

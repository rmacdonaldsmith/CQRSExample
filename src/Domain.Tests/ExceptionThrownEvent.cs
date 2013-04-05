using Contracts;
using Domain;

namespace CQRSSample.Domain.Tests
{
    public sealed class ExceptionThrownEvent : IEvent
    {
        public DomainException Exception { get; set; }

        public ExceptionThrownEvent(DomainException exception)
        {
            Exception = exception;
        }
    }
}

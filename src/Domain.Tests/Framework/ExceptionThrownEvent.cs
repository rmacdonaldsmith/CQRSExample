using System;
using Contracts;

namespace CQRSSample.Domain.Tests.Framework
{
    public sealed class ExceptionThrownEvent : IEvent
    {
        public string Exception { get; set; }
        public string Message { get; set; }

        public ExceptionThrownEvent(Exception exception)
        {
            Exception = exception.GetType().ToString();
            Message = exception.Message;
        }
    }
}

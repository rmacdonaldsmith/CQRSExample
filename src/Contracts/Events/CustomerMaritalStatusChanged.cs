using System;

namespace Contracts.Events
{
    public sealed class CustomerMaritalStatusChanged : IEvent
    {
        public Guid CustomerId { get; set; }

        public string MaritalStatus { get; set; }
    }
}

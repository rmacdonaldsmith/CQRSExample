using System;

namespace Contracts.Events
{
    public sealed class CustomerMovedToNewAddress : IEvent
    {
        public Guid CustomerId { get; set; }

        public string HouseNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}

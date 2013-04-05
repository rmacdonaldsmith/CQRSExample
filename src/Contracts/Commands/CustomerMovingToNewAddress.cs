using System;

namespace Contracts.Commands
{
    public sealed class CustomerMovingToNewAddress : ICommand
    {
        public Guid CustomerId { get; set; }

        public string HouseNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
    }
}

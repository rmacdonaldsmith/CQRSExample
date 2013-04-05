using System;

namespace Contracts.Commands
{
    public sealed class RegisterNewCustomer : ICommand
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string HouseNumber { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }

        public string MaritalStatus { get; set; }

        public string Gender { get; set; }
    }
}

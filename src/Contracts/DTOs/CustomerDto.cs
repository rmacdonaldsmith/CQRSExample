using System;
using System.Collections.Generic;

namespace Contracts.DTOs
{
    public sealed class CustomerDto : IMessage
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int AgeYears { get; set; }

        public int AgeMonths { get; set; }

        public string Gender { get; set; }

        public string MaritalStatus { get; set; }

        public List<AddressDto> Addresses { get; set; }
    }
}

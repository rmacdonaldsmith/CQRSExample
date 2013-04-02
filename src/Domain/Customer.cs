using System;
using Contracts.Events;

namespace Domain
{
    public class Customer : AggregateRoot
    {
        private Guid _id;
        private MaritalStatus _maritalStatus;

        public override Guid Id
        {
            get { return _id; }
        }

        public Customer(Name name, DateTime dateOfBirth, StreetAddress primaryAddress, MaritalStatus maritalStatus)
        {
            ApplyChange(new CustomerRegistered
                {
                    City = primaryAddress.City,
                    DateOfBirth = dateOfBirth,
                    FirstName = name.FirstName,
                    HouseNumber = primaryAddress.HouseNumber,
                    Id = new Guid(),
                    LastName = name.LastName,
                    MaritalStatus = maritalStatus.Status,
                    MiddleName = name.MiddleName,
                    State = primaryAddress.State,
                    Street = primaryAddress.Street,
                    Zip = primaryAddress.Zip.Zip,
                });
        }

        public Customer()
        {
            //empty for the repository to be able to create instances
        }

        private void When(CustomerRegistered customerRegisteredEvent)
        {
            _id = customerRegisteredEvent.Id;
            _maritalStatus = MaritalStatus.Parse(customerRegisteredEvent.MaritalStatus);
        }
    }
}

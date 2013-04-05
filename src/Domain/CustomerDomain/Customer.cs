using System;
using Contracts.Commands;
using Contracts.Events;
using Domain;

namespace CQRSSample.Domain.CustomerDomain
{
    public class Customer : AggregateRoot
    {
        private Guid _id;
        private MaritalStatus _maritalStatus;
        private GenderEnum _gender;

        public override Guid Id
        {
            get { return _id; }
        }

        public Customer()
        {
            //empty for the repository / applicatio services / command handlers to be able to create instances
        }

        public void RegisterNewCustomer(Guid customerId, PersonName personName, DateTime dateOfBirth, StreetAddress primaryAddress, MaritalStatus maritalStatus, GenderEnum gender)
        {
            //do any business logic in here and fail if there is a problem
            ApplyChange(new CustomerRegistered
                {
                    City = primaryAddress.City,
                    DateOfBirth = dateOfBirth,
                    FirstName = personName.FirstName,
                    Gender = gender.Gender,
                    HouseNumber = primaryAddress.HouseNumber,
                    Id = customerId,
                    LastName = personName.LastName,
                    MaritalStatus = maritalStatus.Status,
                    MiddleName = personName.MiddleName,
                    State = primaryAddress.State,
                    Street = primaryAddress.Street,
                    Zip = primaryAddress.Zip.Zip,
                });
        }

        public void ChangeMaritalStatus(MaritalStatus maritalStatus)
        {
            if (_maritalStatus != maritalStatus)
            {
                ApplyChange(new CustomerMaritalStatusChanged
                    {
                        CustomerId = Id,
                        MaritalStatus = maritalStatus.Status,
                    });
            }
        }

        public void CustomerMovingToNewAddress(CustomerMovingToNewAddress command)
        {
            ApplyChange(new CustomerMovedToNewAddress
                {
                    
                });
        }

        internal void When(CustomerRegistered customerRegisteredEvent)
        {
            _id = customerRegisteredEvent.Id;
            _maritalStatus = MaritalStatus.Parse(customerRegisteredEvent.MaritalStatus);
        }

        internal void When(CustomerMaritalStatusChanged evnt)
        {
            _maritalStatus = MaritalStatus.Parse(evnt.MaritalStatus);
        }
    }
}

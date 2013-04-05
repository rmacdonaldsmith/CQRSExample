using System;
using CQRSSample.Domain.CommandHandlers;
using CQRSSample.Domain.Persistence;
using CQRSSample.Domain.Tests.Framework;
using Contracts.Commands;
using Contracts.Events;
using Domain;
using NUnit.Framework;

namespace CQRSSample.Domain.Tests.Customer
{
    public class registering_a_new_customer : SpecificationBase<CustomerDomain.Customer, RegisterNewCustomer>
    {
        private static Guid _customerId = Guid.NewGuid();

        [Test]
        public void when_registering_a_new_user()
        {
            Given();
            When(new RegisterNewCustomer
                {
                    CustomerId = _customerId,
                    FirstName = "Robert",
                    MiddleName = string.Empty,
                    LastName = "Macdonald Smith",
                    HouseNumber = "1200",
                    Street = "The Street",
                    City = "Olathe",
                    State = "KS",
                    Zip = "66061",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    Gender = "Male",
                    MaritalStatus = "Married",
                });
            Then(new CustomerRegistered
                {
                    Id = _customerId,
                    FirstName = "Robert",
                    MiddleName = string.Empty,
                    LastName = "Macdonald Smith",
                    HouseNumber = "1200",
                    Street = "The Street",
                    City = "Olathe",
                    State = "KS",
                    Zip = "66061",
                    DateOfBirth = new DateTime(2000, 1, 1),
                    Gender = "Male",
                    MaritalStatus = "Married",
                });
        }

        [Test]
        public void when_registering_a_new_user_with_invalid_marital_status()
        {
            Given();
            When(new RegisterNewCustomer
            {
                CustomerId = _customerId,
                FirstName = "Robert",
                MiddleName = string.Empty,
                LastName = "Macdonald Smith",
                HouseNumber = "1200",
                Street = "The Street",
                City = "Olathe",
                State = "KS",
                Zip = "66061",
                DateOfBirth = new DateTime(2000, 1, 1),
                Gender = "Male",
                MaritalStatus = "Maried",
            });
            Then(new ExceptionThrownEvent(new InvalidOperationException("MaritalStatus 'Maried' is not valid.")));
        }

        protected override void ExecuteCommand(IRepository<CustomerDomain.Customer> repository, RegisterNewCustomer command)
        {
            //invoke the command handler for this command
            new RegisterNewCustomerCommandHandler(repository).Handle(command);
        }
    }
}

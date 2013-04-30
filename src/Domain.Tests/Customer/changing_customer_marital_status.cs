using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CQRSSample.Domain.Persistence;
using CQRSSample.Domain.Tests.Framework;
using Contracts.Commands;
using Contracts.Events;
using NUnit.Framework;

namespace CQRSSample.Domain.Tests.Customer
{
    [TestFixture]
    public class changing_customer_marital_status : SpecificationBase<CustomerDomain.Customer, ChangeCustomersMaritalStatus>
    {
        private readonly Guid _customerId = Guid.NewGuid();

        [Test]
        public void when_a_customer_gets_married()
        {
            Given(new CustomerRegistered
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
                MaritalStatus = "NotMarried",
            });

            When(new ChangeCustomersMaritalStatus
                {
                    CustomerId = _customerId,
                    MaritalStatus = "Married",
                });

            Then(new CustomerMaritalStatusChanged
                {
                    CustomerId = _customerId,
                    MaritalStatus = "Married",
                });
        }

        protected override void ExecuteCommand(IRepository<CustomerDomain.Customer> repository, ChangeCustomersMaritalStatus command)
        {
            new CommandHandlers.ChangeMaritalStatusCommandHandler(repository).Handle(command);
        }
    }
}

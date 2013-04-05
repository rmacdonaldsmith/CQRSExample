using System;
using CQRSSample.Domain.CustomerDomain;
using CQRSSample.Domain.Persistence;
using Contracts.Commands;

namespace CQRSSample.Domain.CommandHandlers
{
    public class RegisterNewCustomerCommandHandler : Handles<RegisterNewCustomer>
    {
        private readonly IRepository<CustomerDomain.Customer> _repository;

        public RegisterNewCustomerCommandHandler(IRepository<CustomerDomain.Customer> repository)
        {
            _repository = repository;
        }

        public void Handle(RegisterNewCustomer command)
        {
            var maritalStatus = MaritalStatus.Parse(command.MaritalStatus);
            if(maritalStatus == null)
                throw new InvalidOperationException(string.Format("MaritalStatus '{0}' is not valid.", command.MaritalStatus));

            var gender = GenderEnum.Parse(command.Gender);
            if(gender == null)
                throw new InvalidOperationException(string.Format("Gender '{0}' is not valid.", command.Gender));

            var customer = new Customer();
            customer.RegisterNewCustomer(command.CustomerId, new PersonName(command.FirstName, command.MiddleName, command.LastName), 
                command.DateOfBirth, new StreetAddress(command.HouseNumber, command.Street, command.City, command.State, command.Zip), 
                maritalStatus, gender);

            _repository.Save(customer, 1);
        }
    }
}

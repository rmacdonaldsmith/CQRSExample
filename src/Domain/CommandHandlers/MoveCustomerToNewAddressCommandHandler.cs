using System;
using CQRSSample.Domain.CustomerDomain;
using CQRSSample.Domain.Persistence;
using Contracts.Commands;

namespace CQRSSample.Domain.CommandHandlers
{
    public class MoveCustomerToNewAddressCommandHandler : IHandleCommandsOfType<MoveCustomerToNewAddress>
    {
        private readonly IRepository<Customer> _repository;

        public MoveCustomerToNewAddressCommandHandler(IRepository<Customer> repository)
        {
            if (repository == null) throw new ArgumentNullException("repository");

            _repository = repository;
        }

        public void Handle(MoveCustomerToNewAddress command)
        {
            var customer = _repository.GetById(command.CustomerId);
            customer.CustomerMovingToNewAddress(command);
            _repository.Save(customer, 2); //TODO: have the expected version number passed around with the view and command
        }
    }
}

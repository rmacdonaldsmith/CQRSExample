using System;
using CQRSSample.Domain.CustomerDomain;
using CQRSSample.Domain.Persistence;
using Contracts.Commands;

namespace CQRSSample.Domain.CommandHandlers
{
    public class ChangeMaritalStatusCommandHandler : 
        IHandleCommandsOfType<ChangeCustomersMaritalStatus>
    {
        private IRepository<Customer> _repository;

        public ChangeMaritalStatusCommandHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public void Handle(ChangeCustomersMaritalStatus command)
        {
            var newStatus = MaritalStatus.Parse(command.MaritalStatus);
            if (newStatus == null)
                throw new InvalidOperationException(string.Format("MaritalStatus '{0}' is not valid",
                                                                  command.MaritalStatus));

            //load the customer domain object
            var customer = _repository.GetById(command.CustomerId);
            customer.ChangeMaritalStatus(newStatus);
            _repository.Save(customer, 1);
        }
    }
}

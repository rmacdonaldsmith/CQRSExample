using System;
using System.Data;
using CQRSSample.Domain.ReadModel;
using Contracts.DTOs;
using Contracts.Events;

namespace CQRSSample.Domain.EventHandlers
{
    public sealed class ChangeMaritalStatusEventHandler : Handles<CustomerMaritalStatusChanged>
    {
        private readonly IReadModelDataBase _dataBase;

        public ChangeMaritalStatusEventHandler(IReadModelDataBase dataBase)
        {
            if (dataBase == null) throw new ArgumentNullException("dataBase");

            _dataBase = dataBase;
        }

        public void Handle(CustomerMaritalStatusChanged evnt)
        {
            var customer = _dataBase.Get(evnt.CustomerId.ToString()) as CustomerDto;

            if(customer == null)
                throw new DataException(string.Format("Can not find a customer with id '{0}'", evnt.CustomerId));

            customer.MaritalStatus = evnt.MaritalStatus;
            _dataBase.Save(evnt.CustomerId.ToString(), customer);
        }
    }
}

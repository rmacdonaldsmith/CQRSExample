using System;
using CQRSSample.Domain.ReadModel;
using Contracts.DTOs;
using Contracts.Events;

namespace CQRSSample.Domain.EventHandlers
{
    public class CustomerMovedToNewAddressEventHandler : IHandleEventsOfType<CustomerMovedToNewAddress>
    {
        private readonly IReadModelDataBase _dataBase;

        public CustomerMovedToNewAddressEventHandler(IReadModelDataBase dataBase)
        {
            if (dataBase == null) throw new ArgumentNullException("dataBase");

            _dataBase = dataBase;
        }

        public void Handle(CustomerMovedToNewAddress evnt)
        {
            var customer = (CustomerDto)_dataBase.Get(evnt.CustomerId.ToString());
            customer.Address = new AddressDto
                {
                    City = evnt.City,
                    HouseNumber = evnt.HouseNumber,
                    PrimaryAddress = true,
                    State = evnt.State,
                    Street = evnt.Street,
                    Zip = evnt.Zip,
                };
            _dataBase.Save(evnt.CustomerId.ToString(), customer);
        }
    }
}

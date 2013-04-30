using System;
using System.Collections.Generic;
using Contracts.DTOs;

namespace CQRSSample.Domain.ReadModel
{
    public interface ICustomerReadModelFacade
    {
        CustomerDto Get(Guid customerId);

        IEnumerable<CustomerDto> GetAll();
    }
}

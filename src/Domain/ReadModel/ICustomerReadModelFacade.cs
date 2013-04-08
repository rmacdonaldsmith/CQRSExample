using System;
using Contracts.DTOs;

namespace CQRSSample.Domain.ReadModel
{
    interface ICustomerReadModelFacade
    {
        CustomerDto Get(Guid customerId);


    }
}

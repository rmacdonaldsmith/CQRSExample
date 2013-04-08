using System;
using Contracts.DTOs;

namespace CQRSSample.Domain.ReadModel
{
    public sealed class CustomerRemoteFacade : ICustomerReadModelFacade
    {
        private readonly IReadModelDataBase _database;

        public CustomerRemoteFacade(IReadModelDataBase database)
        {
            if (database == null) throw new ArgumentNullException("database");

            _database = database;
        }

        public CustomerDto Get(Guid customerId)
        {
            return (CustomerDto) _database.Get(customerId.ToString());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.DTOs;

namespace CQRSSample.Domain.ReadModel
{
    public sealed class CustomerReadModelService : ICustomerReadModelFacade
    {
        private readonly IReadModelDataBase _database;

        public CustomerReadModelService(IReadModelDataBase database)
        {
            if (database == null) throw new ArgumentNullException("database");

            _database = database;
        }

        public CustomerDto Get(Guid customerId)
        {
            //I am querying a "database" that supports Linq, so Linq to Sql is probably closer
            //to what you would use in here.
            //You can do anything in here, since a consumer of this ICustomerReadModelFacade does not care
            //how the data is stored or retrieved.
            return (CustomerDto) _database
                                     .Queryable()
                                     .FirstOrDefault(pair => pair.Key == customerId.ToString())
                                     .Value;
        }

        public IEnumerable<CustomerDto> GetAll()
        {
            return _database.Queryable().Select(pair => (CustomerDto)pair.Value);
        }
    }
}

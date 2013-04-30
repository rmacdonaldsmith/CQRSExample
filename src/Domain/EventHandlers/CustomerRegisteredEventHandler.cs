using System;
using CQRSSample.Domain.ReadModel;
using Contracts.DTOs;
using Contracts.Events;
using Domain;

namespace CQRSSample.Domain.EventHandlers
{
    public class CustomerRegisteredEventHandler : IHandleEventsOfType<CustomerRegistered>
    {
        private readonly IReadModelDataBase _dataBase;

        public CustomerRegisteredEventHandler(IReadModelDataBase dataBase)
        {
            if (dataBase == null) throw new ArgumentNullException("dataBase");

            _dataBase = dataBase;
        }

        public void Handle(CustomerRegistered evnt)
        {
            var timespan = SystemTime.Now - evnt.DateOfBirth;
            var age = DateTime.MinValue + timespan;

            _dataBase.Insert(evnt.Id.ToString(), new CustomerDto
                {
                    Id = evnt.Id,
                    AgeYears = age.Year - 1,
                    AgeMonths = age.Month - 1,
                    FirstName = evnt.FirstName,
                    LastName = evnt.LastName,
                    MiddleName = evnt.MiddleName,
                    HouseNumber = evnt.HouseNumber,
                    Street = evnt.Street,
                    City = evnt.City,
                    State = evnt.State,
                    Zip = evnt.Zip,
                    DateOfBirth = evnt.DateOfBirth,
                    Gender = evnt.Gender,
                    MaritalStatus = evnt.MaritalStatus,
                });
        }
    }
}

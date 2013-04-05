using System.Collections.Generic;
using CQRSSample.Domain.Persistence;
using Contracts;
using Domain;

namespace CQRSSample.Domain.Tests
{
    public interface ITestableRepository<out T> : IRepository<T> where T : AggregateRoot, new()
    {
        IEnumerable<IEvent> SavedEvents { get; }
    }
}

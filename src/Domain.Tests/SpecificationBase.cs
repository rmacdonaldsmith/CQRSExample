using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CQRSSample.Domain.Persistence;
using Contracts;
using Domain;
using NUnit.Framework;

namespace CQRSSample.Domain.Tests
{
    public abstract class SpecificationBase<TAggregate> where TAggregate : AggregateRoot, new()
    {
        private readonly List<IEvent> _given = new List<IEvent>();
        private ICommand _when;
        private List<IEvent> _then;
        private bool _thenWasCalled;
        private string _idToUse;
        /// <summary>
        /// Setup and Domain / Application services in here.
        /// </summary>
        protected abstract void SetupServices();

        public void Given(params IEvent[] givenEvents)
        {
            _given.AddRange(givenEvents);
        }

        public void When(ICommand command)
        {
            _when = command;
        }

        protected abstract void ExecuteCommand(IRepository<TAggregate> eventStore, ICommand command);

        public void Expect(params IEvent[] expectedEvents)
        {
            _thenWasCalled = true;
            _then.AddRange(expectedEvents);

            IEnumerable<IEvent> actual;
            var repository = new InMemoryEventStoreRepository<TAggregate>(_given);

            try
            {
                ExecuteCommand(repository, _when);
                actual = repository.SavedEvents;
            }
            catch (DomainException de)
            {
                //record exceptions as events and use them in the stream of actual events - make comparison of events (and/or exceptions) simple
                actual = new IEvent[]{new ExceptionThrownEvent(de)};
            }

            //compare the expected events against the actual events and assert equality
            //TODO: this method can return a collection of results that encapsulates where things are not equal - this allows us to print the results
            CompareMessages(_given, actual.ToList());

        }

        public static IEnumerable<IEvent> NoEvents
        {
            get { return new IEvent[0]; }
        }

        protected void CompareMessages(List<IEvent> expected, List<IEvent> actual)
        {
            if (expected.Count != actual.Count)
            {
                Assert.Fail("Expected to receive {0} events but received {1} events.", expected.Count, actual.Count);
            }

            for (int index = 0; index < expected.Count; index++)
            {
                var expectedEvent = expected[index];
                var actualEvent = actual[index];

                if (EventPropertiesAreEqual(actualEvent, expectedEvent, new[] { "ExtensionData" }) == false)
                {
                    Assert.Fail(String.Format("Events at index {0} are not equivalent.", index));
                }
            }
        }

        public static bool EventPropertiesAreEqual(IEvent self, IEvent to, params string[] ignore)
        {
            if (self == null || to == null)
            {
                return false;
            }

            Type selfType = self.GetType();
            if (selfType != to.GetType())
            {
                return false;
            }

            var ignoreList = new List<string>(ignore);
            foreach (var propertyInfo in selfType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!ignoreList.Contains(propertyInfo.Name))
                {
                    object selfValue = selfType.GetProperty(propertyInfo.Name).GetValue(self, null);
                    object toValue = selfType.GetProperty(propertyInfo.Name).GetValue(to, null);

                    if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

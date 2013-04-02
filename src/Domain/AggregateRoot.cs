using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Contracts;

namespace Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<IEvent> _uncommittedChanges = new List<IEvent>();

        public abstract Guid Id { get; }

        public ReadOnlyCollection<IEvent> GetUncommittedEvents 
        {
            get { return new ReadOnlyCollection<IEvent>(_uncommittedChanges); }
        }

        public void ClearUncommittedEvents()
        {
            _uncommittedChanges.Clear();
        }

        public void LoadsFromHistory(IEnumerable<IEvent> history)
        {
            foreach (var e in history)
            {
                ApplyChange(e, false);
            }
        }

        protected void ApplyChange(IEvent @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(IEvent @event, bool isNew)
        {
            //let the compiler do the work to figure out which method to call
            //NOTE:I have done this for simplicity; I would not recommend releasing this to PROD - there are lots of ways to do this, just Google around
            ((dynamic) this).Apply(@event as dynamic);

            //only record events that are new; ie not loaded from the event store
            if (isNew) 
                _uncommittedChanges.Add(@event);
        }
    }
}

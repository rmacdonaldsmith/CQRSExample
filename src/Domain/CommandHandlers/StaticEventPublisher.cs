using System;
using System.Collections.Generic;
using CQRSSample.Domain.EventHandlers;
using Contracts;
using Ninject;

namespace CQRSSample.Domain.CommandHandlers
{
    public static class StaticEventPublisher
    {
        [ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate> _actions;

        //we can use property injection to set the container via IoC
        public static IKernel Container { get; set; }

        //Registers a callback for the given domain event
        public static void Subscribe<TEvent>(Action<TEvent> callback) where TEvent : IEvent
        {
            if (_actions == null)
                _actions = new List<Delegate>();

            _actions.Add(callback);
        }

        //Clears callbacks passed to Subscribe on the current thread
        public static void ClearCallbacks()
        {
            _actions = null;
        }

        //Raises the given domain event
        public static void Publish<TEvent>(TEvent args) where TEvent : IEvent
        {
            //we can use the IoC container
            if (Container != null)
                foreach (var handler in Container.GetAll<IHandleEventsOfType<TEvent>>())
                    handler.Handle(args);

            //and / or the delegates that have been "manually" registered - this block is great for unit testing
            if (_actions != null)
                foreach (var action in _actions)
                    if (action is Action<TEvent>)
                        ((Action<TEvent>)action)(args);
        }
    }
}

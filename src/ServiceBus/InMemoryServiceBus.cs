using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;

namespace ServiceBus
{
    public class InMemoryServiceBus : ISendCommands, IPublishEvents
    {
        private readonly Dictionary<Type, List<Action<IMessage>>> _routeMap = new Dictionary<Type, List<Action<IMessage>>>();

        public void AddRoute(Type messageType, Action<IMessage> handlerDelegate)
        {
            if (_routeMap.ContainsKey(messageType) == false)
                _routeMap.Add(messageType, new List<Action<IMessage>>());

            _routeMap[messageType].Add(handlerDelegate);
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            //send the command to the routes that can handle it: this is the list of delegates for this message type
            //commands are only sent to one handler
            List<Action<IMessage>> commandHandlers;
            if (_routeMap.TryGetValue(typeof (TCommand), out commandHandlers) == false)
                throw new InvalidOperationException(string.Format("No handlers are registered to handle messages of type '{0}'", typeof(TCommand)));

            if (commandHandlers.Count > 1)
                throw new InvalidOperationException("Commands can only be sent to one handler.");

            if(commandHandlers.Count == 0)
                throw new InvalidOperationException(string.Format("No handlers are registered to handle messages of type '{0}'", typeof(TCommand)));

            commandHandlers[0].Invoke(command);
        }

        public void Publish<TEvent>(TEvent evnt) where TEvent : IEvent
        {
            //publish the event to the routes that are registered as subscribers: this is the list of delegates for this message type
            List<Action<IMessage>> eventSubscribers;
            if (_routeMap.TryGetValue(typeof(TEvent), out eventSubscribers) == false)
                throw new InvalidOperationException(string.Format("No handlers are registered to handle messages of type '{0}'", typeof(TEvent)));

            if (eventSubscribers.Count == 0)
                throw new InvalidOperationException(string.Format("No handlers are registered to handle messages of type '{0}'", typeof(TEvent)));

            //publish event to each handler
            //if there are lots of subscribers - can do this using PLinq or threadpool threads for improved throughput
            foreach (var subscriber in eventSubscribers)
            {
                subscriber(evnt);
            }
        }
    }
}

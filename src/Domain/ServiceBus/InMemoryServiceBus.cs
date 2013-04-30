using System;
using System.Collections.Generic;
using System.Linq;
using CQRSSample.Domain.CommandHandlers;
using CQRSSample.Domain.EventHandlers;
using Contracts;
using Contracts.Events;
using Ninject;
using Ninject.Parameters;

namespace CQRSSample.Domain.ServiceBus
{
    public class InMemoryServiceBus : ISendCommands, IPublishEvents
    {
        private readonly IKernel _ninjectContainer;

        public InMemoryServiceBus(IKernel ninjectContainer)
        {
            if (ninjectContainer == null) throw new ArgumentNullException("ninjectContainer");

            _ninjectContainer = ninjectContainer;
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            try
            {
                //we can not look up the eventhandler directly, since TCommand will be an ICommand instance and we can not get to the actual
                //instance type.
                var commandHandler = _ninjectContainer.Get<IHandleCommandsOfType<TCommand>>();
                commandHandler.Handle(command);
            }
            catch (Ninject.ActivationException activationException)
            {
                throw new InvalidOperationException(
                    string.Format("There are no command handlers registered to handle commands of type '{0}'",
                                  typeof (TCommand)), activationException);
            }
        }

        public void Publish<TEvent>(TEvent evnt) where TEvent : class, IEvent
        {
            try
            {
                //hack alert - this is not important for the overall idea behind CQRS so you can ignore
                //all the service bus is doing is routing commands and events to relevant handlers - a proper implemtation will do this all for you
                //TEvent will always be of type IEvent, which means that when we try to lookup the interface type "IHandleEventsOfType<TEvent>" in Ninject, it will always fail
                //What I am doing here is using reflection to contruct the generic interface type by hand: IHandleEventsOfType<actualeventtype>
                Type ifaceType = typeof(IHandleEventsOfType<>);
                Type[] evntTypeArgs = { evnt.GetType() };
                Type genericHandlerType = ifaceType.MakeGenericType(evntTypeArgs);
                var eventHandlers = _ninjectContainer.GetAll(genericHandlerType);

                if (eventHandlers == null || !Enumerable.Any(eventHandlers))
                {
                    throw new InvalidOperationException(
                        string.Format("There are no event handlers registered to handle events of type '{0}'",
                                      typeof (TEvent)));
                }

                foreach (var eventHandler in eventHandlers)
                {
                    //then we need to do some CLR late binding magic to call the Handles method
                    ((dynamic)eventHandler).Handle(evnt as dynamic);
                }
            }
            catch (Ninject.ActivationException activationException)
            {
                throw new InvalidOperationException(
                    string.Format("There are no event handlers registered to handle events of type '{0}'",
                                  typeof(TEvent)), activationException);
            }
        }
    }
}

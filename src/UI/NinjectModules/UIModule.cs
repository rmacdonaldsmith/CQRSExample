using System;
using System.ComponentModel;
using CQRSSample.Domain.CommandHandlers;
using CQRSSample.Domain.CustomerDomain;
using CQRSSample.Domain.Persistence;
using CQRSSample.Domain.ReadModel;
using CQRSSample.Domain.ServiceBus;
using Contracts.Events;
using Ninject.Modules;
using Ninject.Extensions.Conventions;

namespace UI.NinjectModules
{
    public class UIModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Customer>>()
                .To<Repository<Customer>>();

            Bind<IEventStore>()
                .To<EventStore>()
                .InSingletonScope();

            Bind<ICustomerReadModelFacade>()
                .To<CustomerReadModelService>();

            Bind<IReadModelDataBase>()
                .To<ReadModelDataBase>()
                .InSingletonScope();

            //service bus
            var serviceBus = new InMemoryServiceBus(Kernel);
            Bind<IPublishEvents>().ToMethod(context => serviceBus);
            Bind<ISendCommands>().ToMethod(context => serviceBus);

            //do some assembly scanning to find all command handlers and event handlers and register them with Ninject
            //we will use Ninject to locate the handlers in the "service bus"
            Kernel.Bind(scanner => scanner
                                       .FromAssemblyContaining<RegisterNewCustomerCommandHandler>()
                                       .SelectAllClasses()
                                       .InheritedFromAny(new[]
                                           {
                                               typeof (CQRSSample.Domain.CommandHandlers.IHandleCommandsOfType<>),
                                               typeof (CQRSSample.Domain.EventHandlers.IHandleEventsOfType<>)
                                           })
                                       .BindSingleInterface());
        }
    }
}
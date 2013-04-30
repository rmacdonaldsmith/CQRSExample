using Contracts;

namespace CQRSSample.Domain.ServiceBus
{
    public interface ISendCommands
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}

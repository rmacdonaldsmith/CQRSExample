using Contracts;

namespace CQRSSample.Domain.CommandHandlers
{
    public interface IHandleCommandsOfType<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}

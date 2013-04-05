using Contracts;

namespace CQRSSample.Domain.CommandHandlers
{
    public interface Handles<in TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}

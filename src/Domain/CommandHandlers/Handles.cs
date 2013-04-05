using Contracts;

namespace CQRSSample.Domain.CommandHandlers
{
    public interface Handles<TCommand> where TCommand : ICommand
    {
        void Handle(TCommand command);
    }
}

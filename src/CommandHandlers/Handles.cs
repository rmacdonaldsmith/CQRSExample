using System;

namespace CommandHandlers
{
    public interface Handles<TCommand> where TCommand : ICommand
    {
        Void Handle(TCommand command);
    }
}

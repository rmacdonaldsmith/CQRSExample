using System;

namespace Contracts.Commands
{
    public sealed class ChangeCustomersMaritalStatus : ICommand
    {
        public Guid CustomerId { get; set; }

        public string MaritalStatus { get; set; }
    }
}

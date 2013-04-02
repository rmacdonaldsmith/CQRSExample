namespace CommandHandlers
{
    public class RegisterNewCustomerCommandHandler : Handles<RegisterNewCustomer>
    {
        private readonly IRepository<Customer> _repository;

        public void Handle(RegisterNewCustomer command)
        {
            var customer = new Customer(new Name(command.FirstName, command.MiddleName, command.LastName), 
                command.DateOfBirth, new StreetAddress(command.HouseNumber, command.Street, command.City, command.State, command.Zip), 
                MaritalStatus.Parse(command.MaritalStatus));


        }
    }
}

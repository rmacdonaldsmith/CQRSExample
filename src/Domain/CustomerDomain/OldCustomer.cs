using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRSSample.Domain.CustomerDomain
{
    public class OldCustomer
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DoB { get; set; }

        public StreetAddress Address { get; set; }

        public string MaritalStatus { get; set; }

        public bool IsValid()
        {
            //check all properties and object invariants to make sure we 
            //are in a valid state.
            //...
            return true;
        }
    }
}

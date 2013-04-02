using System.Globalization;
using System.Text.RegularExpressions;

namespace Domain
{
    public sealed class StreetAddress
    {
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public ZipCode Zip { get; set; }

        public StreetAddress(string houseNumber, string street, string city, string state, string zip)
        {
            HouseNumber = houseNumber;
            Street = street;
            City = city;
            State = state;
            Zip = new ZipCode(zip);
        }
    }

    public sealed class ZipCode
    {
        private string _zipCode;

        public string Zip 
        {
            get { return _zipCode; }
        }

        public ZipCode(string zipCode)
        {
            if (IsAValid(zipCode) == false)
                throw new DomainException(string.Format("The zipcode provided ('{0}') is not in a valid format.", zipCode));

            _zipCode = zipCode;
        }

        public ZipCode(int zipCode)
            : this(zipCode.ToString(CultureInfo.InvariantCulture))
        {
        }

        private bool IsAValid(string zipCode)
        {
            //basic validation - you can get more complex if you want to.
            //attribution of credit: http://blog.platformular.com/2012/03/03/how-to-validate-us-zip-code-in-c/
            string pattern = @"^\\d{5}(\\-\\d{4})?$";
            var regex = new Regex(pattern);

            return regex.IsMatch(zipCode);
        }
    }
}

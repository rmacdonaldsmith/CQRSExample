namespace Domain
{
    public sealed class Name
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string MiddleInitial 
        {
            get { return string.IsNullOrEmpty(MiddleName) ? string.Empty : MiddleName.Substring(0, 1); }
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        public string FullNameWithMiddleInitial
        {
            get { return string.Format("{0} {1} {2}", FirstName, MiddleInitial, LastName); }
        }

        public string FullNameWithMiddleName
        {
            get { return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName); }
        }

        public Name(string firstName, string middleName, string lastName)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
    }
}

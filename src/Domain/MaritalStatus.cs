using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public sealed class MaritalStatus
    {
        public static MaritalStatus Married = new MaritalStatus("Married");
        public static MaritalStatus NeverMaried = new MaritalStatus("NeverMaried");
        public static MaritalStatus Divorced = new MaritalStatus("Divorced");
        public static readonly List<MaritalStatus> All = new List<MaritalStatus> {Married, NeverMaried, Divorced}; 

        private readonly string _status;

        private MaritalStatus(string status)
        {
            _status = status;
        }

        public string Status
        {
            get { return _status; }
        }

        public static MaritalStatus Parse(string status)
        {
            return All.FirstOrDefault(s => s.Status == status);
        }
    }
}

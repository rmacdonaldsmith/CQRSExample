using System.Collections.Generic;
using System.Linq;

namespace CQRSSample.Domain.CustomerDomain
{
    public sealed class GenderEnum
    {
        public static readonly GenderEnum Male = new GenderEnum("Male");
        public static readonly GenderEnum Female = new GenderEnum("Female");
        public static readonly List<GenderEnum> All = new List<GenderEnum> { Male, Female }; 

        private readonly string _gender;

        public string Gender
        {
            get { return _gender; }
        }

        private GenderEnum(string gender)
        {
            _gender = gender;
        }

        public static GenderEnum Parse(string gender)
        {
            return All.FirstOrDefault(g => g.Gender == gender);
        }
    }
}

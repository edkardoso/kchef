using edk.Kchef.Domain.Entities.Users;
using System.Collections.Generic;

namespace edk.Kchef.Domain.Common.ValueObjects
{

    public class FullNameVO : ValueObject
    {
        protected FullNameVO()
        {

        }

        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public GenderType Gender { get; }


        public FullNameVO(string firstName, string lastName, string middleName, GenderType gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            MiddleName = middleName;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return Gender;
        }

        public string Treatment() => Gender switch
        {
            GenderType.Male => "Sr. ",
            GenderType.Female => "Sra. ",
            _ => string.Empty
        };

        public override string ToString() => $"{Treatment()}{FirstName} {LastName}";


    }
}

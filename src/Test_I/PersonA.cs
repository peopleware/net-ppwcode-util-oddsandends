using System;
using System.Runtime.Serialization;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [DataContract]
    public class PersonA
        : IPerson,
          IEquatable<PersonA>
    {
        public PersonA(string firstName, string lastName, IAddress address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        [DataMember]
        public IAddress Address { get; set; }

        #region IEquatable<PersonA> Members

        public bool Equals(PersonA other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.FirstName, FirstName) && Equals(other.LastName, LastName) && Equals(other.Address, Address);
        }

        #endregion

        #region IPerson Members

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        #endregion

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof(PersonA))
            {
                return false;
            }
            return Equals((PersonA)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (FirstName != null) ? FirstName.GetHashCode() : 0;
                result = (result * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
                result = (result * 397) ^ (Address != null ? Address.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(PersonA left, PersonA right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonA left, PersonA right)
        {
            return !Equals(left, right);
        }
    }
}
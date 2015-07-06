using System;
using System.Runtime.Serialization;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [DataContract]
    public class PersonB
        : PersonA,
          IEquatable<PersonB>
    {
        public PersonB(string firstName, string lastName, IAddress address, IPerson partner)
            : base(firstName, lastName, address)
        {
            Partner = partner;
        }

        [DataMember]
        public IPerson Partner { get; set; }

        #region IEquatable<PersonB> Members

        public bool Equals(PersonB other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return base.Equals(other) && Equals(other.Partner, Partner);
        }

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
            return Equals(obj as PersonB);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (Partner != null ? Partner.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PersonB left, PersonB right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonB left, PersonB right)
        {
            return !Equals(left, right);
        }
    }
}
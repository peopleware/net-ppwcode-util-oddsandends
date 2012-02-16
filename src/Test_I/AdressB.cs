using System;
using System.Runtime.Serialization;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [DataContract]
    public class AdressB : AddressA,
                           IEquatable<AdressB>
    {
        public AdressB(string streetAndNr, string postalCode, string city, string country)
            : base(streetAndNr, postalCode, city)
        {
            Country = country;
        }

        [DataMember]
        public string Country { get; set; }

        #region IEquatable<AdressB> Members

        public bool Equals(AdressB other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return base.Equals(other) && Equals(other.Country, Country);
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
            return Equals(obj as AdressB);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (Country != null ? Country.GetHashCode() : 0);
            }
        }

        public static bool operator ==(AdressB left, AdressB right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AdressB left, AdressB right)
        {
            return !Equals(left, right);
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [DataContract]
    public class AddressA
        : IAddress,
          IEquatable<AddressA>
    {
        public AddressA(string streetAndNr, string postalCode, string city)
        {
            StreetAndNr = streetAndNr;
            PostalCode = postalCode;
            City = city;
        }

        #region IAddress Members

        [DataMember]
        public string StreetAndNr { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string City { get; set; }

        #endregion

        #region IEquatable<AddressA> Members

        public bool Equals(AddressA other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Equals(other.StreetAndNr, StreetAndNr) && Equals(other.PostalCode, PostalCode) && Equals(other.City, City);
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
            if (obj.GetType() != typeof(AddressA))
            {
                return false;
            }
            return Equals((AddressA)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (StreetAndNr != null) ? StreetAndNr.GetHashCode() : 0;
                result = (result * 397) ^ (PostalCode != null ? PostalCode.GetHashCode() : 0);
                result = (result * 397) ^ (City != null ? City.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(AddressA left, AddressA right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AddressA left, AddressA right)
        {
            return !Equals(left, right);
        }
    }
}
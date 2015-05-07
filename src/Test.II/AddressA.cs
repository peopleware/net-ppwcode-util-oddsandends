// Copyright 2014 by PeopleWare n.v..
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Runtime.Serialization;

namespace PPWCode.Util.OddsAndEnds.Test.II
{
    [DataContract]
    public class AddressA
        : IAddress,
          IEquatable<AddressA>
    {
        public static bool operator ==(AddressA left, AddressA right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AddressA left, AddressA right)
        {
            return !Equals(left, right);
        }

        public AddressA(string streetAndNr, string postalCode, string city)
        {
            StreetAndNr = streetAndNr;
            PostalCode = postalCode;
            City = city;
        }

        [DataMember]
        public string StreetAndNr { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string City { get; set; }

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
    }
}
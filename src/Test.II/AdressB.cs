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
    public class AdressB : AddressA,
                           IEquatable<AdressB>
    {
        public static bool operator ==(AdressB left, AdressB right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AdressB left, AdressB right)
        {
            return !Equals(left, right);
        }

        public AdressB(string streetAndNr, string postalCode, string city, string country)
            : base(streetAndNr, postalCode, city)
        {
            Country = country;
        }

        [DataMember]
        public string Country { get; set; }

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
    }
}
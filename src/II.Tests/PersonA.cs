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

namespace PPWCode.Util.OddsAndEnds.II.Tests
{
    [DataContract]
    public class PersonA
        : IPerson,
          IEquatable<PersonA>
    {
        public static bool operator ==(PersonA left, PersonA right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonA left, PersonA right)
        {
            return !Equals(left, right);
        }
        
        public PersonA(string firstName, string lastName, IAddress address)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }

        [DataMember]
        public IAddress Address { get; set; }

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

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

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
    }
}
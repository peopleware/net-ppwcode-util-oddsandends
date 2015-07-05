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
    public class PersonB
        : PersonA,
          IEquatable<PersonB>
    {
        public static bool operator ==(PersonB left, PersonB right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonB left, PersonB right)
        {
            return !Equals(left, right);
        }
        
        public PersonB(string firstName, string lastName, IAddress address, IPerson partner)
            : base(firstName, lastName, address)
        {
            Partner = partner;
        }

        [DataMember]
        public IPerson Partner { get; set; }

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
    }
}
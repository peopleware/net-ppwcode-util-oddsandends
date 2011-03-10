//Copyright 2004 - $Date: 2008-11-15 23:58:07 +0100 (za, 15 nov 2008) $ by PeopleWare n.v..

//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at

//http://www.apache.org/licenses/LICENSE-2.0

//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;

namespace PPWCode.Util.OddsAndEnds.I.UnitTestHelpers
{
    public class ValueTypeSequences
    {
        public static IEnumerable<decimal?> Decimals
        {
            get
            {
                return new List<decimal?>()
                {
                    null,
                    0m,
                    -1m,
                    1m,
                    Decimal.MinValue,
                    Decimal.MaxValue
                };
            }
        }

        public static IEnumerable<DateTime?> Dates
        {
            get
            {
                return new List<DateTime?>()
                {
                    null,
                    DateTime.Now,
                    DateTime.MinValue,
                    DateTime.MaxValue,
                    DateTime.Now.AddDays(-1),
                    DateTime.Now.AddDays(1),
                };
            }
        }

        public static IEnumerable<string> Strings
        {
            get
            {
                return new List<string>()
                {
                    null,
                    string.Empty,
                    "Test"
                };
            }
        }
    }
}

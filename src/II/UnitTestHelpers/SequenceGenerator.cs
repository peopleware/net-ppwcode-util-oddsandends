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

#region Using

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

#endregion

namespace PPWCode.Util.OddsAndEnds.I.UnitTestHelpers
{
    public class SequenceGenerator
    {
        public static IEnumerable<decimal> CreateRandomDecimalSequence(int nrItems, double start, double end, ref decimal sum)
        {
            Contract.Requires(nrItems >= 0);
            Contract.Requires(start < end);
            Contract.Ensures(Contract.Result<IEnumerable<decimal>>().Count() == nrItems);
            Contract.Ensures(Contract.Result<IEnumerable<decimal>>().Sum(o => o) == sum);

            Random r = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            List<decimal> lst =
                Enumerable.Range(1, nrItems)
                    .Select(o => (decimal)((r.NextDouble() * (end - start)) + start))
                    .ToList();
            sum = lst.Sum(o => o);
            return lst;
        }
    }
}

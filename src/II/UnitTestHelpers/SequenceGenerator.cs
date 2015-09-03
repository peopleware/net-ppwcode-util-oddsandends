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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace PPWCode.Util.OddsAndEnds.II.UnitTestHelpers
{
    /// <summary>
    ///     Helper class to generate sequences.
    /// </summary>
    public class SequenceGenerator
    {
        /// <summary>
        ///     Creates a random IEnumerable of decimals.
        /// </summary>
        /// <param name="nrItems">The number of decimals to create.</param>
        /// <param name="start">The minimal possible value.</param>
        /// <param name="end">The maximal possible value.</param>
        /// <param name="sum">The sum of the decimals.</param>
        /// <returns>An IEnumerable of decimals.</returns>
        /// <exception cref="OverflowException">This exception is thrown when sum of generated numbers gets too large.</exception>
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
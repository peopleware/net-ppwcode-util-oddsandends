using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.AssemblyHelpers;

namespace PPWCode.Util.OddsAndEnds.Test.II
{
    [TestFixture]
    public class AssemblyHelperTests
    {
        [Test]
        public void LoadAssemblyNotNullTest()
        {
            var assembly = AssemblyHelper.LoadAssembly("PPWCode.Util.OddsAndEnds.II.dll");
            Assert.IsNotNull(assembly);
        }

        [Test]
        public void CreateInstanceOfSequenceGeneratorIsNotNull()
        {
            var assembly = AssemblyHelper.LoadAssembly("PPWCode.Util.OddsAndEnds.II.dll");
            var adSearch = AssemblyHelper.CreateInstanceOf(assembly, "PPWCode.Util.OddsAndEnds.II.UnitTestHelpers.SequenceGenerator");// "AdSearch");
            Assert.IsNotNull(adSearch);
        }
    }
}

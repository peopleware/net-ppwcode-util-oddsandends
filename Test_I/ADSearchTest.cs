#region Using

using System.Security.Principal;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PPWCode.Util.OddsAndEnds.I.ActiveDirectory;

#endregion

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [TestClass]
    public class AdSearchTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            Assert.IsNotNull(wi);
            AdSearch adSearch = new AdSearch(AdSearch.GetDomainFromUserAccount(wi.Name));
            Assert.IsTrue(adSearch.UserExists(wi.Name));
        }
    }
}
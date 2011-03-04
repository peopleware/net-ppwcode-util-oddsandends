#region Using

using System.Security.Principal;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PPWCode.Util.OddsAndEnds.I.ActiveDirectory;

#endregion

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    /// <summary>
    /// These tests are only available when running with a domain controller.
    /// </summary>
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

        [TestMethod]
        public void TestMethod2()
        {
            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            Assert.IsNotNull(wi);
            AdSearch adSearch = new AdSearch(AdSearch.GetDomainFromUserAccount(wi.Name));
            Assert.IsNotNull(adSearch.FindName(wi.Name));
        }

        [TestMethod]
        public void TestMethod3()
        {
            WindowsIdentity wi = WindowsIdentity.GetCurrent();
            Assert.IsNotNull(wi);
            AdSearch adSearch = new AdSearch(AdSearch.GetDomainFromUserAccount(wi.Name));
            Assert.IsNotNull(adSearch.FindEmail(wi.Name));
        }
    }
}
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using PPWCode.Util.OddsAndEnds.I.Extensions;

#endregion

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    /// <summary>
    /// This is a test class for StringExtensionsTest and is intended
    /// to contain all StringExtensionsTest Unit Tests
    /// </summary>
    [TestClass]
    public class StringExtensionsTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion

        #region IsCaptialized

        [TestMethod, Description("IsCapitalized OneLowercase")]
        public void IsCapitalizedTestOneLowercaseChar()
        {
            Assert.IsFalse("a".StartWithACapital());
        }

        [TestMethod, Description("IsCapitalized OneUppercase")]
        public void IsCapitalizedTestOneUppercaseChar()
        {
            Assert.IsTrue("A".StartWithACapital());
        }

        [TestMethod, Description("IsCapitalized MultipleLowercase")]
        public void IsCapitalizedTestMultipleLowercaseCharacters()
        {
            Assert.IsFalse("andras pandy".StartWithACapital());
        }

        [TestMethod, Description("IsCapitalized MultipleUppercase")]
        public void IsCapitalizedTestMultipleUppercaseCharacters()
        {
            Assert.IsTrue("AGNES PANDY".StartWithACapital());
        }

        [TestMethod, Description("IsCapitalized Sentence")]
        public void IsCapitalizedTestSentce()
        {
            Assert.IsTrue("The dissolved pandies recorded their latest album in 2001".StartWithACapital());
        }

        #endregion
    }
}

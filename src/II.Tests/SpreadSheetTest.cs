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
using System.Data.Common;
using System.Reflection;

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.SpreadSheet;
using PPWCode.Util.OddsAndEnds.II.Streaming;

namespace PPWCode.Util.OddsAndEnds.II.Tests
{
    [TestFixture]
    public class SpreadSheetTest
    {
        public class ExcelRow
        {
            public long PaymentDossierID { get; set; }

            public long AffiliateSynergyId { get; set; }
        }

        private const string MNameSpaceName = "PPWCode.Util.OddsAndEnds.Test.II.";
        private const string ResourceName = "FixGenerateStandardProposals.xlsx";

        private readonly List<string> m_ColumnNames = new List<string>
                                                      {
                                                          "PaymentDossierId",
                                                          "AffiliateSynergyId"
                                                      };

        private readonly Assembly m_Assembly = typeof(SpreadSheetTest).Assembly;

        private static ExcelRow SpreadsheetRowResolver(DbDataReader dr)
        {
            if ((dr.IsDBNull(0) == false) && (dr.IsDBNull(1) == false))
            {
                return new ExcelRow
                {
                    PaymentDossierID = (long)dr.GetDouble(0),
                    AffiliateSynergyId = (long)dr.GetDouble(1)
                };
            }

            return null;
        }

        // TODO: improve test, code depends on installed libraries and 64-bit/32-bit dlls
        [Test]
        public void TestMethod1()
        {
            try
            {
                string fileName = ResourceStreamHelper.WriteEmbeddedResourceToTempFile(m_Assembly, MNameSpaceName, ResourceName);
                IList<ExcelRow> list = GenerateUtil.ReadSheet(fileName, "GSP", m_ColumnNames, SpreadsheetRowResolver);
                const string PaymentDossierId = "PaymentDossierId: ";
                const string AffiliateSynergyID = "AffiliateSynergyID: ";
                foreach (ExcelRow excelRow in list)
                {
                    Console.WriteLine(PaymentDossierId + excelRow.PaymentDossierID);
                    Console.WriteLine(AffiliateSynergyID + excelRow.AffiliateSynergyId);
                }

                Assert.IsNotNull(list);
            }
            catch (Exception exc)
            {
                if (exc.Message.Contains(@"The 'Microsoft.ACE.OLEDB.12.0' provider is not registered on the local machine."))
                {
                    Console.WriteLine(@"SpreadSheetTest not correctly run because there is no Microsoft.ACE.OLEDB.12.0 provider registered!");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
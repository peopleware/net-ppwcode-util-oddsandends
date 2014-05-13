using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;

using NUnit.Framework;

using PPWCode.Util.OddsAndEnds.II.SpreadSheet;
using PPWCode.Util.OddsAndEnds.II.Streaming;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [TestFixture]
    public class SpreadSheetTest
    {
        public class ExcelRow
        {
            public long PaymentDossierID { get; set; }
            public long AffiliateSynergyId { get; set; }
        }

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

        private readonly List<string> m_ColumnNames = new List<string>
        {
            "PaymentDossierId",
            "AffiliateSynergyId"
        };

        private readonly Assembly m_Assembly = typeof(SpreadSheetTest).Assembly;
        private const string MNameSpaceName = "PPWCode.Util.OddsAndEnds.Test.II.";
        private const string ResourceName = "FixGenerateStandardProposals.xlsx";

        [Test]
        // TODO: improve test, code depends on installed libraries and 64-bit/32-bit dlls
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
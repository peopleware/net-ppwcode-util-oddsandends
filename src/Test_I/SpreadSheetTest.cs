using System;
using System.Collections;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PPWCode.Util.OddsAndEnds.I.SpreadSheet;
using PPWCode.Util.OddsAndEnds.I.Streaming;

namespace PPWCode.Util.OddsAndEnds.Test_I
{
    [TestClass]
    public class SpreadSheetTest
    {
        public SpreadSheetTest()
        {
        }

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
        private const string MNameSpaceName = "PPWCode.Util.OddsAndEnds.Test_I.";
        private const string ResourceName = "FixGenerateStandardProposals.xlsx";

        
        [TestMethod]
        public void TestMethod1()
        {
            string fileName = ResourceStreamHelper.WriteEmbeddedResourceToTempFile(m_Assembly, MNameSpaceName, ResourceName);
            IList<ExcelRow> list = GenerateUtil.ReadSheet<ExcelRow>(fileName, "GSP", m_ColumnNames, SpreadsheetRowResolver);
            const string PaymentDossierId = "PaymentDossierId: ";
            const string AffiliateSynergyID = "AffiliateSynergyID: ";
            foreach (ExcelRow excelRow in list)
            {
                Console.WriteLine(PaymentDossierId + excelRow.PaymentDossierID);
                Console.WriteLine(AffiliateSynergyID + excelRow.AffiliateSynergyId);
            }
            Assert.IsNotNull(list);
        }
    }
}

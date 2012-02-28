using System;
using System.Collections;
using System.Data.Common;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using PPWCode.Util.OddsAndEnds.I.SpreadSheet;

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

        //const string FileName = @"C:\Development\Sempera\PPWCode.Util.OddsAndEnds\src\Test_I\FixGenerateStandardProposals.xlsx";
        private readonly string m_FileName = string.Empty;
        
        [TestMethod]
        public void TestMethod1()
        {
            IList<ExcelRow> list = GenerateUtil.ReadSheet<ExcelRow>(m_FileName, "GSP", m_ColumnNames, SpreadsheetRowResolver);
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

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;

namespace PPWCode.Util.OddsAndEnds.I.SpreadSheet
{
    public class ExcelUtil
    {
        private string SpreadSheetFileName { get; set; }
        public ExcelUtil(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new Exception(@"Spreadsheet not found");
            }
            SpreadSheetFileName = fileName;
        }

        private OleDbConnection GetConnection()
        {
            string connectionString = string.Empty;
            string extension = Path.GetExtension(SpreadSheetFileName);
            switch (extension)
            {
                case ".xls":
                    connectionString = string.Format(
                        @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                        @"Data Source={0};" +
                        @"Extended Properties=""Excel 8.0;" +
                        @"providerName=System.Data.OleDb;" +
                        @"HDR=YES;""",
                        SpreadSheetFileName);
                    break;
                case ".xlsx":
                    connectionString = string.Format(
                        @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                        @"Data Source={0};" +
                        @"Extended Properties=""Excel 8.0;" +
                        @"HDR=YES;""",
                        SpreadSheetFileName);
                    break;
            }
            return connectionString != string.Empty ? new OleDbConnection(connectionString) : null;
        }
        public List<T> ReadSheet<T>(string selectStatement, Func<DbDataReader, T> rowResolver)
                  where T : class
        {
            List<T> result = new List<T>();
            OleDbConnection con = GetConnection();
            using (con)
            {
                con.Open();
                var command = new OleDbCommand(selectStatement, con);

                using (DbDataReader dr = command.ExecuteReader())
                {
                    while (dr != null && dr.Read())
                    {
                        T row = rowResolver(dr);
                        if (row != null)
                        {
                            result.Add(row);
                        }
                    }
                }
            }
            return result;
        }
    }
}

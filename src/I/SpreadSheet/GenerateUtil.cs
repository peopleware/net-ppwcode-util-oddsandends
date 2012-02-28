using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

using Spring.Expressions.Parser.antlr;

namespace PPWCode.Util.OddsAndEnds.I.SpreadSheet
{
    public class GenerateUtil
    {
        public static IList<T> ReadSheet<T>(string xlsFile, string sheet, List<string> columns, Func<DbDataReader, T>spreadSheetRowResolver)
             where T :class 
        {
            ExcelUtil excelUtil = new ExcelUtil(xlsFile);
            StringBuilder selectStatement = new StringBuilder();
            selectStatement.Append(@"select");           
            foreach (string item in columns)
            {
                selectStatement.Append("[" + item + "],");
            }
            selectStatement.Remove(selectStatement.Length - 1, 1);
            selectStatement.Append(string.Format(@"from [{0}$]", sheet));

            try
            {
                return excelUtil.ReadSheet<T>(selectStatement.ToString(), spreadSheetRowResolver);
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Structure error in sheet: {0} with message : {1}", sheet, e.Message));
            }
        }
     
    }
}

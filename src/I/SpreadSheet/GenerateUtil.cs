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
            String selectStatement = string.Empty;
            selectStatement += @"select";
            string selectColumns = string.Empty;
            foreach (string item in columns)
            {
                selectColumns +=  "[" + item + "],";
            }
            string selectColumnsCorrect = selectColumns.Remove(selectColumns.Length - 1, 1);
            selectStatement += selectColumnsCorrect + string.Format(@"from [{0}$]", sheet);
           
                   
            try
            {
                return excelUtil.ReadSheet<T>(selectStatement, spreadSheetRowResolver);
            }
            catch (Exception e)
            {
                throw new SemanticException(string.Format("Structure error in sheet: {0} with message : {1}", sheet, e.Message));
            }
        }
     
    }
}

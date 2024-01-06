using System;
using System.Diagnostics;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes
{
    [AttributeUsage( AttributeTargets.Method)]
    [Conditional("UNITY_EDITOR")]
    public class GoogleSheetsPullMethodAttribute : GoogleSheetsBaseAttribute
    {
        public GoogleSheetsPullMethodAttribute(string spreadsheetID, int sheetIndex)
        {
            SpreadsheetID = spreadsheetID;
            SheetIndex = sheetIndex;
        }
        
    }
}
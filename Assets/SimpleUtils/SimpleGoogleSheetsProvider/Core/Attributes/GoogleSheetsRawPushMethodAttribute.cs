using System;
using System.Diagnostics;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes
{
    [AttributeUsage( AttributeTargets.Method)]
    [Conditional("UNITY_EDITOR")]
    public class GoogleSheetsRawPushMethodAttribute : GoogleSheetsBaseAttribute
    {
        public GoogleSheetsRawPushMethodAttribute(string spreadsheetID, int sheetIndex)
        {
            SpreadsheetID = spreadsheetID;
            SheetIndex = sheetIndex;
        }
    }
}
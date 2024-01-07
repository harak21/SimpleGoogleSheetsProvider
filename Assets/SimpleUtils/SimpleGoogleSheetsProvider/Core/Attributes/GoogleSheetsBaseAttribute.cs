using System;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes
{
    public abstract class GoogleSheetsBaseAttribute : Attribute
    {
        public string SpreadsheetID { get; protected set; }
        public int SheetIndex { get;  protected set;}
    }
}
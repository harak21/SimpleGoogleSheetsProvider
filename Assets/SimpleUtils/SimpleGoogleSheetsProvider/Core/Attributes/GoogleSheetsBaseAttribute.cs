using System;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes
{
    public abstract class GoogleSheetsBaseAttribute : Attribute
    {
        public Guid Guid { get; }
        public string SpreadsheetID { get; protected set; }
        public int SheetIndex { get;  protected set;}

        protected GoogleSheetsBaseAttribute()
        {
            Guid = Guid.NewGuid();
        }
    }
}
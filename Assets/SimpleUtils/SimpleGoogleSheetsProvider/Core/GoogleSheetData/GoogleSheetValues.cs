using System;
using System.Collections.Generic;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData
{
    [Serializable]
    public class GoogleSheetValues
    {
        public List<Row> Rows { get; } = new();

        public void AddRow(List<string> rowData)
        {
            Rows.Add(new Row(rowData));
        }
    }
}
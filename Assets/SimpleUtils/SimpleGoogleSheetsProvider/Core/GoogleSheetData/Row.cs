using System;
using System.Collections.Generic;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData
{
    [Serializable]
    public class Row
    {
        public List<string> Cells { get; }

        public Row(List<string> cells)
        {
            Cells = cells;
        }
    }
}
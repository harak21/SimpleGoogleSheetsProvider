using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData
{
    [Serializable]
    public class Row
    {
        [SerializeField] private List<string> cells;
        public List<string> Cells => cells;

        public Row(List<string> cells)
        {
            this.cells = cells;
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData
{
    [Serializable]
    public class GoogleSheetValues
    {
        [SerializeField] private List<Row> rows = new();
        public List<Row> Rows => rows;

        public void AddRow(List<string> rowData)
        {
            rows.Add(new Row(rowData));
        }
    }
}
using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData;
using UnityEngine;

namespace Scenes
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GoogleSheetValues values;
        [SerializeField] private List<RowData> rowsData;

        [GoogleSheetsPullMethod("1NMN17TkEQLwdo3ysgZ4OEAzwjvcLplLrTTnESmYhKro", 0)]
        public void Pull(GoogleSheetValues googleSheetValues)
        {
            values = googleSheetValues;
        }

        [GoogleSheetsPushMethod("1_cAqO3Kcws3OfgxQW2THXvMiNRJieMWigKJCrWuOv8Q", 0)]
        public GoogleSheetValues Push()
        {
            return values; 
        }

        [GoogleSheetsRawPullMethod("1NMN17TkEQLwdo3ysgZ4OEAzwjvcLplLrTTnESmYhKro",0)]
        public void PullRaw(BatchGetValuesByDataFilterResponse batchGetValues)
        {
            if (batchGetValues.ValueRanges.Count < 0 || batchGetValues.ValueRanges[0].ValueRange.Values == null)
                return;

            rowsData ??= new List<RowData>();
            foreach (var row in batchGetValues.ValueRanges[0].ValueRange.Values)
            {
                IList<CellData> cellsData = new List<CellData>();
                foreach (var cell in row)
                {
                    cellsData.Add(new CellData()
                    {
                        UserEnteredValue = new ExtendedValue()
                        {
                            StringValue = cell.ToString()
                        }
                    });
                }
                rowsData.Add(new RowData()
                {
                    Values = cellsData
                });
            }
        }

        [GoogleSheetsRawPushMethod("1_cAqO3Kcws3OfgxQW2THXvMiNRJieMWigKJCrWuOv8Q", 0)]
        public List<RowData> PushRaw()
        {
            return rowsData;
        }
    }
}
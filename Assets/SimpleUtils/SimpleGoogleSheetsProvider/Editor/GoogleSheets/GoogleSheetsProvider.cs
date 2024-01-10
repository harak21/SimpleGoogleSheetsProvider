using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData;
using SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorDataRepository;
using UnityEditor;
using UnityEngine;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.GoogleSheets
{
    public static class GoogleSheetsProvider
    {
        private static readonly SimpleGoogleSheetsEditorData EditorData;
        private static GoogleSheetsServiceConnector _serviceConnector;

        static GoogleSheetsProvider()
        {
            EditorData = AssetDatabase.LoadAssetAtPath<SimpleGoogleSheetsEditorData>(
                AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets($"t: SimpleGoogleSheetsEditorData").First()));
        }

        public static async Task<BatchGetValuesByDataFilterResponse> PullRawData(string spreedSheetId, int sheetID)
        {
            Debug.Log("Retrieving data from the table started");

            if (_serviceConnector == null)
            {
                _serviceConnector = new GoogleSheetsServiceConnector(EditorData.Data);
            }
            
            var batchGetValuesByDataFilterRequest = new BatchGetValuesByDataFilterRequest()
            {
                DataFilters = new DataFilter[1]
                {
                    new()
                    {
                        GridRange = new GridRange()
                        {
                            SheetId = sheetID,
                            StartRowIndex = 0,
                            StartColumnIndex = 0
                        }
                    }
                }
            };
            try
            {
                var sheetService = await _serviceConnector.Connect();
                var getRequest =
                    sheetService.Spreadsheets.Values.BatchGetByDataFilter(batchGetValuesByDataFilterRequest, spreedSheetId);
                var spreadSheet = await getRequest.ExecuteAsync();
                Debug.Log("Retrieving data from the table is finished");
                return spreadSheet;
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }

            return null;
        }
        
        public static async Task<GoogleSheetValues> Pull(string spreedSheetId, int sheetID)
        {
            Debug.Log("Retrieving data from the table started");

            var rawData = await PullRawData(spreedSheetId, sheetID);
            return rawData == null ? null : CreateSheetValues(rawData);
        }

        public static async void RawPush(List<RowData> rowsData, string spreadSheetId, int tableId)
        {
            Debug.Log("Downloading of values to the table started");

            if (_serviceConnector == null)
            {
                _serviceConnector = new GoogleSheetsServiceConnector(EditorData.Data);
            }

            try
            {
                var sheetService = await _serviceConnector.Connect();
                var batchUpdateSpreadsheetRequest = CreateBatchUpdateRequest(rowsData, tableId);
                var pushRequest = sheetService.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, spreadSheetId);
                await pushRequest.ExecuteAsync();
                Debug.Log("Downloading of values to the table is finished");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void Push(GoogleSheetValues values, string spreadSheetId, int tableId)
        {
            var rowsData = CreateRowData(values);
            RawPush(rowsData, spreadSheetId, tableId);
        }

        private static GoogleSheetValues CreateSheetValues(BatchGetValuesByDataFilterResponse response)
        {
            GoogleSheetValues values = new GoogleSheetValues();

            if (response is null)
            {
                Debug.LogWarning("Something's gone wrong. Check your internet connection.");
                return values;
            }

            if (response.ValueRanges is null)
            {
                Debug.LogWarning("Failed to retrieve data from the table");
                return values;
            }

            if (response.ValueRanges.Count < 0 || response.ValueRanges[0].ValueRange.Values == null)
            {
                return values;
            }

            foreach (var row in response.ValueRanges[0].ValueRange.Values)
            {
                List<string> cells = new();
                foreach (var cell in row)
                {
                    cells.Add(cell.ToString());
                }
                values.AddRow(cells);
            }

            return values;
        }

        private static BatchUpdateSpreadsheetRequest CreateBatchUpdateRequest(List<RowData> rowsData, int sheetID)
        {
            UpdateCellsRequest updateCellsRequest = new UpdateCellsRequest()
            {
                Range = new GridRange()
                {
                    SheetId = sheetID,
                    StartColumnIndex = 0,
                    StartRowIndex = 0
                },
                Rows = rowsData,
                Fields = "userEnteredValue"
            };

            return new BatchUpdateSpreadsheetRequest()
            {
                Requests = new List<Request>()
                {
                    new()
                    {
                        UpdateCells = updateCellsRequest
                    }
                }
            };
        }

        private static List<RowData> CreateRowData(GoogleSheetValues values)
        {
            List<RowData> rowsData = new List<RowData>();

            foreach (var row in values.Rows)
            {
                IList<CellData> cellsData = new List<CellData>();
                foreach (var cell in row.Cells)
                {
                    cellsData.Add(new CellData()
                    {
                        UserEnteredValue = new ExtendedValue()
                        {
                            StringValue = cell
                        }
                    });
                }

                rowsData.Add(new RowData()
                {
                    Values = cellsData
                });
            }

            return rowsData;
        }
    }
    
}
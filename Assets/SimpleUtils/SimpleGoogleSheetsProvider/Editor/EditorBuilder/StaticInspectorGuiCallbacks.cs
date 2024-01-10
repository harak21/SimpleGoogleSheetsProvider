using System;
using System.Collections.Generic;
using System.Reflection;
using Google.Apis.Sheets.v4.Data;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData;
using SimpleUtils.SimpleGoogleSheetsProvider.Editor.GoogleSheets;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder
{
    public class StaticInspectorGuiCallbacks
    {
        public static async void GenerateRawPullButton(Object target, MethodInfo methodInfo)
        {
            var pullAttribute = methodInfo.GetCustomAttribute<GoogleSheetsRawPullMethodAttribute>();
            if (pullAttribute != null)
            {
                if (GUILayout.Button(methodInfo.Name))
                {
                    var data = await GoogleSheetsProvider.PullRawData(pullAttribute.SpreadsheetID, pullAttribute.SheetIndex);
                    methodInfo.Invoke(target, new object[] { data});
                }
            }
        }
        
        public static async void GeneratePullButton(Object target, MethodInfo methodInfo)
        {
            var pullAttribute = methodInfo.GetCustomAttribute<GoogleSheetsPullMethodAttribute>();
            if (pullAttribute != null)
            {
                if (GUILayout.Button(methodInfo.Name))
                {
                    var data = await GoogleSheetsProvider.Pull(pullAttribute.SpreadsheetID, pullAttribute.SheetIndex);
                    methodInfo.Invoke(target, new object[] { data});
                }
            }
        }

        public static void GenerateRawPushButton(Object target, MethodInfo methodInfo)
        {
            var pushAttribute = methodInfo.GetCustomAttribute<GoogleSheetsRawPushMethodAttribute>();
            if (pushAttribute != null)
            {
                if (GUILayout.Button(methodInfo.Name))
                {
                    var list = (List<RowData>)methodInfo.Invoke(target, Array.Empty<object>());
                    GoogleSheetsProvider.RawPush(list, pushAttribute.SpreadsheetID, pushAttribute.SheetIndex);
                }
            }
        }
        
        public static void GeneratePushButton(Object target, MethodInfo methodInfo)
        {
            var pushAttribute = methodInfo.GetCustomAttribute<GoogleSheetsPushMethodAttribute>();
            if (pushAttribute != null)
            {
                if (GUILayout.Button(methodInfo.Name))
                {
                    var list = (GoogleSheetValues)methodInfo.Invoke(target, Array.Empty<object>());
                    GoogleSheetsProvider.Push(list, pushAttribute.SpreadsheetID, pushAttribute.SheetIndex);
                }
            }
        }

    }
}
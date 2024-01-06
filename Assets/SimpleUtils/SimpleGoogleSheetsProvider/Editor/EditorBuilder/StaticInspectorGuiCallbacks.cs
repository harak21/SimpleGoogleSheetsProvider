using System;
using System.Reflection;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData;
using SimpleUtils.SimpleGoogleSheetsProvider.Editor.GoogleSheets;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder
{
    public class StaticInspectorGuiCallbacks
    {
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
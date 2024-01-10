using System.Text;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;
using SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder.ReflectionInfo;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder
{
    internal class EditorSourceBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        internal EditorSourceBuilder()
        {
            _stringBuilder.AppendLine("using System;");
            _stringBuilder.AppendLine("using UnityEngine;");
            _stringBuilder.AppendLine("using UnityEditor;");
            _stringBuilder.AppendLine("using System.Reflection;");
            _stringBuilder.AppendLine("using System.Collections.Generic;");
            _stringBuilder.AppendLine("using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;");
        }

        internal string Build() => _stringBuilder.ToString();

        internal void AddEditor(ComponentInfo componentInfo, out string editorName)
        {
            editorName = $"{componentInfo.Type.Name.Replace('+', '_')}Editor";
            _stringBuilder.AppendLine($"public class {editorName} : Editor {{");
            _stringBuilder.AppendLine("public override void OnInspectorGUI() {");
            GenerateOnInspectorGUI(_stringBuilder, componentInfo);
            _stringBuilder.AppendLine("}}");
        }

        private static void GenerateOnInspectorGUI(StringBuilder stringBuilder, ComponentInfo componentInfo)
        {
            stringBuilder.AppendLine("DrawDefaultInspector();");
            foreach (var method in componentInfo.ButtonMethods)
            {
                switch (method.BaseAttribute)
                {
                    case GoogleSheetsPushMethodAttribute:
                        GeneratePushButtonMethod(stringBuilder, method);
                        break;
                    case GoogleSheetsPullMethodAttribute:
                        GeneratePullButtonMethod(stringBuilder, method);
                        break;
                    case GoogleSheetsRawPullMethodAttribute:
                        GenerateRawPullMethod(stringBuilder, method);
                        break;
                    case GoogleSheetsRawPushMethodAttribute:
                        GenerateRawPushMethod(stringBuilder, method);
                        break;
                }
            }
        }

        private static void GenerateRawPullMethod(StringBuilder stringBuilder, ButtonMethodInfo buttonMethodInfo)
        {
            stringBuilder.AppendLine(
                @$"var rawPullMethodInfo = typeof({buttonMethodInfo.Method.DeclaringType.FullName}).GetMethod(""{buttonMethodInfo.Method.Name}"",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);");
            stringBuilder.AppendLine(
                $"SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder.StaticInspectorGuiCallbacks.GenerateRawPullButton(target, rawPullMethodInfo);");
        }

        private static void GeneratePullButtonMethod(StringBuilder stringBuilder, ButtonMethodInfo buttonMethodInfo)
        {
            stringBuilder.AppendLine(
                @$"var pullMethodInfo = typeof({buttonMethodInfo.Method.DeclaringType.FullName}).GetMethod(""{buttonMethodInfo.Method.Name}"",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);");
            stringBuilder.AppendLine(
                $"SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder.StaticInspectorGuiCallbacks.GeneratePullButton(target, pullMethodInfo);");
        }

        private static void GenerateRawPushMethod(StringBuilder stringBuilder, ButtonMethodInfo buttonMethodInfo)
        {
            stringBuilder.AppendLine(
                @$"var rawPushMethodInfo = typeof({buttonMethodInfo.Method.DeclaringType.FullName}).GetMethod(""{buttonMethodInfo.Method.Name}"",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);");
            stringBuilder.AppendLine(
                $"SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder.StaticInspectorGuiCallbacks.GenerateRawPushButton(target, rawPushMethodInfo);");
        }

        private static void GeneratePushButtonMethod(StringBuilder stringBuilder, ButtonMethodInfo buttonMethodInfo)
        {
            stringBuilder.AppendLine(
                @$"var pushMethodInfo = typeof({buttonMethodInfo.Method.DeclaringType.FullName}).GetMethod(""{buttonMethodInfo.Method.Name}"",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);");
            stringBuilder.AppendLine(
                $"SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder.StaticInspectorGuiCallbacks.GeneratePushButton(target, pushMethodInfo);");
        }
    }
}
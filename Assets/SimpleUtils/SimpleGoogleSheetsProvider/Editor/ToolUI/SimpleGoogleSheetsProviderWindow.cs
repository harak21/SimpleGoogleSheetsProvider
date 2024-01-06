using System;
using System.IO;
using System.Linq;
using SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorDataRepository;
using SimpleUtils.SimpleGoogleSheetsProvider.Editor.GoogleSheets;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.ToolUI
{
    public class SimpleGoogleSheetsProviderWindow : EditorWindow
    {
        [MenuItem("Tools/Simple utils/Simple google sheets provider")]
        public static void ShowWindow()
        {
            var window = GetWindow<SimpleGoogleSheetsProviderWindow>();
            window.titleContent = new GUIContent("GoogleSheets provider");
            window.minSize = window.maxSize = new Vector2(600, 100);
            window.ShowAuxWindow();
        }

        private void CreateGUI()
        {
            var treeView = LoadAsset<VisualTreeAsset>("SimpleGoogleSheetsProviderWindowView");
            var root = treeView.CloneTree();
            root.StretchToParentSize();
            ConstructWindowDependencies(root);
            rootVisualElement.Add(root);
            rootVisualElement.styleSheets.Add(LoadAsset<StyleSheet>("SimpleGoogleSheetsProviderStyleSheet"));
        }

        private void ConstructWindowDependencies(TemplateContainer root)
        {
            var settings = FindAsset<SimpleGoogleSheetsEditorData>();
            var clientID = root.Q<TextField>("clientID");
            clientID.SetValueWithoutNotify(settings.Data.ClientId);
            clientID.RegisterValueChangedCallback(evt =>
            {
                var oldData = settings.Data;
                settings.Data = new AuthorizationData(evt.newValue, oldData.ClientSecret);
            });

            var clientSecret = root.Q<TextField>("clientSecret");
            clientSecret.SetValueWithoutNotify(settings.Data.ClientSecret);
            clientSecret.RegisterValueChangedCallback(evt =>
            {
                var oldData = settings.Data;
                settings.Data = new AuthorizationData(oldData.ClientId, evt.newValue);
            });
        }

        private T LoadAsset<T>(string assetName) where T : Object
        {
            var asset = AssetDatabase.FindAssets(assetName).First();
            var path = AssetDatabase.GUIDToAssetPath(asset);
            return AssetDatabase.LoadAssetAtPath<T>(path);
        }

        private T FindAsset<T>() where T : Object
        {
            var assetName = typeof(T).Name;
            var assetGuid = AssetDatabase.FindAssets($"t: {assetName}").First();
            if (string.IsNullOrEmpty(assetGuid))
                throw new FileNotFoundException();

            var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }
    }
}
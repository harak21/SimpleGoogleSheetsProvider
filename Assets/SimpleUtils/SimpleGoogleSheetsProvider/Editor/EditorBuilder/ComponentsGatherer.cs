using System.Collections.Generic;
using System.Reflection;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData;
using SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder.ReflectionInfo;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder
{
    internal static class ComponentsGatherer
    {
        public static IEnumerable<ComponentInfo> Gather()
        {
            var buttonMethods = new List<ButtonMethodInfo>();

            foreach (var type in TypeCache.GetTypesDerivedFrom<Object>())
            {
                if (type.IsAbstract || type.IsGenericTypeDefinition || type.IsGenericType)
                    continue;

                foreach (var method in type.GetMethods(BindingFlags.Instance 
                                                         | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    var pullMethodAttribute = method.GetCustomAttribute<GoogleSheetsPullMethodAttribute>();
                    if (pullMethodAttribute != null)
                    {
                        AddPullMethod(method, pullMethodAttribute, buttonMethods);
                    }

                    var pushMethodAttribute = method.GetCustomAttribute<GoogleSheetsPushMethodAttribute>();
                    if (pushMethodAttribute != null)
                    {
                        AddPushMethod(method, pushMethodAttribute, buttonMethods);
                    }
                }

                if (buttonMethods.Count <= 0)
                    continue;

                var componentInfo = new ComponentInfo(type, buttonMethods.ToArray());
                yield return componentInfo;
                
                buttonMethods.Clear();
            }
        }

        private static void AddPullMethod(MethodInfo method,
            GoogleSheetsBaseAttribute baseAttribute, List<ButtonMethodInfo> buttonMethods)
        {
            var parameterInfos = method.GetParameters(); 

            if (parameterInfos.Length == 0 || parameterInfos.Length > 1 ||
                parameterInfos[0].ParameterType != typeof(GoogleSheetValues))
            {
                Debug.LogError("Method must contain an input parameter GoogleSheetValues");
                return;
            }

            buttonMethods.Add(new ButtonMethodInfo(method, baseAttribute));
        }

        private static void AddPushMethod(MethodInfo method,
            GoogleSheetsBaseAttribute baseAttribute, List<ButtonMethodInfo> buttonMethods)
        {
            var methodReturnType = method.ReturnType;

            if (methodReturnType != typeof(GoogleSheetValues))
            {
                Debug.LogError("Method must return GoogleSheetValues");
                return;
            }

            buttonMethods.Add(new ButtonMethodInfo(method, baseAttribute));
        }
    }
}
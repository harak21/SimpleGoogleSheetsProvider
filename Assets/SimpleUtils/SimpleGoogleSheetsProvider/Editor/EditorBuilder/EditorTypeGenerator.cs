using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CSharp;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;
using SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder.ReflectionInfo;
using UnityEngine;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder
{
    internal class EditorTypeGenerator
    {
        public static IEnumerable<(Type InspectedType, Type EditorType)> GenerateForComponents(
            params ComponentInfo[] components)
        {
            var editorNames = new List<string>();

            var sourceBuilder = new EditorSourceBuilder();
            foreach (ComponentInfo componentInfo in components)
            {
                sourceBuilder.AddEditor(componentInfo, out var editorName);
                editorNames.Add(editorName);
            }

            var source = sourceBuilder.Build();

            var componentAssemblies = components.Select(p => p.Type.Assembly).Distinct().ToList();
            var assembly = CompileAssemblyFromSource(source, componentAssemblies);
            for (var index = 0; index < components.Length; index++)
            {
                ComponentInfo componentType = components[index];
                string editorName = editorNames[index];

                var editor = assembly.GetType(editorName);
                if (editor == null) continue;

                yield return (componentType.Type, editor);
            }
        }
        
        private static Assembly CompileAssemblyFromSource(string sourceCode, IEnumerable<Assembly> assemblies)
        {
            var csProvider = new CSharpCodeProvider();
            var compParams = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                ReferencedAssemblies =
                {
                    typeof(UnityEngine.Object).Assembly.Location,
                    typeof(UnityEditor.Editor).Assembly.Location,
                    typeof(GUILayoutOption).Assembly.Location,
                    typeof(EditorSourceBuilder).Assembly.Location,
                    typeof(GoogleSheetsBaseAttribute).Assembly.Location
                }
            };
            foreach (Assembly assembly in assemblies)
            {
                compParams.ReferencedAssemblies.Add(assembly.Location);
            }

            var ass = Assembly.Load("netstandard, Version=2.1.0.0");
            compParams.ReferencedAssemblies.Add(ass.Location);

            CompilerResults compilerResults = csProvider.CompileAssemblyFromSource(compParams, sourceCode);

            foreach (CompilerError error in compilerResults.Errors)
            {
                Debug.LogError(error);
            }

            return compilerResults.CompiledAssembly;
        }
    }
}
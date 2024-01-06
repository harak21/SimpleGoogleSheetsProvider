using System;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder.ReflectionInfo
{
    internal class ComponentInfo
    {
        public readonly Type Type;
        public readonly ButtonMethodInfo[] ButtonMethods;

        public ComponentInfo(Type type, ButtonMethodInfo[] buttonMethods)
        {
            Type = type;
            ButtonMethods = buttonMethods;
        }
    }
}
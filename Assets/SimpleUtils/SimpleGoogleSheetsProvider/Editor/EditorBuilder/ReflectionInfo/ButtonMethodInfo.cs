using System.Reflection;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorBuilder.ReflectionInfo
{
    internal class ButtonMethodInfo
    {
        public readonly MethodInfo Method;
        public readonly GoogleSheetsBaseAttribute BaseAttribute;

        public ButtonMethodInfo(MethodInfo method, GoogleSheetsBaseAttribute baseAttribute)
        {
            Method = method;
            BaseAttribute = baseAttribute;
        }
    }
}
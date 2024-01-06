using SimpleUtils.SimpleGoogleSheetsProvider.Editor.GoogleSheets;
using UnityEngine;

namespace SimpleUtils.SimpleGoogleSheetsProvider.Editor.EditorDataRepository
{
    [CreateAssetMenu(fileName = "SimpleGoogleSheetsEditorData", menuName = "SimpleGoogleSheetsEditorData", order = 0)]
    internal class SimpleGoogleSheetsEditorData : ScriptableObject
    {
        [SerializeField] private AuthorizationData authorizationData;

        internal AuthorizationData Data
        {
            get => authorizationData;
            set => authorizationData = value;
        }
    }
}
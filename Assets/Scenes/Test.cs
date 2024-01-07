using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData;
using UnityEngine;

namespace Scenes
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GoogleSheetValues values;

        [GoogleSheetsPullMethod("asdafsazdcsadqwdeawsd", 0)]
        public void Pull(GoogleSheetValues googleSheetValues)
        {
            values = googleSheetValues;
        }

        [GoogleSheetsPushMethod("gasdgadsfsadf", 0)]
        public GoogleSheetValues Push()
        {
            return values; 
        }
    }
}
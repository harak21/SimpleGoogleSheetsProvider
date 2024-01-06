using SimpleUtils.SimpleGoogleSheetsProvider.Core.Attributes;
using SimpleUtils.SimpleGoogleSheetsProvider.Core.GoogleSheetData;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private GoogleSheetValues values;

        [GoogleSheetsPullMethod("1NMN17TkEQLwdo3ysgZ4OEAzwjvcLplLrTTnESmYhKro", 0)]
        public void Pull(GoogleSheetValues googleSheetValues)
        {
            values = googleSheetValues;
        }

        [GoogleSheetsPushMethod("1_cAqO3Kcws3OfgxQW2THXvMiNRJieMWigKJCrWuOv8Q", 0)]
        public GoogleSheetValues Push()
        {
            return values; 
        }
    }
}
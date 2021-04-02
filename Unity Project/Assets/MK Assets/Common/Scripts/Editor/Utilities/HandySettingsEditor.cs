/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using UnityEditor;
using MK.Common.Helpers;
using MK.FlexGridLayout.AssetData;

/// <summary>
/// Only create editor/shortcut in project settings which are only one
/// </summary>
namespace MK.Common.Utilities
{
    [CustomEditor(typeof(FlexGridData))]
    public class TimeDataEditor : Editor
    {
        [SettingsProvider]
        internal static SettingsProvider CreateTimeDataProvider()
        {
            var assetPath = AssetDatabase.GetAssetPath(AssetDataHelper.GetFlexGridData());

            var keywords = SettingsProvider.GetSearchKeywordsFromPath(assetPath);
            return AssetSettingsProvider.CreateProviderFromAssetPath("Project/App-Flex Grid Vertical Data", assetPath, keywords);
        }
    }
}

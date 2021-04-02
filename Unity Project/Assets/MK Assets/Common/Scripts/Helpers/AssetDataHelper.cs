/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using MK.FlexGridLayout.AssetData;

namespace MK.Common.Helpers
{
    // don't save data in this class, its just a helper class
    public static class AssetDataHelper
    {
        // TODO: change this path if you drag and drop the plugin somewhere else, instead of under Assets folder
        public const string DATABASE_PATH_FLEX_GRID_DATA =
            "Assets/MK Assets/Flex Grid Layout/Scripts/Asset Data/FlexGrid Data/FlexGrid Data Vertical.asset";

#if UNITY_EDITOR
        public static FlexGridData GetFlexGridData()
        {
            return GetDataFile<FlexGridData>(DATABASE_PATH_FLEX_GRID_DATA);
        }

        #region General

        static T GetDataFile<T>(string _assetPath) where T : ScriptableObject
        {
            return (T) AssetDatabase.LoadAssetAtPath(_assetPath, typeof(T));
        }

        #endregion General

#endif
    }
}

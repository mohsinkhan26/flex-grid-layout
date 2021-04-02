/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using UnityEditor;
using UnityEngine;
using MK.Common.Helpers;
using MK.FlexGridLayout.AssetData;

namespace MK.Common.Utilities
{
    public static class HandyDataEditor
    {
        #region Module Specific

        static FlexGridData flexGridData;

        [MenuItem("Tools/Game/Flex Grid Vertical Data", false, 20)]
        public static void FlexGridDataFile()
        {
            flexGridData = AssetDataHelper.GetFlexGridData();
            LoadData(ref flexGridData, AssetDataHelper.DATABASE_PATH_FLEX_GRID_DATA);
        }

        [MenuItem("Tools/Game/Clear Data", false, 51)]
        public static void ClearData()
        {
            PlayerPrefs.DeleteAll();
        }

        #endregion Module Specific

        #region General

        static void LoadData<T>(ref T _object, string _assetPath) where T : ScriptableObject
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(_assetPath);

            if (_object == null)
                CreateData(ref _object, _assetPath);

            SelectAssetFile(_object);
        }

        static void CreateData<T>(ref T _object, string _assetPath) where T : ScriptableObject
        {
            _object = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(_object, _assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public static void SelectAssetFile<T>(T _object) where T : ScriptableObject
        {
            // select the .asset fiile
            EditorGUIUtility.PingObject(_object);
            // focus the project window
            EditorUtility.FocusProjectWindow();
        }

        #endregion General
    }
}

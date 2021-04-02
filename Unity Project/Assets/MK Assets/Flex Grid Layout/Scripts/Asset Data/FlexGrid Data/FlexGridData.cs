/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using UnityEngine;

namespace MK.FlexGridLayout.AssetData
{
    [CreateAssetMenu(fileName = "FlexGrid Data", menuName = "Game/Flex Grid Data", order = 1)]
    public sealed class FlexGridData : ScriptableObject
    {
        [Tooltip("TRUE: Row height is fixed (Vertical), FALSE: COLUMN width is fixed (Horizontal)")]
        public bool isRowHeightFixed;
        
        [Header("Each item of grid")]
        public bool showCross;
        public bool showAdd;
        public bool makeInteractable;

        [Tooltip("Adjust this value carefully, otherwise content will overlap. And for lower width, 2 is subtracted")]
        [SerializeField]
        private float fixedTextSize;

        public float FixedTextTextSize
        {
            get
            {
                if (Screen.width < 1500)
                    return fixedTextSize - 2; // for small screen size devices
                return fixedTextSize;
            }
        }

        public GameObject flexGridPrefab;
        public GameObject flexGridItemPrefab;
        public TextAnchor containerAlignment;

        // add new item
        [Header("Last item - Add Button")] public bool addLastItemAddButton;
        public int addLastItemAddTagSize;
        public GameObject flexGridLastItemAdd;
        public Sprite lastItemAddImage;
        public bool showLastItemText;
        public string lastItemText;
    }
}

/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using System;
using System.Collections.Generic;
using UnityEngine;
using MK.Common;
using MK.FlexGridLayout.AssetData;

namespace MK.FlexGridLayout.Core
{
    /// <summary>
    /// Row in case of Vertical layout, while Column in case of Horizontal layout
    /// </summary>
    public class FlexGrid : MonoBehaviour
    {
        public int InstanceID { get; private set; }

        private FlexGridData flexGridData;
        [SerializeField] private Transform gridItemsContainer;
        private List<FlexGridItem> gridItems;
        private FlexGridLayout parent;

        public int TextLength
        {
            get
            {
                int count = 0;
                if (gridItems != null)
                {
                    for (int i = gridItems.Count - 1; i >= 0; --i)
                    {
                        count += gridItems[i].TextLength;
                    }
                }

                return count;
            }
        }

        public int FlexGridItemsCount
        {
            get
            {
                Initialize();
                return gridItems.Count;
            }
        }

        public List<FlexGridItem> FlexGridItems
        {
            get
            {
                Initialize();
                return gridItems;
            }
        }

        private void OnEnable()
        {
            InstanceID = this.GetInstanceID();
        }

        public bool HasSpace(FlexGridData _flexGridData, string _text)
        {
            flexGridData = _flexGridData;
            return (TextLength + _text.Length < flexGridData.FixedTextTextSize);
        }

        void Initialize()
        {
            if (gridItems == null) gridItems = new List<FlexGridItem>();
        }

        public FlexGridItem AddItem(FlexGridData _flexGridData, FlexGridLayout _parent, string _text,
            bool _isSelected,
            Action _onCross, Action _onAdd, Action<bool, string> _onToggleValueChanged)
        {
            Initialize();
            flexGridData = _flexGridData;
            parent = _parent;

            if (TextLength + _text.Length < flexGridData.FixedTextTextSize)
            {
                FlexGridItem flexGridItem = InstantiateNewItem();
                flexGridItem.SetText(flexGridData, this, _text, _isSelected, _onCross, _onAdd,
                    _onToggleValueChanged);
                flexGridItem.transform.SetAsLastSibling();

                return flexGridItem;
            }

            return null;
        }

        public void AddMovedItem(FlexGridItem _flexGridItem)
        {
            _flexGridItem.transform.SetParent(gridItemsContainer, false);
            gridItems.Add(_flexGridItem);
            _flexGridItem.transform.SetAsLastSibling();
        }

        FlexGridItem InstantiateNewItem()
        {
            FlexGridItem flexGridItem =
                Instantiate(flexGridData.flexGridItemPrefab).GetComponent<FlexGridItem>();
            flexGridItem.transform.SetParent(gridItemsContainer, false);
            gridItems.Add(flexGridItem);
            return flexGridItem;
        }

        public void RemoveItem(FlexGridItem _flexGridItem)
        {
            gridItems.Remove(_flexGridItem);
            parent.RemoveItem(_flexGridItem.Text, this);
            Destroy(_flexGridItem.gameObject);
        }

        public void OnRemoveAdjustLayout(FlexGridItem _flexGridItem)
        {
            gridItems.Remove(_flexGridItem);
        }

        public void ApplyAssetValues(FlexGridData _flexGridData)
        {
            flexGridData = _flexGridData;
            for (int i = gridItems.Count - 1; i >= 0; --i)
            {
                gridItems[i].ApplyAssetValues(_flexGridData);
            }
        }

        #region Sub Menu

        [ContextMenu("Debug Grid Length")]
        void DebugGridLength()
        {
            this.Log("Grid Length: " + TextLength);
        }

        #endregion Sub Menu
    }
}

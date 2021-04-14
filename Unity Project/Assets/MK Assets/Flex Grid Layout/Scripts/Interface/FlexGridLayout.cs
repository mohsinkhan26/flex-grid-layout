/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using MK.Common;
using MK.FlexGridLayout.Core;
using MK.FlexGridLayout.AssetData;

namespace MK.FlexGridLayout
{
    public class FlexGridLayout : MonoBehaviour
    {
        [SerializeField] private FlexGridData flexGridData;

        [SerializeField] private Transform gridsContainer;
        [SerializeField] private HorizontalOrVerticalLayoutGroup horizontalOrVerticalLayoutGroup;
        private List<FlexGrid> flexGrids;
        [HideInInspector] public List<string> tags;
        [HideInInspector] public List<string> selectedTags;

        private GameObject lastItem;

        public List<FlexGridItem> FlexGridItems
        {
            get
            {
                Initialize();
                return flexGrids.SelectMany(item => item.FlexGridItems).ToList();
            }
        }

        public void ChangeFlexGridData(FlexGridData _flexGridData)
        {
            flexGridData = _flexGridData;
        }

        void Initialize()
        {
            if (flexGrids == null) flexGrids = new List<FlexGrid>();
            if (tags == null) tags = new List<string>();
            if (selectedTags == null) selectedTags = new List<string>();

            horizontalOrVerticalLayoutGroup.childAlignment = flexGridData.containerAlignment;
        }

        public void AddEntry(string _text, bool _isSelected, Action _onAdd, Action _onCross = null)
        {
            Initialize();

            if (tags.Contains(_text)) return; // already exists

            if (0 < flexGrids.Count)
            {
                // already have a list
                bool notAddedYet = true;
                for (int i = 0; i < flexGrids.Count; ++i)
                {
                    if (flexGrids[i].HasSpace(flexGridData, _text))
                    {
                        notAddedYet = false;
                        flexGrids[i].AddItem(flexGridData, this, _text, _isSelected, () =>
                        {
                            tags.Remove(_text);
                            _onCross?.Invoke();
                            AddLastItem(_onAdd);
                        }, _onAdd, OnToggleValueChanged);
                        tags.Add(_text);
                        break;
                    }
                }

                if (notAddedYet)
                {
                    // add new one
                    AddNewGrid(_text, _isSelected, _onCross, _onAdd);
                }
            }
            else
            {
                // add new one
                AddNewGrid(_text, _isSelected, _onCross, _onAdd);
            }

            AddLastItem(_onAdd);
        }

        void AddNewGrid(string _text, bool _isSelected, Action _onCross, Action _onAdd)
        {
            FlexGrid flexGrid = InstantiateNewGrid();
            flexGrid.AddItem(flexGridData, this, _text, _isSelected, () =>
            {
                tags.Remove(_text);
                _onCross?.Invoke();
            }, _onAdd, OnToggleValueChanged);
            tags.Add(_text);
        }

        void OnToggleValueChanged(bool _isOn, string _text)
        {
            // selection of tags
            if (_isOn)
            {
                if (!selectedTags.Contains(_text)) selectedTags.Add(_text);
            }
            else
            {
                if (selectedTags.Contains(_text)) selectedTags.Remove(_text);
            }
        }

        FlexGrid InstantiateNewGrid()
        {
            FlexGrid flexGrid =
                Instantiate(flexGridData.flexGridPrefab).GetComponent<FlexGrid>();
            flexGrid.transform.SetParent(gridsContainer, false);
            flexGrids.Add(flexGrid);
            return flexGrid;
        }

        public void RemoveItem(string _text, FlexGrid _flexGrid)
        {
            if (selectedTags != null && selectedTags.Count > 0 && selectedTags.Any(txt => txt == _text))
            {
                selectedTags.Remove(_text);
            }

            OnRemoveAdjustLayout(_flexGrid);
        }

        void OnRemoveAdjustLayout(FlexGrid _flexGrid)
        {
            try
            {
                bool found = false;
                for (int i = 0; i < flexGrids.Count; ++i)
                {
                    if (_flexGrid.gameObject.activeInHierarchy)
                    {
                        if (_flexGrid.InstanceID == flexGrids[i].InstanceID && !found)
                        {
                            found = true; // not checked the removed item row/column
                        }
                        else if (found)
                        {
                            for (int j = 0; j < flexGrids[i].FlexGridItemsCount; ++j)
                            {
                                if (_flexGrid.HasSpace(flexGridData, flexGrids[i].FlexGridItems[j].Text))
                                {
                                    _flexGrid.AddMovedItem(flexGrids[i].FlexGridItems[j]);
                                    flexGrids[i].OnRemoveAdjustLayout(flexGrids[i].FlexGridItems[j]);
                                    if (lastItem != null) AddLastItem();
                                    OnRemoveAdjustLayout(_flexGrid); // as the list is changed now
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception _ex)
            {
                this.LogException("Message: " + _ex + "\n\nStackTrace: " + _ex.StackTrace + "\n\n");
            }
        }

        public void UnSelectAll()
        {
            if (selectedTags == null) selectedTags = new List<string>();
            else selectedTags.Clear();

            List<FlexGridItem> allItems = new List<FlexGridItem>();
            allItems.AddRange(FlexGridItems);

            for (int i = allItems.Count - 1; i >= 0; --i)
                allItems[i].toggle.isOn = false;
        }

        public void RemoveAllGridsAndItems()
        {
            if (lastItem != null)
            {
                // destroy as it will also be created with the row/column
                Destroy(lastItem);
                lastItem = null;
            }

            if (flexGrids == null) flexGrids = new List<FlexGrid>();
            // make a fresh grid
            for (int i = flexGrids.Count - 1; i >= 0; --i)
                Destroy(flexGrids[i].gameObject);
            if (flexGrids.Count > 0) flexGrids.Clear();

            if (selectedTags != null) selectedTags = new List<string>();
            else selectedTags.Clear();

            if (tags != null) tags = new List<string>();
            else tags.Clear();
        }

        #region Last Item

        public void AddLastItem(Action _onAdd = null)
        {
            if (lastItem == null)
            {
                if (flexGridData.addLastItemAddButton)
                {
                    // add the last row last item
                    FlexGridLastItem flexGridLastItem =
                        Instantiate(flexGridData.flexGridLastItemAdd).GetComponent<FlexGridLastItem>();
                    flexGridLastItem.SetText(flexGridData, _onAdd);
                    lastItem = flexGridLastItem.gameObject;
                }
            }

            if (lastItem != null)
            {
                // bool flag = flexGrids.LastOrDefault(grid => grid.gameObject.activeInHierarchy && grid.TextTextLength > 0 && grid.HasSpace(flexGridData.lastItemText))
                bool flag = (flexGrids == null || flexGrids.Count == 0)
                    ? false
                    : flexGrids.LastOrDefault().HasSpace(flexGridData, flexGridData.lastItemText);
                lastItem.SetActive(flexGridData.addLastItemAddButton); // make the GameObject active before setting its parent
                if (flag)
                {
                    // has space in last row/column
                    // lastItem.SetParent(flexGrids.LastOrDefault(grid => grid.gameObject.activeInHierarchy && grid.TextTextLength > 0 && grid.HasSpace(flexGridData.lastItemText)).transform,
                    lastItem.transform.SetParent(flexGrids.LastOrDefault().transform, false);
                }
                else
                {
                    FlexGrid flexGrid = InstantiateNewGrid();
                    lastItem.transform.SetParent(flexGrid.transform, false);
                }

                lastItem.transform.SetAsLastSibling();

                ShowLastItem(flexGridData.addLastItemAddButton);
            }
        }

        public void ShowLastItem(bool _show)
        {
            if (lastItem != null && flexGridData.addLastItemAddButton)
                lastItem.SetActive(_show);
        }

        #endregion Last Item

        #region Inspector Sub Menu

        [ContextMenu("Apply Asset Values")]
        void ApplyAssetValues()
        {
            if (lastItem != null)
                lastItem.GetComponent<FlexGridLastItem>().ApplyAssetValues(flexGridData);

            for (int i = flexGrids.Count - 1; i >= 0; --i)
            {
                flexGrids[i].ApplyAssetValues(flexGridData);
            }

            horizontalOrVerticalLayoutGroup.childAlignment = flexGridData.containerAlignment;
        }

        [ContextMenu("Add Test Entry")]
        void AddTestEntry()
        {
            AddEntry(Random.Range(1, 99999999).ToString(), false, AddTestEntry);
        }

        [ContextMenu("Add 8 Test Entry")]
        void Add8TestEntry()
        {
            for (int i = 0; i < 8; ++i)
                AddEntry(Random.Range(1, 99999999).ToString(), false, AddTestEntry);
        }

        #endregion Inspector Sub Menu
    }
}

/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using System;
using UnityEngine;
using UnityEngine.UI;
using MK.FlexGridLayout.AssetData;

namespace MK.FlexGridLayout.Core
{
    public class FlexGridItem : MonoBehaviour
    {
        public int InstanceID { get; private set; }
        private FlexGridData flexGridData;
        [SerializeField] private GameObject crossButton;
        [SerializeField] private GameObject addButton;
        [SerializeField] public Toggle toggle;
        [SerializeField] private Text text;
        [SerializeField] private LayoutElement[] layoutElement;

        private FlexGrid parent;
        private Action onCross;
        private Action onAdd;
        private Action<bool, string> onToggleValueChanged;

        public string Text
        {
            get { return text.text; }
        }

        public int TextLength
        {
            get { return text.text.Length; }
        }

        private void OnEnable()
        {
            InstanceID = this.GetInstanceID();
        }

        public void SetText(FlexGridData _flexGridData, FlexGrid _parent, string _text, bool _isSelected,
            Action _onCross, Action _onAdd, Action<bool, string> _onToggleValueChanged)
        {
            flexGridData = _flexGridData;
            parent = _parent;
            onToggleValueChanged = _onToggleValueChanged;

            text.text = _text;
            if (!flexGridData.makeInteractable && toggle.isOn) toggle.isOn = false; // make it false if the toggle is ON
            if (flexGridData.makeInteractable) toggle.isOn = _isSelected; // if it is ON previously
            toggle.interactable = flexGridData.makeInteractable;

            crossButton.gameObject.SetActive(flexGridData.showCross);
            if (flexGridData.showCross)
            {
                onCross = _onCross;
            }

            addButton.gameObject.SetActive(flexGridData.showAdd);
            if (flexGridData.showAdd)
            {
                onAdd = _onAdd;
            }
        }

        public void ApplyAssetValues(FlexGridData _flexGridData)
        {
            flexGridData = _flexGridData;
            toggle.interactable = flexGridData.makeInteractable;
            toggle.isOn = false;
        }

        public void AddClicked()
        {
            onAdd?.Invoke();
        }

        public void CrossClicked()
        {
            onCross?.Invoke();
            parent.RemoveItem(this);
        }

        public void OnToggleSelect(Toggle _toggle)
        {
            if (onToggleValueChanged != null)
                onToggleValueChanged.Invoke(_toggle.isOn, text.text);
        }
    }
}

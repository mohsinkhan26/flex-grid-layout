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
    public class FlexGridLastItem : MonoBehaviour
    {
        private FlexGridData flexGridData;
        [SerializeField] private GameObject addButton;
        [SerializeField] private Image addImage;
        [SerializeField] private Text text;

        private Action onAdd;

        public int TextLength
        {
            get { return text.text.Length; }
        }

        public void SetText(FlexGridData _flexGridData, Action _onAdd)
        {
            flexGridData = _flexGridData;
            addImage.sprite = flexGridData.lastItemAddImage;
            text.gameObject.SetActive(flexGridData.showLastItemText);
            text.text = flexGridData.lastItemText;

            onAdd = _onAdd;
        }

        public void ApplyAssetValues(FlexGridData _flexGridData)
        {
            flexGridData = _flexGridData;
            addImage.sprite = flexGridData.lastItemAddImage;
            text.gameObject.SetActive(flexGridData.showLastItemText);
            text.text = flexGridData.lastItemText;
        }

        public void AddClicked()
        {
            onAdd?.Invoke();
        }
    }
}

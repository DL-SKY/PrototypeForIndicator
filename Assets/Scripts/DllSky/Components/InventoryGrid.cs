using DllSky.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DllSky.Components
{
    public interface IInventoryGridItem
    {
        void Initialize(object _data);
    }

    public enum EnumInventoryGridShowType
    {
        Immediately,
        PerFewItems,
    }

    public class InventoryGrid : MonoBehaviour
    {
        #region Variables
        public Transform parent;
        public GameObject prefab;

        public EnumInventoryGridShowType showType;
        public int itemsPerFrame = 1;

        public bool isInit;

        private List<object> itemDatas;
        private List<IInventoryGridItem> inventoryItems = new List<IInventoryGridItem>();
        #endregion

        #region Unity methods
        private void Awake()
        {
            isInit = false;
        }
        #endregion

        #region Public methods
        public void Initialize(List<object> _itemDatas)
        {
            isInit = false;

            itemDatas = _itemDatas;
            inventoryItems.Clear();
            parent?.DestroyChildren();

            ShowItems();
        }
        #endregion

        #region Private methods
        private void ShowItems()
        {
            StopAllCoroutines();

            if (parent == null)
                parent = transform;
            if (prefab == null)
                return;

            switch (showType)
            {
                case EnumInventoryGridShowType.Immediately:
                    ShowImmediately();
                    break;

                case EnumInventoryGridShowType.PerFewItems:
                    StartCoroutine(ShowInCoroutine());
                    break;
            }
        }

        private void ShowImmediately()
        {
            foreach (var data in itemDatas)
                inventoryItems.Add(CreateItem(data));

            isInit = true;
        }

        private IInventoryGridItem CreateItem(object _data)
        {
            var newItem = Instantiate(prefab, parent);
            var newInventoryItem = newItem.GetComponent<IInventoryGridItem>();

            newInventoryItem?.Initialize(_data);

            return newInventoryItem;
        }
        #endregion

        #region Coroutines
        private IEnumerator ShowInCoroutine()
        {
            int i = 0;

            foreach (var data in itemDatas)
            {
                i++;
                inventoryItems.Add(CreateItem(data));

                if (i >= itemsPerFrame)
                {
                    i = 0;
                    yield return null;
                }
            }

            isInit = true;
        }
        #endregion
    }
}

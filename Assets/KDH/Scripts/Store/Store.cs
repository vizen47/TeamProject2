using System.Collections.Generic;
using KDH.Scripts.Item;
using UnityEngine;

namespace KDH.Scripts.Store
{
    public class Store : MonoBehaviour
    {
        [Header("데이터")]
        [SerializeField] private List<ItemSO> _data;
        public HashSet<ItemSO> ItemDataSet { get; private set; }

        [SerializeField] private ItemSOHandler[] _itemSoHandlers;
        private List<ItemSO> _currentList;

        private void Awake()
        {
            AddSet();
            ResetItems();
        }

        private void AddSet()
        {
            ItemDataSet = new HashSet<ItemSO>(_data);
        }
        
        public void ResetItems()
        {
            _currentList = new List<ItemSO>(ItemDataSet);

            List<ItemSO> item = new();

            for (int i = 0; i < _currentList.Count; i++)
            {
                int a = Random.Range(0, _currentList.Count);

                if (item.Contains(_currentList[a]))
                {
                    i--;
                    continue;
                }

                item.Add(_currentList[a]);
            }

            int j = 0;
            
            foreach (var itemHandler in _itemSoHandlers)
            {
                itemHandler.SetItemData(item[j]);
                j++;
            }
        }
    }
}
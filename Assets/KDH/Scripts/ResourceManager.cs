using System.Collections.Generic;
using UnityEngine;

namespace KDH.Scripts
{
    public class ResourceManager : MonoBehaviour
    {
        private int _credit;

        public int Credit
        {
            get => _credit;
            set
            {
                if (value > 0)
                    _credit = value;
            }
        }

        private readonly Dictionary<ItemSO, int> _itemDict = new();

        public void GetItem(ItemSO item, int count)
        {
            _itemDict.TryAdd(item, count);
        }
    }
}

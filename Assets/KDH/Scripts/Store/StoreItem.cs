using KDH.Scripts.Item;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.Store
{
    public class StoreItem : MonoBehaviour
    {
        [field: SerializeField] public Button purchaseBtn;

        private ResourceManager _resourceManager;
        
        private ItemSOHandler _itemhandler;
        
        void Awake()
        {
            _itemhandler = GetComponentInChildren<ItemSOHandler>();
            _resourceManager = FindFirstObjectByType<ResourceManager>();
            
            purchaseBtn.onClick.AddListener(() =>
            {
                _resourceManager.GetItem(_itemhandler.ItemData, 1);
                _resourceManager.UseCredit(_itemhandler.ItemData.cost);
            });
        }
    }
}

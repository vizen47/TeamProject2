using KDH.Scripts.Items;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.Stores
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
                _resourceManager.AddDeliveryItem(_itemhandler.ItemData, 1);
                _resourceManager.UseCredit(_itemhandler.ItemData.cost);
            });
        }
    }
}

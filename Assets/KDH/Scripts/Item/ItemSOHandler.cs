using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.Item
{
    public class ItemSOHandler : MonoBehaviour
    {
        public ItemSO ItemData { get; private set; }

        [SerializeField] private TextMeshProUGUI _amount;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Image _icon;
        
        public void SetItemData(ItemSO item)
        {
            ItemData = item;
            
            if (ItemData == null)
            {
                _amount.SetText("");
                _cost.SetText("");
                _icon.sprite = null;
                
                return;
            }
            
            _amount.SetText("1/1");
            _cost.SetText($"{ItemData.cost}크레딧");
            _icon.sprite = ItemData.icon;
        }
    }
}

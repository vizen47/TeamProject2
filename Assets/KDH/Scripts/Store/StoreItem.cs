using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.Store
{
    public class StoreItem : MonoBehaviour
    {
        // 여기서 아이템의 정보를 가지고 있음 or ItemHandler을 만들어서 거기서 가져와도 오케이
        [field: SerializeField] public Button purchaseBtn;

        void Awake()
        {
            purchaseBtn.onClick.AddListener(() =>
            {
                // 아이템의 가격에 따라 ResourceManager에서 계산 및 딕셔너리 추가.
            });
        }
    }
}

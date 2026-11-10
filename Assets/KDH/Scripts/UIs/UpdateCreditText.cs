using TMPro;
using UnityEngine;

namespace KDH.Scripts.UIs
{
    public class UpdateCreditText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;

        public void SetCreditText(int amount)
        {
            text.SetText($"크레딧: {amount}");
        }
    }
}

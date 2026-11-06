using TMPro;
using UnityEngine;

namespace KDH.Scripts.UIs
{
    public class HealthText : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void ChangeHealthText(int value)
        {
            _text.SetText($"체력: {value}");
        }
    }
}

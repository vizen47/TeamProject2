using TMPro;
using UnityEngine;
namespace KDH.Scripts.UIs
{
    public class Popup : MonoBehaviour
    {
        [field: SerializeField] public Transform Target { get; private set; }
        [SerializeField] private TextMeshProUGUI _text;

        void Start()
        {
            if (Target.gameObject.activeSelf)
                Target.gameObject.SetActive(false);
        }
        
        public void SetUI(string message)
        {
            if (!Target.gameObject.activeSelf)
                Target.gameObject.SetActive(true);
            
            _text.SetText(message);
        }
    }
}

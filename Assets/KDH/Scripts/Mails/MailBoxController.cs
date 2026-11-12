using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.Mails
{
    public class MailBoxController : MonoBehaviour, IUIController
    {
        [field: SerializeField] public GameObject TargetUI { get; private set; }
        [field: SerializeField] public Button CollectBtn { get; private set; }
        [field: SerializeField] public TextMeshProUGUI MailMainText { get; private set; }
        [field: SerializeField, TextArea] public string Message { get; private set; }

        [SerializeField] private ClickHotspot _mailBox;

        public event Action OnActiveChange;

        private void Start()
        {
            CollectBtn.onClick.AddListener(HandleChangeActive);

#if UNITY_EDITOR
            if (_mailBox == null)
            {
                Debug.Log("버튼 연결이 안 될지도");
            }
#endif
        }

        private void OnDestroy()
        {
            CollectBtn.onClick.RemoveAllListeners();
        }

        public void HandleChangeActive()
        {
            TargetUI.SetActive(!TargetUI.activeSelf);
            OnActiveChange?.Invoke();
        }

        public void HandleCollectBtn()
        {
            CollectBtn.onClick?.Invoke();
        }
    }
}

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

        public event Action OnActiveChange;

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

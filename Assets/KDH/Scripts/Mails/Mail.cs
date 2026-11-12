using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KDH.Scripts.Mails
{
    public class Mail : MonoBehaviour
    {
        [SerializeField] private ResourceManager _resourceManager;
        [SerializeField] private MailBoxController _mailBoxController;
        [SerializeField] private MiniItemUI _mailPrefab;
        [SerializeField] private Transform _contentTrm;

        private void Start()
        {
            _mailBoxController.CollectBtn.onClick.AddListener(DestroyItems);

            if (_mailBoxController.TargetUI.activeSelf)
            {
                _mailBoxController.HandleChangeActive();
            }
        }

        private void Update()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                _mailBoxController.HandleChangeActive();
            }
        }

        private void OnDestroy()
        {
            _mailBoxController.CollectBtn.onClick.RemoveAllListeners();
        }

        public void InitItem(IReadOnlyDictionary<ItemSO, int> items)
        {
            foreach (var item in items)
            {
                MiniItemUI miniItemUI = Instantiate(_mailPrefab, _contentTrm);
                miniItemUI.Icon.sprite = item.Key.icon;
                miniItemUI.Count.SetText("x" + item.Value);
            }

            _mailBoxController.MailMainText.SetText($"{_contentTrm.childCount}{_mailBoxController.Message}");
        }

        public void DestroyItems()
        {
            for(int i = 0; i < _contentTrm.childCount; i++)
            {
                Destroy(_contentTrm.GetChild(i).gameObject);
            }

            foreach (var item in _resourceManager.DeliveryItemDict)
            {
                _resourceManager.GetItem(item.Key, item.Value);
            }

            _mailBoxController.MailMainText.SetText("아이템이 더 이상 없는 거 같다..");
            _resourceManager.ClearDeliveryDict();
        }
    }
}

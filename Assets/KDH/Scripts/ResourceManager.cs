using System;
using System.Collections.Generic;
using KDH.Scripts.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KDH.Scripts
{
    public class ResourceManager : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        
        private int _credit = 100;

        public int Credit
        {
            get => _credit;
            set
            {
                _credit = Mathf.Max(0, value);
                OnCreditChange?.Invoke(Credit);
            }
        }

        public event Action<int> OnCreditChange;

        private readonly Dictionary<ItemSO, int> _itemDict = new();

        private void Start()
        {
            OnCreditChange += _uiManager.CreditText.SetCreditText;
            
            OnCreditChange?.Invoke(Credit);
        }

        private void Update()
        {
# if UNITY_EDITOR
            if (Keyboard.current.tKey.wasPressedThisFrame)
            {
                foreach (var i in _itemDict)
                    Debug.Log(i.Key.itemName + ": " + i.Value);
            }
# endif
        }
        
        private void OnDestroy()
        {
            OnCreditChange -= _uiManager.CreditText.SetCreditText;
        }

        public void GetItem(ItemSO item, int count)
        {
            _itemDict.TryAdd(item, count);
        }

        public void GetCredit(int amount)
        {
            Credit += amount;
        }
        
        public void UseCredit(int amount)
        {
            // 내가 살 수 있는지 검사 해야 함.
            if (Credit < amount)
            {
                _uiManager.PopupUI.SetUI("크레딧이 부족합니다.");
                return;
            }
            
            Credit -= amount;
        }
    }
}

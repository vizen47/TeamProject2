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

        private int _life = 10;

        public int Life
        {
            get => _life;
            set
            {
                _life = Mathf.Max(0, value);
                OnChangeLife?.Invoke(Life);
            }
        }

        public event Action<int> OnCreditChange;
        public event Action<int> OnChangeLife;

        private readonly Dictionary<ItemSO, int> _itemDict = new();
        private readonly Dictionary<ItemSO, int> _mailItemDict = new();
        private readonly Dictionary<ItemSO, int> _delieryItemDict = new();
        public IReadOnlyDictionary<ItemSO, int> DeliveryItemDict => _delieryItemDict;

        private void Start()
        {
            OnCreditChange += _uiManager.CreditText.SetCreditText;
            OnChangeLife += _uiManager.HealthText.ChangeHealthText;

            OnCreditChange?.Invoke(Credit);
            OnChangeLife?.Invoke(Life);
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Keyboard.current.yKey.wasPressedThisFrame)
            {
                foreach (var i in _itemDict)
                    Debug.Log(i.Key.itemName + ": " + i.Value);
            }
        }
# endif
        
        private void OnDestroy()
        {
            OnCreditChange -= _uiManager.CreditText.SetCreditText;
            OnChangeLife -= _uiManager.HealthText.ChangeHealthText;
        }

        public void AddDeliveryItem(ItemSO item, int count) // 주문
        {
            if (_delieryItemDict.TryGetValue(item, out int currentValue))
            {
                _delieryItemDict[item] = currentValue + count;
            }
            else
            {
                _delieryItemDict[item] = 1;
            }
        }

        public void AddMailItem(ItemSO item, int count) // 우편함에 아이템 넣기
        {
            _mailItemDict.TryAdd(item, count);
        }

        public void GetItem(ItemSO item, int count) // 즉시 아이템을 획득
        {
            _itemDict.TryAdd(item, count);
        }

        public void GetCredit(int amount)
        {
            Credit += amount;
        }
        public void GetLife(int amount)
        {
            Life += amount;
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

        public void SubstractLife(int amount)
        {
            Life -= amount;
        }

        public void ClearDeliveryDict()
        {
            _delieryItemDict.Clear();
        }
    }
}

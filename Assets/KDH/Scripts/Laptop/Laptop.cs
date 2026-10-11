using System;
using KDH.Scripts.CoreLib;
using KDH.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KDH.Scripts.Laptop
{
    public class Laptop : MonoSingleton<Laptop>, IPointerEnterHandler, IPointerExitHandler
    {
        [field: SerializeField] public GameObject MonitorScreen { get; private set; }
        private PopupUI _popuiUI;

        public event Action OnChangeActive;
        public bool IsActive { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            _popuiUI = GetComponentInChildren<PopupUI>();
        }

        void Start()
        {
            OnChangeActive += _popuiUI.ActiveAnimation;      
            
            MonitorScreen.SetActive(true); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
            OnChangeActive?.Invoke(); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            OnChangeActive -= _popuiUI.ActiveAnimation;    
        }

        public void HandleChangeActive()
        {
            OnChangeActive?.Invoke();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}

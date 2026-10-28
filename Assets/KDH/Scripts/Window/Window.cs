using System;
using KDH.Scripts.UI;
using UnityEngine;

namespace KDH.Scripts.Window
{
    public class Window : MonoBehaviour
    {
        private GameObject _targetUI;
        public event Action OnChangeActive;

        private SlideUpUIMotion _slideUpUIMotion;
        
        void Awake()
        {
            _slideUpUIMotion = GetComponentInChildren<SlideUpUIMotion>();
            _targetUI = gameObject;
        }

        void Start()
        {
            OnChangeActive += _slideUpUIMotion.ActiveAnimation;
            
            _targetUI.SetActive(true); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
            OnChangeActive?.Invoke(); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
        }
        
        void OnDestroy()
        {
            OnChangeActive -= _slideUpUIMotion.ActiveAnimation;    
        }

        public void HandleChangeActive()
        {
            OnChangeActive?.Invoke();
        }
    }
}

using System;
using KDH.Scripts.Systems;
using KDH.Scripts.UI;
using UnityEngine;

namespace KDH.Scripts.Internet
{
    public class Internet : MonoBehaviour, IApplicable
    {
        [field: SerializeField] public GameObject TargetApplication { get; set; }
        private PopupUI _popuiUI;

        public GameObject GameObject => gameObject;
        public event Action OnChangeActive;

        void Awake()
        {
            _popuiUI = GetComponentInChildren<PopupUI>();
        }

        void Start()
        {
            OnChangeActive += _popuiUI.ActiveAnimation;      
            
            TargetApplication.SetActive(true); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
            OnChangeActive?.Invoke(); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
        }

        void OnDestroy()
        {
            OnChangeActive -= _popuiUI.ActiveAnimation;    
        }

        public void HandleChangeActive()
        {
            OnChangeActive?.Invoke();
            ApplicationController.Instance.Init(this);
        }
    }
}

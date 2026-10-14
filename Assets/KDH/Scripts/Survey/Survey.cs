using System;
using KDH.Scripts.Systems;
using KDH.Scripts.UI;
using UnityEngine;

namespace KDH.Scripts.Survey
{
    public class Survey : MonoBehaviour, IApplicable
    {
        [field: SerializeField] public GameObject TargetApplication { get; set; }
        [field: SerializeField] public GameObject WorkApp { get; set; }
        private PopUpUI _popUiUI;
        public GameObject GameObject => gameObject;
        
        public event Action OnChangeActive;
        

        void Awake()
        {
            _popUiUI = GetComponentInChildren<PopUpUI>();
        }

        void Start()
        {
            OnChangeActive += _popUiUI.ActiveAnimation;
            
            TargetApplication.SetActive(true); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
            OnChangeActive?.Invoke(); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
        }

        void OnDestroy()
        {
            OnChangeActive -= _popUiUI.ActiveAnimation;    
        }

        public void HandleChangeActive()
        {
            OnChangeActive?.Invoke();
            ApplicationController.Instance.Init(this);
        }
        
        public void SetUnEnableWorkApp(bool isActive)
        {
            WorkApp.SetActive(isActive);
        }
    }
}

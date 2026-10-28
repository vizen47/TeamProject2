using System;
using KDH.Scripts.Systems;
using KDH.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.Store
{
    public class StoreUI : MonoBehaviour, IApplicable
    {
        [field: SerializeField] public GameObject TargetApplication { get; set; }
        [field: SerializeField] public GameObject WorkApp { get; set; }
        [SerializeField] private ApplicationController _applicationController;
        [SerializeField] private PopUpUIMotion popUIMotionUIMotion;
        [SerializeField] private Button[] _activeButton;
        public GameObject GameObject => gameObject;
        public event Action OnChangeActive;
      
        void Start()
        {
            OnChangeActive += popUIMotionUIMotion.ActiveAnimation;      
            
            TargetApplication.SetActive(true); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
            OnChangeActive?.Invoke(); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분

            foreach (var button in _activeButton)
            {
                button.onClick.AddListener(HandleChangeActive);
            }
        }

        void OnDestroy()
        {
            OnChangeActive -= popUIMotionUIMotion.ActiveAnimation; 
            
            foreach (var button in _activeButton)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        public void HandleChangeActive()
        {
            OnChangeActive?.Invoke();
            _applicationController.Init(this);
        }
        
        public void SetUnEnableWorkApp(bool isActive)
        {
            WorkApp.SetActive(isActive);
        }
    }
}

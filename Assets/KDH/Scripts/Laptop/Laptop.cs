using System;
using KDH.Scripts.CoreLib;
using KDH.Scripts.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KDH.Scripts.Laptop
{
    public class Laptop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [field: SerializeField] public GameObject MonitorScreen { get; private set; }
        [field: SerializeField] public GameObject Keyboard { get; private set; }
        [SerializeField] private SlideUpUIMotion[] slideUpUI;
        
        public event Action OnChangeActive;
        public bool IsActive { get; private set; }

        private void Awake()
        {
            slideUpUI = GetComponentsInChildren<SlideUpUIMotion>();
        }

        void Start()
        {
            foreach (var i in slideUpUI)
            {
                OnChangeActive += i.ActiveAnimation;
            }

            MonitorScreen.SetActive(true); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
            Keyboard.SetActive(true);
            OnChangeActive?.Invoke(); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
        }

        private void OnDestroy()
        {
            foreach (var i in slideUpUI)
            {
                OnChangeActive -= i.ActiveAnimation;
            }
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

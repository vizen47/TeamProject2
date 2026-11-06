using System;
using KDH.Scripts.Systems;
using KDH.Scripts.UIs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.Surveies
{
    public class SurveyUI : MonoBehaviour, IApplicable
    {
        [field: SerializeField] public GameObject TargetApplication { get; set; }
        [field: SerializeField] public GameObject WorkApp { get; set; }
        [SerializeField] private ApplicationController _applicationController;
        [SerializeField] private PopUpUIMotion popUIMotionUIMotion;
        [field: SerializeField] public Button YesBtn { get; private set; }
        [field: SerializeField] public Button IdkBtn { get; private set; }
        [field: SerializeField] public Button NoBtn { get; private set; }
        [field: SerializeField] public TextMeshProUGUI QuizText { get; private set; }
        [SerializeField] private Button _backgroundBtn;
        [SerializeField] private Button _activeButton;
        [SerializeField] private Button _normalizeButton;
        [SerializeField] private Button _closeButton;
        
        public GameObject GameObject => gameObject;
        public event Action OnChangeActive;

        void Start()
        {
            OnChangeActive += popUIMotionUIMotion.ActiveAnimation;

            TargetApplication.SetActive(true); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
            OnChangeActive?.Invoke(); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분

            //_backgroundBtn.onClick.AddListener(ActiveTrue);
            //_backgroundBtn.onClick.AddListener(HandleChangeActive);
            _activeButton.onClick.AddListener(ActiveTrue);
            _activeButton.onClick.AddListener(HandleChangeActive);
            _normalizeButton.onClick.AddListener(() => popUIMotionUIMotion.NormalizeAnimation());
            _closeButton.onClick.AddListener(ActiveFalse);
            _closeButton.onClick.AddListener(HandleChangeActive);
        }

        void OnDestroy()
        {
            OnChangeActive -= popUIMotionUIMotion.ActiveAnimation;

            _backgroundBtn.onClick.RemoveAllListeners();
            _activeButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        private void ActiveTrue()
        {
            if (TargetApplication.activeSelf == true)
            {
                return;
            }

            WorkApp.SetActive(true);
            popUIMotionUIMotion.ActiveTrueAnimation();
        }

        private void ActiveFalse()
        {
            WorkApp.SetActive(false);
            popUIMotionUIMotion.ActiveFalseAnimation();
        }

        public void HandleChangeActive()
        {
            _applicationController.Init(this);
        }
        
        public void SetUnEnableWorkApp(bool isActive)
        {
            WorkApp.SetActive(isActive);
        }
    }
}

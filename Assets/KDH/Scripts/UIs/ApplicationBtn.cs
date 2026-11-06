using KDH.Scripts.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.UIs
{
    public class ApplicationBtn : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Transform _applicationUI;
        [SerializeField] private PopUpUIMotion popUIMotionUIMotion;

        private IApplicable _target;

        private float _timer = 0f;
        private bool _isClicked = false;

        private void Start()
        {
            _button.onClick.AddListener(HandleClick);

            _target = _applicationUI.GetComponent<IApplicable>();
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            if (_isClicked) // 첫 클릭이 감지 되었을 때
            {
                _timer += Time.deltaTime;

                if (_timer >= 0.5f) // 0.5초가 지나면 클릭 초기화
                {
                    _isClicked = false;
                    _timer = 0f;
                }
            }
        }

        private void HandleClick()
        {
            if (_target.WorkApp.activeSelf == true && _target.TargetApplication.activeSelf == true) // 이미 켜져있으면 스킵
            {
                return;
            }

            if (_isClicked && _timer < 0.5f)
            {
                _target.SetUnEnableWorkApp(true);
                _target.WorkApp.SetActive(true);
                popUIMotionUIMotion.ActiveTrueAnimation();
            }
            
            _isClicked = true; // 첫 클릭 감지
        }
    }
}

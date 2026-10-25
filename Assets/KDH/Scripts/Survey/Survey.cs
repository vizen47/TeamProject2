using System;
using KDH.Scripts.Systems;
using KDH.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace KDH.Scripts.Survey
{
    public class Survey : MonoBehaviour, IApplicable
    {
        [field: SerializeField] public GameObject TargetApplication { get; set; }
        [field: SerializeField] public GameObject WorkApp { get; set; }
        [SerializeField] private Button _yesBtn, _idkBtn, _noBtn;
        [SerializeField] private TextMeshProUGUI _quizText;
        [SerializeField ] private PopUpUI _popUiUI;
        private int _currentSurveyIndex;
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
            
            _yesBtn.onClick.AddListener(InitSurveyQuiz);
            _idkBtn.onClick.AddListener(InitSurveyQuiz);
            _noBtn.onClick.AddListener(InitSurveyQuiz);
            InitSurveyQuiz();
        }

        void OnDestroy()
        {
            OnChangeActive -= _popUiUI.ActiveAnimation;
            
            _yesBtn.onClick.RemoveAllListeners();
            _idkBtn.onClick.RemoveAllListeners();
            _noBtn.onClick.RemoveAllListeners();
        }

        void Update()
        {
            if (Keyboard.current.tKey.wasPressedThisFrame)
                InitSurvey();
        }
        
        #region UI

        public void HandleChangeActive()
        {
            OnChangeActive?.Invoke();
            ApplicationController.Instance.Init(this);
        }
        
        public void SetUnEnableWorkApp(bool isActive)
        {
            WorkApp.SetActive(isActive);
        }

        #endregion

        public void InitSurvey()
        {   
            AddNormalSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].normalSurveyLevelCount.easyCount,
                SurveyQuizSO.Level.Easy);
            AddNormalSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].normalSurveyLevelCount.normalCount,
                SurveyQuizSO.Level.Normal);
            AddNormalSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].normalSurveyLevelCount.hardCount,
                SurveyQuizSO.Level.Hard);
            AddNormalSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].normalSurveyLevelCount.insaneCount,
                SurveyQuizSO.Level.Insane);
            AddWeirdSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].weirdSurveyLevelCount.easyCount,
                SurveyQuizSO.Level.Easy);
            AddWeirdSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].weirdSurveyLevelCount.normalCount,
                SurveyQuizSO.Level.Normal);
            AddWeirdSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].weirdSurveyLevelCount.hardCount,
                SurveyQuizSO.Level.Hard);
            AddWeirdSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].weirdSurveyLevelCount.insaneCount,
                SurveyQuizSO.Level.Insane);
        }

        private void AddNormalSurvey(int count, SurveyQuizSO.Level level)
        {
            if (GameManager.Instance == null) return;
            
            GameManager gameManager = GameManager.Instance;
         
            if (gameManager.SurveyData == null || !gameManager.SurveyData[UnityEngine.Random.Range(0, gameManager.SurveyData.Count)]) return;
            
            for (int i = 0; i < count; i++)
            {
                SurveyQuizSO data;
                while (true)
                {
                    data = gameManager.SurveyData[UnityEngine.Random.Range(0, gameManager.SurveyData.Count)];
                    if (data.weirdQuiz) continue;
                    int index = gameManager.SurveyData.FindIndex(x => x == data);
                    gameManager.SurveyData.RemoveAt(index);
                    if (data.level == level)
                        break;
                }

                gameManager.TodaySurveys.Add(data);
            }
        }

        private void AddWeirdSurvey(int count, SurveyQuizSO.Level level)
        {
            GameManager gameManager = GameManager.Instance;
            
            for (int i = 0; i < count; i++)
            {
                SurveyQuizSO data;
                while (true)
                {
                    data = gameManager.SurveyData[UnityEngine.Random.Range(0, gameManager.SurveyData.Count)];
                    if (!data.weirdQuiz) continue;
                    int index = gameManager.SurveyData.FindIndex(x => x == data);
                    gameManager.SurveyData.RemoveAt(index);
                    if (data.level == level)
                        break;
                }

                gameManager.TodaySurveys.Add(data);
            }
        }

        public void InitSurveyQuiz() // UI를 초기화 해주는 작업
        {
            if (_currentSurveyIndex >= GameManager.Instance.TodaySurveys.Count)
            {
                _quizText.SetText("모든 설문을 조사함");
                _yesBtn.gameObject.SetActive(false);
                _idkBtn.gameObject.SetActive(false);
                _noBtn.gameObject.SetActive(false);
                return;
            }
            
            if (!_yesBtn.gameObject.activeSelf)
                _yesBtn.gameObject.SetActive(true);
            if (!_noBtn.gameObject.activeSelf)
                _noBtn.gameObject.SetActive(true);
            
            GameManager.Instance.CurrentSurvey = GameManager.Instance.TodaySurveys[_currentSurveyIndex];
            SurveyQuizSO surveyData = GameManager.Instance.CurrentSurvey;

            _quizText.SetText(surveyData.mainQuizText);
            
            if (surveyData.weirdQuiz)
            {
                _idkBtn.gameObject.SetActive(false);
            }
            else
            {
                _idkBtn.gameObject.SetActive(true);
            }

            _currentSurveyIndex++;
        }
    }
}

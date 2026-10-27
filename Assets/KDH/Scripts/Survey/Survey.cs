using System;
using System.Collections.Generic;
using KDH.Scripts.Systems;
using KDH.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KDH.Scripts.Survey
{
    public class Survey : MonoBehaviour, IApplicable
    {
        [field: SerializeField] public GameObject TargetApplication { get; set; }
        [field: SerializeField] public GameObject WorkApp { get; set; }
        [SerializeField] private Button _yesBtn, _idkBtn, _noBtn;
        [SerializeField] private TextMeshProUGUI _quizText;
        [SerializeField ] private PopUpUIMotion popUIMotionUIMotion;
        private int _currentSurveyIndex;
        public bool HasCompletedSurvey { get; private set; }
        public GameObject GameObject => gameObject;
        
        public event Action OnChangeActive;

        private bool _hasClearedTuto;
        
        void Awake()
        {
            popUIMotionUIMotion = GetComponentInChildren<PopUpUIMotion>();
        }

        void Start()
        {
            OnChangeActive += popUIMotionUIMotion.ActiveAnimation;
            
            TargetApplication.SetActive(true); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
            OnChangeActive?.Invoke(); // <= 이부분 초기화 시작하면 자동으로 끄고 시작할려고 추가한 부분
            
            _yesBtn.onClick.AddListener(InitSurveyQuiz);
            _idkBtn.onClick.AddListener(InitSurveyQuiz);
            _noBtn.onClick.AddListener(InitSurveyQuiz);
            InitSurveyQuiz();
        }

        void OnDestroy()
        {
            OnChangeActive -= popUIMotionUIMotion.ActiveAnimation;
            
            _yesBtn.onClick.RemoveAllListeners();
            _idkBtn.onClick.RemoveAllListeners();
            _noBtn.onClick.RemoveAllListeners();
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
            if (!_hasClearedTuto)
            {
                TutorialSurvey();
                return;
            }

            AddSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].normalSurveyLevelCount.easyCount, SurveyQuizSO.Level.Easy, false);
            AddSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].normalSurveyLevelCount.normalCount, SurveyQuizSO.Level.Normal, false);
            AddSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].normalSurveyLevelCount.hardCount, SurveyQuizSO.Level.Hard, false);
            AddSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].normalSurveyLevelCount.insaneCount, SurveyQuizSO.Level.Insane, false);
            AddSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].weirdSurveyLevelCount.easyCount, SurveyQuizSO.Level.Easy, true);
            AddSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].weirdSurveyLevelCount.normalCount, SurveyQuizSO.Level.Normal, true);
            AddSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].weirdSurveyLevelCount.hardCount, SurveyQuizSO.Level.Hard, true);
            AddSurvey(GameManager.Instance.ApplySurveyCountPerDay[GameManager.Instance.CurrentDay].weirdSurveyLevelCount.insaneCount, SurveyQuizSO.Level.Insane, true);
        }

        private void TutorialSurvey()
        {
            GameManager gameManager = GameManager.Instance;
            
            if (GameManager.Instance == null || gameManager.SurveyData == null) return;

            List<SurveyQuizSO> list = gameManager.SurveyData.FindAll(x =>
                x != null && x.level == SurveyQuizSO.Level.Tutorial && !x.weirdQuiz);
            
            for (int i = 0; i < list.Count; i++)
            {
                gameManager.TodaySurveys.Add(list[i]);
                gameManager.SurveyData.Remove(list[i]);
            }

            _hasClearedTuto = true;
        }
        
        private void AddSurvey(int count, SurveyQuizSO.Level level, bool isWeird)
        {
            GameManager gameManager = GameManager.Instance;
            
            if (GameManager.Instance == null || gameManager.SurveyData == null) return;
            
            for (int i = 0; i < count; i++)
            {
                if (gameManager.SurveyData.Count <= 0)
                {
                    Debug.Log($"현재 남아있는 설문 조사 데이터가 없습니다. 현재 남아있는 설문 조사의 양{gameManager.SurveyData.Count}");
                    return;
                }
                
                List<SurveyQuizSO> list = gameManager.SurveyData.FindAll(x => x != null && x.level == level && x.weirdQuiz == isWeird);

                var surveyQuiz = list[UnityEngine.Random.Range(0, list.Count)];
                
                gameManager.TodaySurveys.Add(surveyQuiz);
                gameManager.SurveyData.Remove(surveyQuiz);
            }
        }

        public void ResetTodaySurvey()
        {
            HasCompletedSurvey = false;
            
            _currentSurveyIndex = 0;
            GameManager.Instance.TodaySurveys.Clear();
        }
        
        public void InitSurveyQuiz() // UI를 초기화 해주는 작업
        {
            if (_currentSurveyIndex >= GameManager.Instance.TodaySurveys.Count)
            {
                HasCompletedSurvey = true;
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

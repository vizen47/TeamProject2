using System.Collections.Generic;
using KDH.Scripts.Systems;
using UnityEngine;

namespace KDH.Scripts.Survey
{
    public class Survey : MonoBehaviour
    {
        [field: SerializeField] public SurveyUI SurveyUI { get; private set; }
        [SerializeField] private GameManager _gameManager; 
        
        private int _currentSurveyIndex;
        public bool HasCompletedSurvey { get; private set; }

        private bool _hasClearedTuto;
        void Start()
        {
            SurveyUI.YesBtn.onClick.AddListener(InitSurveyQuiz);
            SurveyUI.IdkBtn.onClick.AddListener(InitSurveyQuiz);
            SurveyUI.NoBtn.onClick.AddListener(InitSurveyQuiz);
            
            InitSurveyQuiz();
        }

        private void OnDestroy()
        {   
            SurveyUI.YesBtn.onClick.RemoveAllListeners();
            SurveyUI.IdkBtn.onClick.RemoveAllListeners();
            SurveyUI.NoBtn.onClick.RemoveAllListeners();
        }
        
        public void InitSurvey()
        {
            if (!_hasClearedTuto)
            {
                TutorialSurvey();
                return;
            }

            AddSurvey(_gameManager.ApplySurveyCountPerDay[_gameManager.CurrentDay].normalSurveyLevelCount.easyCount, SurveyQuizSO.Level.Easy, false);
            AddSurvey(_gameManager.ApplySurveyCountPerDay[_gameManager.CurrentDay].normalSurveyLevelCount.normalCount, SurveyQuizSO.Level.Normal, false);
            AddSurvey(_gameManager.ApplySurveyCountPerDay[_gameManager.CurrentDay].normalSurveyLevelCount.hardCount, SurveyQuizSO.Level.Hard, false);
            AddSurvey(_gameManager.ApplySurveyCountPerDay[_gameManager.CurrentDay].normalSurveyLevelCount.insaneCount, SurveyQuizSO.Level.Insane, false);
            AddSurvey(_gameManager.ApplySurveyCountPerDay[_gameManager.CurrentDay].weirdSurveyLevelCount.easyCount, SurveyQuizSO.Level.Easy, true);
            AddSurvey(_gameManager.ApplySurveyCountPerDay[_gameManager.CurrentDay].weirdSurveyLevelCount.normalCount, SurveyQuizSO.Level.Normal, true);
            AddSurvey(_gameManager.ApplySurveyCountPerDay[_gameManager.CurrentDay].weirdSurveyLevelCount.hardCount, SurveyQuizSO.Level.Hard, true);
            AddSurvey(_gameManager.ApplySurveyCountPerDay[_gameManager.CurrentDay].weirdSurveyLevelCount.insaneCount, SurveyQuizSO.Level.Insane, true);
        }

        private void TutorialSurvey()
        {
            if (_gameManager == null || _gameManager.SurveyData == null) return;

            List<SurveyQuizSO> list = _gameManager.SurveyData.FindAll(x =>
                x != null && x.level == SurveyQuizSO.Level.Tutorial && !x.weirdQuiz);
            
            for (int i = 0; i < list.Count; i++)
            {
                _gameManager.TodaySurveys.Add(list[i]);
                _gameManager.SurveyData.Remove(list[i]);
            }

            _hasClearedTuto = true;
        }
        
        private void AddSurvey(int count, SurveyQuizSO.Level level, bool isWeird)
        {
            if (_gameManager == null || _gameManager.SurveyData == null) return;
            
            for (int i = 0; i < count; i++)
            {
                if (_gameManager.SurveyData.Count <= 0)
                {
                    Debug.Log($"현재 남아있는 설문 조사 데이터가 없습니다. 현재 남아있는 설문 조사의 양{_gameManager.SurveyData.Count}");
                    return;
                }
                
                List<SurveyQuizSO> list = _gameManager.SurveyData.FindAll(x => x != null && x.level == level && x.weirdQuiz == isWeird);

                var surveyQuiz = list[Random.Range(0, list.Count)];
                
                _gameManager.TodaySurveys.Add(surveyQuiz);
                _gameManager.SurveyData.Remove(surveyQuiz);
            }
        }

        public void ResetTodaySurvey()
        {
            HasCompletedSurvey = false;
            
            _currentSurveyIndex = 0;
            _gameManager.TodaySurveys.Clear();
        }
        
        public void InitSurveyQuiz() // UI를 초기화 해주는 작업
        {
            if (_currentSurveyIndex >= _gameManager.TodaySurveys.Count)
            {
                HasCompletedSurvey = true;
                SurveyUI.QuizText.SetText("모든 설문을 조사함");
                SurveyUI.YesBtn.gameObject.SetActive(false);
                SurveyUI.IdkBtn.gameObject.SetActive(false);
                SurveyUI.NoBtn.gameObject.SetActive(false);
                return;
            }
            
            if (!SurveyUI.YesBtn.gameObject.activeSelf)
                SurveyUI.YesBtn.gameObject.SetActive(true);
            if (!SurveyUI.NoBtn.gameObject.activeSelf)
                SurveyUI.NoBtn.gameObject.SetActive(true);
            
            _gameManager.CurrentSurvey = _gameManager.TodaySurveys[_currentSurveyIndex];
            SurveyQuizSO surveyData = _gameManager.CurrentSurvey;

            SurveyUI.QuizText.SetText(surveyData.mainQuizText);
            
            if (surveyData.weirdQuiz)
            {
                SurveyUI.IdkBtn.gameObject.SetActive(false);
            }
            else
            {
                SurveyUI.IdkBtn.gameObject.SetActive(true);
            }

            _currentSurveyIndex++;
        }
    }
}

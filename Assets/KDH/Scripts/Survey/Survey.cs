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

        public void ResetTodaySurvey()
        {
            HasCompletedSurvey = false;
            
            _currentSurveyIndex = 0;

            if (_gameManager.CurrentDay >= _gameManager.DailySurveyList.Length)
            {
                Debug.Log("더 이상 설문조사가 없습니다.");
                return;
            }

            _gameManager.TodaySurveys = _gameManager.DailySurveyList[_gameManager.CurrentDay];
        }
        
        public void InitSurveyQuiz() // UI를 초기화 해주는 작업
        {
            if (_currentSurveyIndex >= _gameManager.TodaySurveys.list.Count || _gameManager.CurrentDay >= _gameManager.DailySurveyList.Length)
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
            
            _gameManager.CurrentSurvey = _gameManager.TodaySurveys.list[_currentSurveyIndex];
            SurveyQuizSO surveyData = _gameManager.CurrentSurvey;

            SurveyUI.QuizText.SetText(surveyData.mainQuizText);
            
            if (surveyData.weirdQuiz) // 이상현상인 경우에는 모르겠음 버튼을 비활성화 시킴
            {
                SurveyUI.IdkBtn.gameObject.SetActive(false);
            }
            else
            {
                SurveyUI.IdkBtn.gameObject.SetActive(true);
            }

            if (_gameManager.CurrentDay == 0) // 튜토리얼 설문조사인 경우에는 모르겠음 버튼과 아니오 버튼을 비활성화 시킴
            {
                SurveyUI.IdkBtn.gameObject.SetActive(false);
                SurveyUI.NoBtn.gameObject.SetActive(false);
            }

            _currentSurveyIndex++;
        }
    }
}

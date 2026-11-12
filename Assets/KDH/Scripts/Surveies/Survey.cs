using Assets.KDH.Scripts.Surveies;
using KDH.Scripts.Systems;
using System.Collections.Generic;
using UnityEngine;

namespace KDH.Scripts.Surveies
{
    public class Survey : MonoBehaviour
    {
        [field: SerializeField] public SurveyUI SurveyUI { get; private set; }
        [SerializeField] private GameManager _gameManager; 
        
        public bool HasCompletedSurvey { get; private set; }
        public bool IsDead { get; private set; } = false;

        private List<NormalAnswer> _normalAnswers = new();
        private List<bool> _weirdAnswers = new();
        private int _currentSurveyIndex;

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

        public void ResultSurvey(List<SurveyQuizSO> surveies)
        {
            List<SurveyQuizSO> surveys = new();
            List<bool> weirdResult = new();

            foreach (var survey in surveies)
            {
                if (survey.weirdQuiz)
                {
                    weirdResult.Add(survey.isWeird);
                    surveys.Add(survey);
                }

                for (int i = 0; i < 3; i++) // NormalAnswer의 enum 종류가 3가지라 3번 반복
                {
                    if (survey.normalAnswerReward[i].answer == _normalAnswers[i])
                    {
                        _gameManager.Resources.GetCredit(survey.normalAnswerReward[i].credit);
                        break;
                    }
                }
            }

            int cnt = 0;

            foreach (bool suvey in _weirdAnswers)
            {
                if (_weirdAnswers.Count == 0 || weirdResult.Count == 0)
                {
                    return;
                }

                if (suvey == true && !weirdResult[cnt])
                {
                    _gameManager.Resources.SubstractLife(surveys[cnt].damage);
                }
                else if (suvey == false && weirdResult[cnt])
                {
                    IsDead = true;
                }

                cnt++;
            }
        }

        public void GetNormalSuveyResult(NormalSurveyTypeHandler type) // UnityEvent 직렬화
        {
            _normalAnswers.Add(type.NormalAnswer);
        }

        public void GetWeridSurveyResult(bool value) // UnityEvent 직렬화
        {
            if (_gameManager.CurrentSurvey.weirdQuiz)
                _weirdAnswers.Add(value);
        }

        private void ResetSurveyResult()
        {
            _normalAnswers.Clear();
            _weirdAnswers.Clear();
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

            _gameManager.TodaySurveies = _gameManager.DailySurveyList[_gameManager.CurrentDay];

            ResetSurveyResult(); // 이전 설문 조사 내용 기록 삭제
        }
        
        public void InitSurveyQuiz() // UI를 초기화 해주는 작업
        {
            if (_currentSurveyIndex >= _gameManager.TodaySurveies.list.Count || _gameManager.CurrentDay >= _gameManager.DailySurveyList.Length)
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
            
            _gameManager.CurrentSurvey = _gameManager.TodaySurveies.list[_currentSurveyIndex];
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

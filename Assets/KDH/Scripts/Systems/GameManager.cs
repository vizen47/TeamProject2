using System.Collections.Generic;
using KDH.Scripts.CoreLib;
using KDH.Scripts.Survey;
using UnityEngine;

namespace KDH.Scripts.Systems
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [Header("Settings")]
        [field: SerializeField] public int MaxDayAmount { get; private set; }
        [field: SerializeField] public ApplySurveyDayData[] ApplySurveyCountPerDay { get; private set; }
        public SurveyQuizSO CurrentSurvey { get; set; }
        public int CurrentDay { get; private set; }

        [Header("Data")]
        [field: SerializeField] public List<SurveyQuizSO> TodaySurveys { get; private set; } = new();
        [field: SerializeField] public List<SurveyQuizSO> SurveyData { get; private set; } = new();
        
        [Header("Modules")]
        [field: SerializeField] public Survey.Survey Survey { get; private set; }
        [field: SerializeField] public Internet.Internet Internet { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            Survey.ResetTodaySurvey();
            Survey.InitSurvey();
        }
        
        public void ContinueNextDay()
        {
            if (!Survey.HasCompletedSurvey)
            {
                UIManager.Instance.PopupUI.Target.gameObject.SetActive(true);
                UIManager.Instance.PopupUI.SetUI("자기 전 모든 설문조사에 응시해야 합니다.");
                return;
            }

            CurrentDay++;
#if UNITY_EDITOR
            Debug.Log($"현재 {CurrentDay}일차");
#endif
            Survey.ResetTodaySurvey();
            Survey.InitSurvey();
            Survey.InitSurveyQuiz();
            UIManager.Instance.FadeInOutUI.Fade(1f, 1f, 1f);
        }
    }
}

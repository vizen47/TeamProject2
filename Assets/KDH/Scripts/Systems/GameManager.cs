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
        public List<SurveyQuizSO> TodaySurveys { get; private set; } = new();
        public int CurrentDay { get; private set; }
        
        [Header("Moduels")]
        [field: SerializeField] public Survey.Survey Survey { get; private set; }
        [field: SerializeField] public Internet.Internet Internet { get; private set; }

        [Header("Data")]
        [field: SerializeField] public List<SurveyQuizSO> SurveyData { get; private set; } = new();

        protected override void Awake()
        {
            base.Awake();

            Survey.InitSurvey();
        }

        public void ContinueNextDay()
        {
            Debug.Log($"현재 {CurrentDay}일차");
            Survey.InitSurvey();
            Survey.InitSurveyQuiz();
            CurrentDay++;
        }
    }
}

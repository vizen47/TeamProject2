using System.Collections.Generic;
using KDH.Scripts.Survey;
using UnityEngine;

namespace KDH.Scripts.Systems
{
    public class GameManager : MonoBehaviour
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
        [field: SerializeField] public UIManager UIManager { get; private set; }
        [field: SerializeField] public Survey.Survey SurveyUI { get; private set; }
        [field: SerializeField] public Store.StoreUI StoreUI { get; private set; }
        [field: SerializeField] public Store.Store Store { get; private set; }

        private void Awake()
        {
            SurveyUI.ResetTodaySurvey();
            SurveyUI.InitSurvey();
        }

        public void ContinueNextDay()
        {
            if (!SurveyUI.HasCompletedSurvey)
            {
                UIManager.PopupUI.Target.gameObject.SetActive(true);
                UIManager.PopupUI.SetUI("자기 전 모든 설문조사에 응시해야 합니다.");
                return;
            }

            CurrentDay++;
#if UNITY_EDITOR
            Debug.Log($"현재 {CurrentDay}일차");
#endif
            SurveyUI.ResetTodaySurvey();
            SurveyUI.InitSurvey();
            SurveyUI.InitSurveyQuiz();
            Store.ResetItems();
            UIManager.NextDayFadeInOutUI.Fade(1f, 1f, 1.25f, $"{CurrentDay}일차");
        }
    }
}

using KDH.Scripts.Survey;
using UnityEngine;

namespace KDH.Scripts.Systems
{
    public class GameManager : MonoBehaviour
    {
        [Header("Settings")]
        public SurveyQuizSO CurrentSurvey { get; set; }
        public int CurrentDay { get; private set; }

        [Header("Data")]
        [field: SerializeField] public DailySurveyListSO[] DailySurveyList { get; private set; }
        public DailySurveyListSO TodaySurveys { get; set; }
        [Header("Modules")] 
        [field: SerializeField] public UIManager UIManager { get; private set; }
        [field: SerializeField] public Survey.Survey SurveyUI { get; private set; }
        [field: SerializeField] public Store.StoreUI StoreUI { get; private set; }
        [field: SerializeField] public Store.Store Store { get; private set; }

        private void Awake()
        {
            SurveyUI.ResetTodaySurvey();
        }

        public void ContinueNextDay()
        {
            if (!SurveyUI.HasCompletedSurvey)
            {
                UIManager.PopupUI.SetUI("자기 전 모든 설문조사에 응시해야 합니다.");
                return;
            }

            CurrentDay++;

#if UNITY_EDITOR
            Debug.Log($"현재 {CurrentDay}일차");
#endif

            SurveyUI.ResetTodaySurvey();
            SurveyUI.InitSurveyQuiz();
            Store.ResetItems();
            UIManager.NextDayFadeInOutUI.Fade(1f, 1f, 1.25f, $"{CurrentDay}일차");
        }
    }
}

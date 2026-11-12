using KDH.Scripts.Mails;
using KDH.Scripts.Stores;
using KDH.Scripts.Surveies;
using KDH.Scripts.WeirdEvents;
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
        public DailySurveyListSO TodaySurveies { get; set; }
        [Header("Modules")]
        [field: SerializeField] public WeirdEventManager WeirdEventManager { get; private set; }
        [field: SerializeField] public ResourceManager Resources { get; private set; }
        [field: SerializeField] public UIManager UIManager { get; private set; }
        [field: SerializeField] public Survey Survey { get; private set; }
        [field: SerializeField] public StoreUI StoreUI { get; private set; }
        [field: SerializeField] public Store Store { get; private set; }
        [field: SerializeField] public Mail MailObj { get; private set; }

        private void Awake()
        {
            Survey.ResetTodaySurvey();
        }

        public void ContinueNextDay()
        {
            if (!Survey.HasCompletedSurvey)
            {
                UIManager.PopupUI.SetUI("자기 전 모든 설문조사에 응시해야 합니다.");
                return;
            }
            

            Survey.ResultSurvey(TodaySurveies.list);
            CurrentDay++;

#if UNITY_EDITOR
            Debug.Log($"현재 {CurrentDay}일차");
#endif

            foreach (var d in Resources.DeliveryItemDict)
            {
                Resources.AddMailItem(d.Key, d.Value);
            }

            MailObj.InitItem(Resources.DeliveryItemDict);
            Survey.ResetTodaySurvey();
            Survey.InitSurveyQuiz();
            Store.ResetItems();

            if (Survey.IsDead)
            {
                Debug.Log("이상현상이 있었는데 없다고 했습니다 사망.");
            }

            UIManager.NextDayFadeInOutUI.Fade(1f, 1f, 1.25f, $"{CurrentDay}일차", () =>
            {
                AbstractWeirdEvent weirdEvent = WeirdEventManager.GetWeirdEventRandom(DailySurveyList[CurrentDay]);

                WeirdEventManager.EndEvents(weirdEvent);
                WeirdEventManager.StartEvents(weirdEvent);
            });
        }
    }
}

using KDH.Scripts.Surveies;
using System.Collections.Generic;
using UnityEngine;

namespace KDH.Scripts.WeirdEvents
{
    public class WeirdEventManager : MonoBehaviour
    {
        public AbstractWeirdEvent AbstractWeird { get; private set; }

        public AbstractWeirdEvent GetWeirdEventRandom(DailySurveyListSO list)
        {
            if (list == null)
            {
                Debug.Log("WeirdManager에 값이 없음");
                return null;
            }

            List<AbstractWeirdEvent> abstractWeirdEvents = new();

            foreach (var weirdEvent in list.list)
            {
                if (weirdEvent.weirdEventSO != null)
                {
                    abstractWeirdEvents.Add(weirdEvent.weirdEventSO.GetComponent<AbstractWeirdEvent>());
                }
            }

            int index = Random.Range(0, abstractWeirdEvents.Count);

            return abstractWeirdEvents.Count != 0 ? abstractWeirdEvents[index] : null;
        }

        public void StartEvents(AbstractWeirdEvent weirdEvent)
        {
            if (weirdEvent == null)
            {   
                return;
            }

            AbstractWeird = weirdEvent;
            weirdEvent.OnStartEvent();
        }

        public void EndEvents(AbstractWeirdEvent weirdEvent)
        {
            if (weirdEvent == null)
            {
               return;
            }

            weirdEvent.OnEndEvent();
        }
    }
}

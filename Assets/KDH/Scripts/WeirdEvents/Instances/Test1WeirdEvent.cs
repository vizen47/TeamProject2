using KDH.Scripts.WeirdEvents;
using UnityEngine;

namespace Assets.KDH.Scripts.WeirdEvents.Instances
{
    public class Test1WeirdEvent : AbstractWeirdEvent
    {
        public override void OnStartEvent()
        {
            Debug.Log("이벤트1 시작");
        }

        public override void OnEndEvent()
        {
            Debug.Log("이벤트1 종료");
        }
    }
}

using KDH.Scripts.WeirdEvents;
using UnityEngine;

namespace Assets.KDH.Scripts.WeirdEvents.Instances
{
    public class Test2WeirdEvent : AbstractWeirdEvent
    {
        public override void OnStartEvent()
        {
            Debug.Log("이벤트2 시작");
        }

        public override void OnEndEvent()
        {
            Debug.Log("이벤트2 종료");
        }
    }
}

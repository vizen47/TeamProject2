using UnityEngine;

namespace KDH.Scripts.WeirdEvents
{
    public abstract class AbstractWeirdEvent : MonoBehaviour
    {
        public abstract void OnStartEvent();
        public abstract void OnEndEvent();
    }
}

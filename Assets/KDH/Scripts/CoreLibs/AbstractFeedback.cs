using UnityEngine;

namespace KDH.Scripts.CoreLibs
{
    public abstract class AbstractFeedback : MonoBehaviour
    {
        public abstract void CreateFeedback();
        public abstract void FinishFeedback();

        private void OnDisable()
        {
            FinishFeedback();
        }
    }
}
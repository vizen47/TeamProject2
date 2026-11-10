using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KDH.Scripts.CoreLibs
{
    public class FeedbackPlayer : MonoBehaviour
    {
        private List<AbstractFeedback> _feedbacks = new();

        void Awake()
        {
            _feedbacks = GetComponents<AbstractFeedback>().ToList();
        }
        
        public void PlayAllFeedback()
        {
            _feedbacks.ForEach(feedback => feedback.CreateFeedback());
        }

        public void StopAllFeedback()
        {
            _feedbacks.ForEach(feedback => feedback.FinishFeedback());
        }
    }
}
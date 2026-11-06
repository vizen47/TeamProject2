using System.Collections.Generic;
using UnityEngine;

namespace KDH.Scripts.Surveies
{
    [CreateAssetMenu(fileName = "DailySurveyList", menuName = "SO/KDH/DailySurveyList")]
    public class DailySurveyListSO : ScriptableObject
    {
        public List<SurveyQuizSO> list = new List<SurveyQuizSO>();
    }
}

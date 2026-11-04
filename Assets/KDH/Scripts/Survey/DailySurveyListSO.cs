using KDH.Scripts.Survey;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailySurveyList", menuName = "SO/KDH/DailySurveyList")]
public class DailySurveyListSO : ScriptableObject
{
    public List<SurveyQuizSO> list = new List<SurveyQuizSO>();
}

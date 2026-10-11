using UnityEngine;

namespace KDH.Scripts.Survey
{
    public enum NormalCorrectAnswer
    {
        Yes,
        Idk,
        No
    }

    public enum WeiredCorrectAnswer
    {
        Yes,
        No
    }
    
    [CreateAssetMenu(fileName = "SurveyQuizData", menuName = "SO/KDH/SurveyQuizData",order = 0)]
    public class SurveyQuizSO : ScriptableObject
    {
        [TextArea]
        public string mainQuizText;
        
        [Header("이상현상 문제인가요?")]
        public bool weirdQuiz;
        
        // [ShowIf("hasBonusStats")]
        public int bonusAttackPower;
        public NormalCorrectAnswer normalCorrectAnswer;
    }
}

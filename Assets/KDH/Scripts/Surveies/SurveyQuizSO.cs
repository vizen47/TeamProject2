using System;
using UnityEngine;

namespace KDH.Scripts.Surveies
{
    #region  DataFrom

    public enum NormalAnswer
    {
        Yes,
        Idk,
        No
    }

    public enum WeirdAnswer
    {
        Yes,
        No
    }

    [Serializable]
    public struct NormalReward
    {
        public int credit;
        public NormalAnswer answer;
    }

    #endregion

    [CreateAssetMenu(fileName = "SurveyQuizData", menuName = "SO/KDH/SurveyQuizData", order = 0)]
    public class SurveyQuizSO : ScriptableObject
    {
        
        [Header("설정")][TextArea]
        public string mainQuizText;
        
        [Header("이상현상 문제인가요?")]
        public bool weirdQuiz;
        public GameObject weirdEventSO;
        public NormalAnswer normalAnswer;
        public WeirdAnswer weirdAnswer;
        public NormalReward[] normalAnswerReward;
        public bool isWeird;
        public int damage;
    }
}

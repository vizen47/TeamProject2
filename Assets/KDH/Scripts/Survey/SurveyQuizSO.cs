using System;
using UnityEngine;

namespace KDH.Scripts.Survey
{
    [CreateAssetMenu(fileName = "SurveyQuizData", menuName = "SO/KDH/SurveyQuizData",order = 0)]
    public class SurveyQuizSO : ScriptableObject
    {
        #region  DataFrom

        public enum NormalAnswer
        {
            Yes,
            Idk,
            No
        }

        public enum WeiredAnswer
        {
            Yes,
            No
        }

        public enum Level
        {
            Easy,
            Normal,
            Hard,
            Insane
        }

        [Serializable]
        public struct NormalReward
        {
            public int credit;
            public NormalAnswer answer;
        }

        [Serializable]
        public struct WeiredReward
        {
            public int credit;
            public WeiredAnswer answer;
        }

        #endregion
        
        public Level level;
        [Header("설정")][TextArea]
        public string mainQuizText;
        
        [Header("이상현상 문제인가요?")]
        public bool weirdQuiz;
        public NormalAnswer normalAnswer;
        public WeiredAnswer weirdAnswer;
        public NormalReward[] normalAnswerReward;
        public WeiredReward[] weiredAnswerReward;
    }
}

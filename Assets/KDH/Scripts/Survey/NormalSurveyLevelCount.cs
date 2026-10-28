using System;
using UnityEngine;

namespace KDH.Scripts.Survey
{
    [Serializable]
    public struct NormalSurveyLevelCount
    {
        [Header("일반 설문 난이도의 개수")] 
        public int tutorialCount;
        public int easyCount;
        public int normalCount;
        public int hardCount;
        public int insaneCount;
    }
}
using System;

namespace KDH.Scripts.Survey
{
    [Serializable]
    public struct ApplySurveyDayData
    {
        public NormalSurveyLevelCount normalSurveyLevelCount;
        public WeirdSurveyLevelCount weirdSurveyLevelCount;
    }
}
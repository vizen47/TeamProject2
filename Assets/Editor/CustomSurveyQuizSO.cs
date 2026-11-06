using KDH.Scripts.Surveies;
using UnityEditor;
using UnityEngine;
using static Codice.CM.Common.Serialization.Replication.BasePackageReader;

[CustomEditor(typeof(SurveyQuizSO))]
public class CustomSurveyQuizSO : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // 커스텀 시작

        var mainQuizText = serializedObject.FindProperty("mainQuizText"); // SO에 있는 값 가져오기
        var weirdQuizProperty = serializedObject.FindProperty("weirdQuiz"); // SO에 있는 값 가져와서 주소 저장
        
        EditorGUILayout.PropertyField(mainQuizText);
        EditorGUILayout.PropertyField(weirdQuizProperty); // 인스펙터에 그리기

        if (weirdQuizProperty.boolValue) // 가져왔던 데이터, bool
        {
            var isWeird = serializedObject.FindProperty("isWeird");
            EditorGUILayout.PropertyField(isWeird, new GUIContent("정답 설정"));

            if (isWeird.boolValue)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("weirdEventSO"), new GUIContent("이벤트"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("damage"), new GUIContent("데미지 설정")); // 인스펙터에 그리기
            }

        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("normalAnswerReward"), new GUIContent("일반 설문조사 설정")); // 인스펙터에 그리기
        }

        serializedObject.ApplyModifiedProperties(); // 커스텀 끝
    }
}
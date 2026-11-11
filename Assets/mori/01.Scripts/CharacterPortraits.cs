using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Character Portraits")]
public class CharacterPortraits : ScriptableObject
{
    [System.Serializable]
    public struct Expression
    {
        public string key;
        public Sprite sprite;
    }

    public string speakerName;
    public Expression[] expressions;
    public Expression defaultExpression => expressions[0];
}

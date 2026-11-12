using UnityEngine;

public enum Type
{
    Food,
    Drink,
    Tool,
    misc
}

public enum ItemSpecialEvent
{
    None,
    HiddenEndingTrigger,
    NegativeEvent
}

[CreateAssetMenu(fileName = "ItemData", menuName = "SO/DHM/ItemData")]
public class ItemSO : ScriptableObject
{
    [Header("기본 정보")]
    public string itemID;
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public int cost;

    [Header("회복 설정")]
    public Type type;
    public float hungerAmount;
    public float thirstAmount;

    [Header("특수 효과 설정")]
    public ItemSpecialEvent specialEvent;

    public void Use(CharacterStats target)
    {
        if (target == null)
        {
            Debug.LogWarning("회복시킬 대상이 지정되지 않음.");
            return;
        }

        target.RestoreHunger(hungerAmount);
        target.RestoreThirst(thirstAmount);
    }
}
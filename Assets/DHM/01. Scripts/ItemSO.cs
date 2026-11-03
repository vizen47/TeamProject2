using UnityEngine;

public enum RecoveryType
{
    Hunger,
    Thirst,
    Both
}

[CreateAssetMenu(fileName = "ItemData", menuName = "SO/DHM/ItemData")]
public class ItemSO : ScriptableObject
{
    [Header("기본 정보")]
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public int cost;
    
    [Header("회복 설정")]
    public RecoveryType recoveryType;
    public float hungerAmount;
    public float thirstAmount;

    public void Use(CharacterStats target)
    {
        if (target == null)
        {
            Debug.LogWarning("회복시킬 대상이 지정되지 않음.");
            return;
        }

        switch (recoveryType)
        {
            case RecoveryType.Hunger:
                target.RestoreHunger(hungerAmount);
                Debug.Log($"{itemName} 사용 -> {target.characterName}의 허기 {hungerAmount} 회복 (현재: {target.currentHunger})");
                break;

            case RecoveryType.Thirst:
                target.RestoreThirst(thirstAmount);
                Debug.Log($"{itemName} 사용 -> {target.characterName}의 갈증 {thirstAmount} 회복 (현재: {target.currentThirst})");
                break;

            case RecoveryType.Both:
                target.RestoreHunger(hungerAmount);
                target.RestoreThirst(thirstAmount);
                Debug.Log($"{itemName} 사용 -> {target.characterName}의 허기 {hungerAmount}, 갈증 {thirstAmount} 회복");
                break;
        }
    }
}
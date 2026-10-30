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

    // 아이템 사용 로직 예시
    public void Use()
    {
        switch (recoveryType)
        {
            case RecoveryType.Hunger:
                //PlayerStats.Instance.RestoreHunger(hungerAmount);
                Debug.Log($"{itemName} 사용 -> 허기 {hungerAmount} 회복");
                break;

            case RecoveryType.Thirst:
                //PlayerStats.Instance.RestoreThirst(thirstAmount);
                Debug.Log($"{itemName} 사용 -> 갈증 {thirstAmount} 회복");
                break;

            case RecoveryType.Both:
                //PlayerStats.Instance.RestoreHunger(hungerAmount);
                //PlayerStats.Instance.RestoreThirst(thirstAmount);
                Debug.Log($"{itemName} 사용 -> 허기 {hungerAmount}, 갈증 {thirstAmount} 회복");
                break;
        }
    }
}
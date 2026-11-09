using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public string characterName;

    [Header("스탯 설정")]
    public float maxHunger = 10f;
    public float currentHunger;
    public float maxThirst = 5f;
    public float currentThirst;

    [Header("일일 감소량")]
    public float hungerDecreasePerDay = 2.5f;
    public float thirstDecreasePerDay = 1.5f;

    public void Init()
    {
        currentHunger = maxHunger;
        currentThirst = maxThirst;
    }

    public void PassDay()
    {
        currentHunger -= hungerDecreasePerDay;
        currentThirst -= thirstDecreasePerDay;

        currentHunger = Mathf.Max(0f, currentHunger);
        currentThirst = Mathf.Max(0f, currentThirst);
    }

    public void RestoreHunger(float amount)
    {
        currentHunger += amount;
        currentHunger = Mathf.Clamp(currentHunger, 0f, maxHunger);
    }

    public void RestoreThirst(float amount)
    {
        currentThirst += amount;
        currentThirst = Mathf.Clamp(currentThirst, 0f, maxThirst);
    }
}

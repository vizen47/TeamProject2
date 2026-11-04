using UnityEngine;
using TMPro;

namespace DHM
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("생존자 정보")]
        public CharacterStats player = new CharacterStats { characterName = "백우빈", hungerDecreasePerDay = 2.5f, thirstDecreasePerDay = 1.5f };
        public CharacterStats sister = new CharacterStats { characterName = "백하연", hungerDecreasePerDay = 2.0f, thirstDecreasePerDay = 1.0f };

        private int currentDay = 1;

        [Header("플레이어 UI")]
        public TextMeshProUGUI playerHungerText;
        public TextMeshProUGUI playerThirstText;

        [Header("여동생 UI")]
        public TextMeshProUGUI sisterHungerText;
        public TextMeshProUGUI sisterThirstText;

        [Header("일차 카운트 UI")]
        public TextMeshProUGUI DayText;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            player.Init();
            sister.Init();

            UpdateAllUI();
        }

        public void OnClickNextDayButton()
        {
            currentDay++;
            DayText.text = $"{currentDay}일차";

            player.PassDay();
            CheckDeath(player);

            sister.PassDay();
            CheckDeath(sister);

            UpdateAllUI();
        }

        private void UpdateAllUI()
        {
            if (playerHungerText != null) playerHungerText.text = $"남은 허기: {player.currentHunger} / {player.maxHunger}";
            if (playerThirstText != null) playerThirstText.text = $"남은 갈증: {player.currentThirst} / {player.maxThirst}";

            if (sisterHungerText != null) sisterHungerText.text = $"남은 허기: {sister.currentHunger} / {sister.maxHunger}";
            if (sisterThirstText != null) sisterThirstText.text = $"남은 갈증: {sister.currentThirst} / {sister.maxThirst}";
        }

        private void CheckDeath(CharacterStats survivor)
        {
            if (survivor.currentHunger <= 0f)
            {
                Debug.LogError($"{survivor.characterName}이 아사함.");
            }

            if (survivor.currentThirst <= 0f)
            {
                Debug.LogError($"{survivor.characterName}이 탈수함.");
            }
        }
    }
}
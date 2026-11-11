using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

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

        private Dictionary<string, int> itemUseCounts = new Dictionary<string, int>();

        [Header("설정")]
        [SerializeField] private int hiddenEndingThreshold = 20;

        [Header("테스트 아이템 설정")]
        public ItemSO testItem;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            player.Init();
            sister.Init();

            if (DayText != null) DayText.text = $"{currentDay}일차";
            UpdateAllUI();
        }

        private void Update()
        {
            if (Keyboard.current.qKey.wasPressedThisFrame)
            {
                if (testItem != null)
                {
                    UseItemOnCharacter(testItem, player);
                }
                else
                {
                    Debug.LogWarning("GameManager에 테스트 아이템이 연결되지 않았습니다!");
                }
            }

            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (testItem != null)
                {
                    UseItemOnCharacter(testItem, player);
                }
                else
                {
                    Debug.LogWarning("GameManager에 테스트 아이템이 연결되지 않았습니다!");
                }
            }
        }

        public void OnClickNextDayButton()
        {
            currentDay++;
            if (DayText != null) DayText.text = $"{currentDay}일차";

            player.PassDay();
            CheckDeath(player);

            sister.PassDay();
            CheckDeath(sister);

            UpdateAllUI();
        }

        public void UseItemOnCharacter(ItemSO item, CharacterStats target)
        {
            if (item == null || target == null) return;

            item.Use(target);

            HandleSpecialEffect(item);

            UpdateAllUI();
        }

        private void HandleSpecialEffect(ItemSO item)
        {
            switch (item.specialEvent)
            {
                case ItemSpecialEvent.HiddenEndingTrigger:
                    if (!itemUseCounts.ContainsKey(item.itemID))
                    {
                        itemUseCounts[item.itemID] = 0;
                    }

                    itemUseCounts[item.itemID]++;
                    Debug.Log($"{item.itemName} 누적 횟수: {itemUseCounts[item.itemID]}회");

                    if (itemUseCounts[item.itemID] >= hiddenEndingThreshold)
                    {
                        TriggerHiddenEnding(item);
                    }
                    break;

                case ItemSpecialEvent.NegativeEvent:
                    Debug.LogWarning($"{item.itemName} 아이템 복용. 부정적 이벤트 발생.");
                    break;

                case ItemSpecialEvent.None:
                default:
                    break;
            }
        }

        private void TriggerHiddenEnding(ItemSO item)
        {
            Debug.LogError($"{item.itemName} 아이템을 충분히 섭취하여 히든 엔딩으로 진입");
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
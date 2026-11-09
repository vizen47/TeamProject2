using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace DHM
{
    public class InventoryUIManager : MonoBehaviour
    {
        [Header("카테고리 패널")]
        public GameObject foodCategoryPanel;

        [Header("우측 상세 정보 UI (지정하신 이름들)")]
        public Image itemIconImage;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI itemDescText;
        public TextMeshProUGUI itemCountText;

        [Header("초상화 하단 허기/갈증 변화 텍스트")]
        public TextMeshProUGUI playerHPText;
        public TextMeshProUGUI sisterHPText;

        private Dictionary<ItemSO, int> itemCounts = new Dictionary<ItemSO, int>();
        private ItemSO currentSelectedItem;

        private void Start()
        {
            if (foodCategoryPanel != null)
                foodCategoryPanel.SetActive(false);
        }

        public void OnClickFoodCategoryButton()
        {
            if (foodCategoryPanel != null)
            {
                bool isActive = foodCategoryPanel.activeSelf;
                foodCategoryPanel.SetActive(!isActive);
            }
        }

        public void OnClickItemButton(ItemSO clickedItem)
        {
            if (clickedItem == null) return;

            currentSelectedItem = clickedItem;

            if (!itemCounts.ContainsKey(clickedItem))
            {
                itemCounts[clickedItem] = 5;
            }

            if (itemIconImage != null) itemIconImage.sprite = clickedItem.icon;
            if (itemNameText != null) itemNameText.text = clickedItem.itemName;
            if (itemDescText != null) itemDescText.text = clickedItem.description;
            if (itemCountText != null) itemCountText.text = $"보유 : {itemCounts[clickedItem]}개";

            UpdatePreviewHPText();
        }

        private void UpdatePreviewHPText()
        {
            if (currentSelectedItem == null) return;

            if (playerHPText != null)
            {
                float currentH = GameManager.Instance.player.currentHunger;
                float currentT = GameManager.Instance.player.currentThirst;
                float maxH = GameManager.Instance.player.maxHunger;
                float maxT = GameManager.Instance.player.maxThirst;

                if (currentSelectedItem.recoveryType == RecoveryType.Hunger)
                {
                    float afterH = Mathf.Clamp(currentH + currentSelectedItem.hungerAmount, 0f, maxH);
                    playerHPText.text = $"허기: {currentH} -> {afterH}";
                }
                else if (currentSelectedItem.recoveryType == RecoveryType.Thirst)
                {
                    float afterT = Mathf.Clamp(currentT + currentSelectedItem.thirstAmount, 0f, maxT);
                    playerHPText.text = $"갈증: {currentT} -> {afterT}";
                }
                else if (currentSelectedItem.recoveryType == RecoveryType.Both)
                {
                    float afterH = Mathf.Clamp(currentH + currentSelectedItem.hungerAmount, 0f, maxH);
                    float afterT = Mathf.Clamp(currentT + currentSelectedItem.thirstAmount, 0f, maxT);
                    playerHPText.text = $"허기: {currentH}->{afterH} / 갈증: {currentT}->{afterT}";
                }
            }

            if (sisterHPText != null)
            {
                float currentH = GameManager.Instance.sister.currentHunger;
                float currentT = GameManager.Instance.sister.currentThirst;
                float maxH = GameManager.Instance.sister.maxHunger;
                float maxT = GameManager.Instance.sister.maxThirst;

                if (currentSelectedItem.recoveryType == RecoveryType.Hunger)
                {
                    float afterH = Mathf.Clamp(currentH + currentSelectedItem.hungerAmount, 0f, maxH);
                    sisterHPText.text = $"허기: {currentH} -> {afterH}";
                }
                else if (currentSelectedItem.recoveryType == RecoveryType.Thirst)
                {
                    float afterT = Mathf.Clamp(currentT + currentSelectedItem.thirstAmount, 0f, maxT);
                    sisterHPText.text = $"갈증: {currentT} -> {afterT}";
                }
                else if (currentSelectedItem.recoveryType == RecoveryType.Both)
                {
                    float afterH = Mathf.Clamp(currentH + currentSelectedItem.hungerAmount, 0f, maxH);
                    float afterT = Mathf.Clamp(currentT + currentSelectedItem.thirstAmount, 0f, maxT);
                    sisterHPText.text = $"허기: {currentH}->{afterH} / 갈증: {currentT}->{afterT}";
                }
            }
        }

        public void OnClickFeedPlayer()
        {
            UseItemOnTarget(GameManager.Instance.player);
        }

        public void OnClickFeedSister()
        {
            UseItemOnTarget(GameManager.Instance.sister);
        }

        private void UseItemOnTarget(CharacterStats target)
        {
            if (currentSelectedItem == null) return;

            if (itemCounts.ContainsKey(currentSelectedItem) && itemCounts[currentSelectedItem] > 0)
            {
                itemCounts[currentSelectedItem]--;

                if (itemCountText != null)
                    itemCountText.text = $"보유 : {itemCounts[currentSelectedItem]}개";

                //GameManager.Instance.UseItemOnCharacter(currentSelectedItem, target);

                UpdatePreviewHPText();
            }
            else
            {
                Debug.LogWarning("아이템 개수가 부족합니다!");
            }
        }
    }
}
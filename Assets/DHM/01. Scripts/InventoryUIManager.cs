using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

namespace DHM
{
    public class InventoryUIManager : MonoBehaviour
    {
        [Header("카테고리")]
        public GameObject foodCategory;
        public GameObject drinkCategory;
        public GameObject toolCategory;

        [Header("우측 상세 정보 UI")]
        public GameObject itemInfoPanel;
        public Image itemIconImage;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI itemDescriptionText;
        public TextMeshProUGUI itemCountText;

        [Header("초상화 하단 허기/갈증 변화 텍스트")]
        public TextMeshProUGUI playerHPText;
        public TextMeshProUGUI sisterHPText;

        private Dictionary<ItemSO, int> itemCounts = new Dictionary<ItemSO, int>();
        private ItemSO currentSelectedItem;

        private void Start()
        {
            if (foodCategory != null)
                foodCategory.SetActive(false);

            if (drinkCategory != null)
                drinkCategory.SetActive(false);

            if (toolCategory != null)
                toolCategory.SetActive(false);

            if (itemInfoPanel != null)
                itemInfoPanel.SetActive(false);
        }

        public void OnClickFoodCategoryButton()
        {
            if (foodCategory != null)
            {
                bool isActive = foodCategory.activeSelf;
                foodCategory.SetActive(!isActive);
            }
        }

        public void OnClickDrinkCategoryButton()
        {
            if (drinkCategory != null)
            {
                bool isActive = drinkCategory.activeSelf;
                drinkCategory.SetActive(!isActive);
            }
        }

        public void OnClickToolCategoryButton()
        {
            if (toolCategory != null)
            {
                bool isActive = toolCategory.activeSelf;
                toolCategory.SetActive(!isActive);
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

            if (itemInfoPanel != null)
                itemInfoPanel.SetActive(true);

            if (itemIconImage != null) itemIconImage.sprite = clickedItem.icon;
            if (itemNameText != null) itemNameText.text = clickedItem.itemName;
            if (itemDescriptionText != null) itemDescriptionText.text = clickedItem.description;
            if (itemCountText != null) itemCountText.text = $"보유 : {itemCounts[clickedItem]}개";

            UpdatePreviewHPText();
        }

        public void CloseItemPanel()
        {
            if (itemInfoPanel != null)
                itemInfoPanel.SetActive(false);

            currentSelectedItem = null;
        }

        public void UpdatePreviewHPText()
        {
            if (itemInfoPanel == null || !itemInfoPanel.activeSelf || currentSelectedItem == null) return;

            if (playerHPText != null)
            {
                float currentH = GameManager.Instance.player.currentHunger;
                float currentT = GameManager.Instance.player.currentThirst;
                float maxH = GameManager.Instance.player.maxHunger;
                float maxT = GameManager.Instance.player.maxThirst;

                float afterH = Mathf.Clamp(currentH + currentSelectedItem.hungerAmount, 0f, maxH);
                float afterT = Mathf.Clamp(currentT + currentSelectedItem.thirstAmount, 0f, maxT);
                playerHPText.text = $"허기: {currentH} -> {afterH}\n갈증: {currentT} -> {afterT}";
            }

            if (sisterHPText != null)
            {
                float currentH = GameManager.Instance.sister.currentHunger;
                float currentT = GameManager.Instance.sister.currentThirst;
                float maxH = GameManager.Instance.sister.maxHunger;
                float maxT = GameManager.Instance.sister.maxThirst;

                float afterH = Mathf.Clamp(currentH + currentSelectedItem.hungerAmount, 0f, maxH);
                float afterT = Mathf.Clamp(currentT + currentSelectedItem.thirstAmount, 0f, maxT);
                sisterHPText.text = $"허기: {currentH} -> {afterH}\n갈증: {currentT} -> {afterT}";
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

                GameManager.Instance.UseItemOnCharacter(currentSelectedItem, target);

                UpdatePreviewHPText();
            }
            else
            {
                Debug.LogWarning("아이템 개수가 부족합니다!");
            }
        }
    }
}
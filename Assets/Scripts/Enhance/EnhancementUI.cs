using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnhancementUI : MonoBehaviour
{
    public Image itemImage;
    public Text itemNameText;
    public Text enhancementLevelText;
    public Text successRateText;
    public Text costText;
    public Text resultText;
    public Button enhanceButton;

    public EquipmentEnhancer enhancer; //강화시스템
    public Player player;
    private ItemData currentItem;

    void Start()
    {
        enhanceButton.onClick.AddListener(OnEnhanceClicked);
    }

    public void SetItem(ItemData item)
    {
        currentItem = item;

        if (item != null)
        {
            itemImage.sprite = item.Icon;
            itemNameText.text = item.itemName;
            enhancementLevelText.text = $"+{item.enhancementLevel}";
            successRateText.text = $"성공 확률: {enhancer.successRate * 100}%";
            float cost = item.enhancementCost * Mathf.Pow(item.enhancementCostMultiplier, item.enhancementLevel);
            costText.text = $"비용: {(int)cost} 골드";
        }
    }

    void OnEnhanceClicked()
    {
        if (currentItem == null || player == null) return;

        bool success = enhancer.Enhance(currentItem, player);

        resultText.text = success ? "강화 성공!" : "강화 실패!";
        SetItem(currentItem); //UI갱신
    }
}

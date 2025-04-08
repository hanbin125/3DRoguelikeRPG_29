using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItem : MonoBehaviour
{
    [SerializeField] private Image itemimage;
    [SerializeField] private TextMeshProUGUI power;
    [SerializeField] private TextMeshProUGUI mana;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI Reduction;
    [SerializeField] private TextMeshProUGUI CriticalChance;
    [SerializeField] private TextMeshProUGUI CriticalDamage;
    [SerializeField] private Button btn_equip;
    [SerializeField] private Button btn_Release;

    private ItemData currentItem;
    private System.Action<ItemData> onEquipCallback;
    private System.Action<ItemData> onReleaseCallback;

    private void Awake()
    {
        btn_equip.onClick.AddListener(OnEquipButtonClicked);
        btn_Release.onClick.AddListener(OnReleaseButtonClicked);
    }

    public void Initialize(System.Action<ItemData> equipCallback, System.Action<ItemData> releaseCallback)
    {
        onEquipCallback = equipCallback;
        onReleaseCallback = releaseCallback;
    }

    public void Show(ItemData item)
    {
        if (item == null) return;
        
        currentItem = item;
        gameObject.SetActive(true);
        UpdateUI();
    }

    public void Hide()
    {
        currentItem = null;
        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        if (currentItem == null) return;

        // 아이템 이미지 설정
        if (currentItem.Icon != null)
            itemimage.sprite = currentItem.Icon;

        // 능력치 표시
        power.text = $"공격력: {currentItem.GetOptionValue(ConditionType.Power)}";
        mana.text = $"마나: {currentItem.GetOptionValue(ConditionType.Mana)}";
        health.text = $"체력: {currentItem.GetOptionValue(ConditionType.Health)}";
        speed.text = $"속도: {currentItem.GetOptionValue(ConditionType.Speed)}";
        Reduction.text = $"피해감소: {currentItem.GetOptionValue(ConditionType.reduction)}";
        CriticalChance.text = $"치명타확률: {currentItem.GetOptionValue(ConditionType.CriticalChance)}";
        CriticalDamage.text = $"치명타피해: {currentItem.GetOptionValue(ConditionType.CriticalDamage)}";
    }

    private void OnEquipButtonClicked()
    {
        if (currentItem != null)
        {
            onEquipCallback?.Invoke(currentItem);
        }
    }

    private void OnReleaseButtonClicked()
    {
        if (currentItem != null)
        {
            onReleaseCallback?.Invoke(currentItem);
        }
    }
}

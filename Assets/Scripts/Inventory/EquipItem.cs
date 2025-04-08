using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipItem : PopupUI
{
    [SerializeField] private Image itemimage;
    [SerializeField] private TextMeshProUGUI power;
    [SerializeField] private TextMeshProUGUI mana;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI Reduction;
    [SerializeField] private TextMeshProUGUI CriticalChance;
    [SerializeField] private TextMeshProUGUI CriticalDamage;

    private ItemData currentItem;

    public void Show(ItemData item)
    {
        if (item == null) return;
        
        currentItem = item;
        base.Show();
        UpdateUI();
    }

    public override void Hide()
    {
        currentItem = null;
        base.Hide();
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

    protected override void Clear()
    {
        base.Clear();
        currentItem = null;
    }
}

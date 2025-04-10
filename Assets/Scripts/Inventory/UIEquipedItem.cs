using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEquipedItem : PopupUI
{
    [SerializeField] private Image itemimage;
    [SerializeField] private TextMeshProUGUI power;
    [SerializeField] private TextMeshProUGUI mana;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI Reduction;
    [SerializeField] private TextMeshProUGUI CriticalChance;
    [SerializeField] private TextMeshProUGUI CriticalDamage;

    private ItemData EquipItem;

    protected override void Awake()
    {
        base.Awake();
        UIManager.Instance.RegisterUI(this);
    }
    public void Show(ItemData item)
    {
        if (item == null) return;
        
        EquipItem = item;
        base.Show();
        UpdateUI();
    }

    public override void Hide()
    {
        EquipItem = null;
        base.Hide();
    }

    private void UpdateUI()
    {
        if (EquipItem == null) return;

        // 아이템 이미지 설정
        if (EquipItem.Icon != null)
            itemimage.sprite = EquipItem.Icon;

        // 능력치 표시
        power.text = $"공격력: {EquipItem.GetOptionValue(ConditionType.Power)}";
        mana.text = $"마나: {EquipItem.GetOptionValue(ConditionType.Mana)}";
        health.text = $"체력: {EquipItem.GetOptionValue(ConditionType.Health)}";
        speed.text = $"속도: {EquipItem.GetOptionValue(ConditionType.Speed)}";
        Reduction.text = $"피해감소: {EquipItem.GetOptionValue(ConditionType.reduction)}";
        CriticalChance.text = $"치명타확률: {EquipItem.GetOptionValue(ConditionType.CriticalChance)}";
        CriticalDamage.text = $"치명타피해: {EquipItem.GetOptionValue(ConditionType.CriticalDamage)}";
    }

    protected override void Clear()
    {
        base.Clear();
        EquipItem = null;
    }
}

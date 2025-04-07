using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : BaseStat
{
    [SerializeField] private int baseHP = 100;
    [SerializeField] private int baseMP = 50;
    [SerializeField] private int baseSpeed = 5;
    [SerializeField] private int baseAttack = 10;
    [SerializeField] private float baseDMGReduction = 0f;
    [SerializeField] private int baseCriticalChance = 5;
    [SerializeField] private float baseCriticalDamage = 1.25f;

    private Dictionary<StatType, float> equipmentBonuses = new Dictionary<StatType, float>();
    private Dictionary<StatType, float> buffBonuses = new Dictionary<StatType, float>();

    protected override void InitializeStats()
    {
        base.InitializeStats();

        // 기본 스탯 설정
        SetStatValue(StatType.HP, baseHP);
        SetStatValue(StatType.MP, baseMP);
        SetStatValue(StatType.Attack, baseAttack);
    }

    public override float GetStatValue(StatType type)
    {
        float baseValue = base.GetStatValue(type);
        float equipBonus = equipmentBonuses.TryGetValue(type, out float equip) ? equip : 0f;
        float buffBonus = buffBonuses.TryGetValue(type, out float buff) ? buff : 0f;

        return baseValue + equipBonus + buffBonus;
    }

    public void AddEquipmentBonus(StatType type, float bonus)
    {
        if (!equipmentBonuses.ContainsKey(type))
            equipmentBonuses[type] = 0f;

        equipmentBonuses[type] += bonus;
        OnStatChanged(type);
    }

    public void AddBuff(StatType type, float bonus)
    {
        if (!buffBonuses.ContainsKey(type))
            buffBonuses[type] = 0f;

        buffBonuses[type] += bonus;
        OnStatChanged(type);
    }

    protected override void OnStatChanged(StatType type)
    {
        base.OnStatChanged(type);
        // UI 업데이트 등 추가 작업
    }
}
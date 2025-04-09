using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : BaseStat
{
    [SerializeField] int _maxHP = 100;
    [SerializeField] int _baseHP = 100;
    [SerializeField] int _maxMP = 50;
    [SerializeField] int _baseMP = 50;
    [SerializeField] int _baseSpeed = 5;
    [SerializeField] int _baseAttack = 10;
    [SerializeField] float _baseDMGReduction = 0f;
    [SerializeField] int _baseCriticalChance = 5;
    [SerializeField] float _baseCriticalDamage = 1.25f;

    public event Action<float> OnMaxHPChanged;
    public event Action<float> OnHPChanged;
    public event Action<float> OnMaxMPChanged;
    public event Action<float> OnMPChanged;
    public event Action<float> OnSpeedChanged;
    public event Action<float> OnAttackChanged;
    public event Action<float> OnDMGReductionChanged;
    public event Action<float> OnCriticalChanceChanged;
    public event Action<float> OnCriticalDamageChanged;


    private void Awake()
    {
        InitializeStats();
    }
    private Dictionary<StatType, float> equipmentBonuses = new Dictionary<StatType, float>();
    private Dictionary<StatType, float> buffBonuses = new Dictionary<StatType, float>();

    protected override void InitializeStats()
    {
        base.InitializeStats();

        // 기본 스탯 설정
        SetStatValue(StatType.HP, _baseHP);
        SetStatValue(StatType.MaxHP, _maxHP);
        SetStatValue(StatType.MP, _baseMP);
        SetStatValue(StatType.MaxMP, _maxMP);
        SetStatValue(StatType.Attack, _baseAttack);
        SetStatValue(StatType.Speed, _baseSpeed);
        SetStatValue(StatType.DMGReduction, _baseDMGReduction);
        SetStatValue(StatType.CriticalChance, _baseCriticalChance);
        SetStatValue(StatType.CriticalDamage, _baseCriticalDamage);
    }

    public override float GetStatValue(StatType type)
    {
        float baseValue = base.GetStatValue(type);
        //float equipBonus = equipmentBonuses.TryGetValue(type, out float equip) ? equip : 0f;
        //float buffBonus = buffBonuses.TryGetValue(type, out float buff) ? buff : 0f;

        return baseValue;
        //+ equipBonus + buffBonus;
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
        if (type == StatType.HP)
        {
            float currentHP = GetStatValue(StatType.HP);
            OnHPChanged?.Invoke(currentHP);
        }
        else if (type == StatType.Speed)
        {
            float currentSpeed = GetStatValue(StatType.Speed);
            OnSpeedChanged?.Invoke(currentSpeed);
        }
        else if(type == StatType.MP)
        {

        }

    }
}
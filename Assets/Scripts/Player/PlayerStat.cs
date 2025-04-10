using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : BaseStat<PlayerStatType>
{
    [SerializeField] int _maxHP = 100;
    [SerializeField] int _baseHP = 100;
    [SerializeField] int _maxMP = 50;
    [SerializeField] int _baseMP = 50;
    [SerializeField] int _baseSpeed = 5;
    [SerializeField] int _baseAttack = 10;
    [SerializeField] float _baseDMGReduction = 1f;
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
    private Dictionary<PlayerStatType, float> equipmentBonuses = new Dictionary<PlayerStatType, float>();
    private Dictionary<PlayerStatType, float> buffBonuses = new Dictionary<PlayerStatType, float>();

    protected override void InitializeStats()
    {
        base.InitializeStats();

        // 기본 스탯 설정
        SetStatValue(PlayerStatType.HP, _baseHP);
        SetStatValue(PlayerStatType.MaxHP, _maxHP);
        SetStatValue(PlayerStatType.MP, _baseMP);
        SetStatValue(PlayerStatType.MaxMP, _maxMP);
        SetStatValue(PlayerStatType.Attack, _baseAttack);
        SetStatValue(PlayerStatType.Speed, _baseSpeed);
        SetStatValue(PlayerStatType.DMGReduction, _baseDMGReduction);
        SetStatValue(PlayerStatType.CriticalChance, _baseCriticalChance);
        SetStatValue(PlayerStatType.CriticalDamage, _baseCriticalDamage);
    }

    public override float GetStatValue(PlayerStatType type)
    {
        float baseValue = base.GetStatValue(type);
        //float equipBonus = equipmentBonuses.TryGetValue(type, out float equip) ? equip : 0f;
        //float buffBonus = buffBonuses.TryGetValue(type, out float buff) ? buff : 0f;

        return baseValue;
        //+ equipBonus + buffBonus;
    }

    public void AddEquipmentBonus(PlayerStatType type, float bonus)
    {
        if (!equipmentBonuses.ContainsKey(type))
            equipmentBonuses[type] = 0f;

        equipmentBonuses[type] += bonus;
        OnStatChanged(type);
    }

    public void AddBuff(PlayerStatType type, float bonus)
    {
        if (!buffBonuses.ContainsKey(type))
            buffBonuses[type] = 0f;

        buffBonuses[type] += bonus;
        OnStatChanged(type);
    }

    protected override void OnStatChanged(PlayerStatType type)
    {
        base.OnStatChanged(type);

        switch (type)
        {
            case PlayerStatType.MaxHP:
                float maxHP = GetStatValue(PlayerStatType.MaxHP);
                OnMaxHPChanged?.Invoke(maxHP);
                break;
            case PlayerStatType.HP:
                float currentHP = GetStatValue(PlayerStatType.HP);
                OnHPChanged?.Invoke(currentHP);
                break;
            case PlayerStatType.MaxMP:
                float maxMP = GetStatValue(PlayerStatType.MaxMP);
                OnMaxMPChanged?.Invoke(maxMP);
                break;
            case PlayerStatType.MP:
                float currentMP = GetStatValue(PlayerStatType.MP);
                OnMPChanged?.Invoke(currentMP);
                break;
            case PlayerStatType.Speed:
                float currentSpeed = GetStatValue(PlayerStatType.Speed);
                OnSpeedChanged?.Invoke(currentSpeed);
                break;
            case PlayerStatType.Attack:
                float currentAttack = GetStatValue(PlayerStatType.Attack);
                OnAttackChanged?.Invoke(currentAttack);
                break;
            case PlayerStatType.DMGReduction:
                float currentDMGReduction = GetStatValue(PlayerStatType.DMGReduction);
                OnDMGReductionChanged?.Invoke(currentDMGReduction);
                break;
            case PlayerStatType.CriticalChance:
                float currentCriticalChance = GetStatValue(PlayerStatType.CriticalChance);
                OnCriticalChanceChanged?.Invoke(currentCriticalChance);
                break;
            case PlayerStatType.CriticalDamage:
                float currentCriticalDamage = GetStatValue(PlayerStatType.CriticalDamage);
                OnCriticalDamageChanged?.Invoke(currentCriticalDamage);
                break;
            default:
                break;
        }
    }
}
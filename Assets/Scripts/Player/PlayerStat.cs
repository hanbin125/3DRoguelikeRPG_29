using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : BaseStat
{
    [SerializeField] int _baseHP = 100;
    [SerializeField] int _baseMP = 50;
    [SerializeField] int _baseSpeed = 5;
    [SerializeField] int _baseAttack = 10;
    [SerializeField] float _baseDMGReduction = 0f;
    [SerializeField] int _baseCriticalChance = 5;
    [SerializeField] float _baseCriticalDamage = 1.25f;
    [SerializeField] TextMeshProUGUI _hpText;
    [SerializeField] TextMeshProUGUI _mpText;
    [SerializeField] TextMeshProUGUI _attackText;
    [SerializeField] TextMeshProUGUI _speedText;
    [SerializeField] TextMeshProUGUI _dmgReductionText;
    [SerializeField] TextMeshProUGUI _criticalChanceText;
    [SerializeField] TextMeshProUGUI _criticalDamageText;

    private void Start()
    {
        InitializeStats();
        SetBaseHP();
    }
    private Dictionary<StatType, float> equipmentBonuses = new Dictionary<StatType, float>();
    private Dictionary<StatType, float> buffBonuses = new Dictionary<StatType, float>();

    protected override void InitializeStats()
    {
        base.InitializeStats();

        // 기본 스탯 설정
        SetStatValue(StatType.HP, _baseHP);
        SetStatValue(StatType.MP, _baseMP);
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

    public void SetBaseHP()
    {
        _hpText.text = GetStatValue(StatType.HP).ToString("F0");
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
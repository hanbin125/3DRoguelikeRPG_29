using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : BaseStat<PlayerStatType>
{
    public event Action<PlayerStat> OnStatsChanged;

    private Dictionary<PlayerStatType, float> _equipmentBonuses = new Dictionary<PlayerStatType, float>();
    private Dictionary<PlayerStatType, float> _buffBonuses = new Dictionary<PlayerStatType, float>();
    //private Dictionary<PlayerStatType, float> _totalStats = new Dictionary<PlayerStatType, float>();
    private void Awake()
    {
        InitializeStats();
    }
    /// <summary>
    /// 스탯 0으로 초기화
    /// </summary>
    protected override void InitializeStats()
    {
        //stas를 0으로 초기화 
        base.InitializeStats();
        foreach (PlayerStatType type in Enum.GetValues(typeof(PlayerStatType)))
        {
            _equipmentBonuses[type] = 0f;
            _buffBonuses[type] = 0f;
        }
    }

    public void InitBaseStat(PlayerStatData playerStatData)
    {
        if (playerStatData != null)
        {
            // ScriptableObject에서 기본값을 가져와 초기화
            foreach (PlayerStatType type in System.Enum.GetValues(typeof(PlayerStatType)))
            {
                float baseValue = playerStatData.GetBaseValue(type);
                SetStatValue(type, baseValue);
            }
        }
        else
        {
            Debug.LogWarning("PlayerStatData 이 없습니다.");
        }
    }
    public override float GetStatValue(PlayerStatType type)
    {
        float baseValue = base.GetStatValue(type);
        float equipBonus = _equipmentBonuses.TryGetValue(type, out float equip) ? equip : 0f;
        float buffBonus = _buffBonuses.TryGetValue(type, out float buff) ? buff : 0f;

        return baseValue + equipBonus + buffBonus;
    }

    public void AddEquipmentBonus(Dictionary<PlayerStatType, float> totalconditionTypes)
    {
        foreach (var stat in totalconditionTypes)
        {
            _equipmentBonuses[stat.Key] += stat.Value;
        }
        OnStatChanged();
    }

    public void AddBuff(PlayerStatType type, float bonus)
    {
        if (!_buffBonuses.ContainsKey(type))
        {
            _buffBonuses[type] = 0f;
        }

        _buffBonuses[type] += bonus;
        OnStatChanged();
    }

    protected override void OnStatChanged()
    {
        base.OnStatChanged();
        OnStatsChanged?.Invoke(this);
    }
    /// <summary>
    /// 장비 보너스 스텟 초기화 
    /// </summary>
    internal void ClearEquipmentBonuses()
    {
        foreach (PlayerStatType type in Enum.GetValues(typeof(PlayerStatType)))
        {
            _equipmentBonuses[type] = 0f;
        }
        OnStatsChanged?.Invoke(this);
    }
}
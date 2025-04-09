using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    MaxHP,
    HP,
    MaxMP,
    MP,
    Speed,
    Attack,
    DMGReduction,
    CriticalChance,
    CriticalDamage
}

public interface IBaseStat
{
    float GetStatValue(StatType type);
    void SetStatValue(StatType type, float value);
    void ModifyStat(StatType type, float amount);

}
public abstract class BaseStat : MonoBehaviour, IBaseStat
{
    protected Dictionary<StatType, float> stats = new Dictionary<StatType, float>();

    protected virtual void InitializeStats()
    {
        foreach (StatType type in Enum.GetValues(typeof(StatType)))
        {
            stats[type] = 0f;
        }
    }

    public virtual float GetStatValue(StatType type)
    {
        return stats.TryGetValue(type, out float value) ? value : 0f;
    }

    public virtual void SetStatValue(StatType type, float value)
    {
        stats[type] = value;
        OnStatChanged(type);
    }

    public virtual void ModifyStat(StatType type, float amount)
    {
        if (stats.ContainsKey(type))
        {
            stats[type] += amount;
            OnStatChanged(type);
        }
    }

    protected virtual void OnStatChanged(StatType type)
    {

    }
}

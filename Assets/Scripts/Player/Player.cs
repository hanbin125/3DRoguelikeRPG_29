using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
public interface BaseEntity
{
    void TakeDamage(int damage);
    void Healing(int heal);
    float GetCurrentHP();
    bool IsDead();
}

public class Player : MonoBehaviour, BaseEntity
{
    private PlayerStat _stats;

    private void Awake()
    {
        _stats = GetComponent<PlayerStat>();
    }

    public void TakeDamage(int damage)
    {
        float currentHP = _stats.GetStatValue(StatType.HP);
        _stats.SetStatValue(StatType.HP, currentHP - damage);
    }

    public void Healing(int heal)
    {
        float currentHP = _stats.GetStatValue(StatType.HP);
        _stats.SetStatValue(StatType.HP, currentHP + heal);
    }
    public float GetCurrentHP()
    {
        return _stats.GetStatValue(StatType.HP);
    }

    public bool IsDead()
    {
        return _stats.GetStatValue(StatType.HP) <= 0f;
    }

    //public void EquipItem(Item item)
    //{
    //    foreach (var statBonus in item.BaseEntity)
    //    {
    //        stats.AddEquipmentBonus(statBonus.type, statBonus.value);
    //    }
    //}
}

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

    private CurrencyManager currency;
    public CurrencyManager Currency => currency;

    private void Awake()
    {
        _stats = GetComponent<PlayerStat>();
        currency = GetComponent<CurrencyManager>();
        // 골드 기본값 
        currency.AddCurrency(CurrencyType.Gold, 1000);
    }

    public void TakeDamage(int damage)
    {
        float currentHP = _stats.GetStatValue(StatType.HP);
        _stats.SetStatValue(StatType.HP, currentHP - damage);
        _stats.SetBaseHP();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public interface BaseEntity
{
    void TakeDamage(float damage);
    void Healing(float heal);
    float GetCurrentHP();
    bool IsDead();
}
public class Player : MonoBehaviour, BaseEntity
{
    private PlayerStat stats;

    private void Start()
    {
        stats = GetComponent<PlayerStat>();
    }

    public void TakeDamage(float damage)
    {
        float currentHP = stats.GetStatValue(StatType.HP);
        stats.SetStatValue(StatType.HP, currentHP - damage);
    }

    public void Healing(float heal)
    {
        float currentHP = stats.GetStatValue(StatType.HP);
        stats.SetStatValue(StatType.HP, currentHP + heal);
    }
    public float GetCurrentHP()
    {
        return stats.GetStatValue(StatType.HP);
    }

    public bool IsDead()
    {
        return stats.GetStatValue(StatType.HP) <= 0f;
    }

    //public void EquipItem(Item item)
    //{
    //    foreach (var statBonus in item.BaseEntity)
    //    {
    //        stats.AddEquipmentBonus(statBonus.type, statBonus.value);
    //    }
    //}
}

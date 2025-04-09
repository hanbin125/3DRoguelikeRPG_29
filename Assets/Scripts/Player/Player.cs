using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface BaseEntity
{
    void TakeDamage(int damage);
    void Healing(int heal);
    float GetCurrentHP();
    bool IsDead();
    void SpeedUp(float speed);
}

public class Player : MonoBehaviour, BaseEntity
{
    private PlayerStat _stats;

    [SerializeField] FloatingJoystick _floatingJoystick;
    [SerializeField] Rigidbody _rb;

    private CurrencyManager currency;
    public CurrencyManager Currency => currency;

    private void Awake()
    {
        _stats = GetComponent<PlayerStat>();
        currency = GetComponent<CurrencyManager>();
        // 골드 기본값 
        currency.AddCurrency(CurrencyType.Gold, 1000);
    }
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * _floatingJoystick.Vertical + Vector3.right * _floatingJoystick.Horizontal;
        _rb.AddForce(direction * _stats.GetStatValue(PlayerStatType.Speed) * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
    public void TakeDamage(int damage)
    {
        float currentHP = _stats.GetStatValue(PlayerStatType.HP);
        _stats.SetStatValue(PlayerStatType.HP, Mathf.Max(currentHP - damage, 0));
    }

    public void Healing(int heal)
    {
        float currentHP = _stats.GetStatValue(PlayerStatType.HP);
        float maxHP = _stats.GetStatValue(PlayerStatType.MaxHP);
        _stats.SetStatValue(PlayerStatType.HP, Mathf.Min(currentHP + heal, maxHP));
        _stats.ModifyStat(PlayerStatType.HP, heal);
    }
    public float GetCurrentHP()
    {
        return _stats.GetStatValue(PlayerStatType.HP);
    }

    public bool IsDead()
    {
        return _stats.GetStatValue(PlayerStatType.HP) <= 0f;
    }

    public void SpeedUp(float speed)
    {
        //float currentSpeed = _stats.GetStatValue(StatType.Speed);
        //_stats.SetStatValue(StatType.Speed, currentSpeed + speed);
        _stats.ModifyStat(PlayerStatType.Speed, speed);
    }

    //public void EquipItem(Item item)
    //{
    //    foreach (var statBonus in item.BaseEntity)
    //    {
    //        stats.AddEquipmentBonus(statBonus.type, statBonus.value);
    //    }
    //}
}

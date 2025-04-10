using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface BaseEntity
{
    void MaxHPUp(float maxHP);
    void Healing(int heal);
    void MaxMPUp(float maxMP);
    void BaseMPUp(float currentMP);
    void SpeedUp(float speed);
    void TakeDamage(int damage);
    void Hit();
    void AttackUp(float attack);
    void DMGReductionUp(float damageReduction);
    void CriticalChanceUp(float criticalChance);
    void CriticalDamageUp(float criticalDamage);
    bool IsDead();

    //float GetCurrentHP();

}

public class Player : MonoBehaviour, BaseEntity
{
    [SerializeField] private PlayerStatData statData;

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
    private void Start()
    {
        _stats.InitBaseStat(statData);
    }
    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * _floatingJoystick.Vertical + Vector3.right * _floatingJoystick.Horizontal;

        if (direction.sqrMagnitude > 0.01f)
        {
            direction = direction.normalized;
            _rb.velocity = direction * _stats.GetStatValue(PlayerStatType.Speed);
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }

    public void MaxHPUp(float value)
    {
        _stats.ModifyStat(PlayerStatType.MaxHP, value);
        _stats.ModifyStat(PlayerStatType.HP, value);
    }
    public void Healing(int value)
    {
        float maxHP = _stats.GetStatValue(PlayerStatType.MaxHP);
        float currentHP = _stats.GetStatValue(PlayerStatType.HP);
        _stats.SetStatValue(PlayerStatType.HP, Mathf.Min(currentHP + value, maxHP));
    }
    public void MaxMPUp(float value)
    {
        _stats.ModifyStat(PlayerStatType.MaxMP, value);
        _stats.ModifyStat(PlayerStatType.MP, value);
    }
    public void BaseMPUp(float value)
    {
        float maxMP = _stats.GetStatValue(PlayerStatType.MaxMP);
        float currentMP = _stats.GetStatValue(PlayerStatType.MP);
        _stats.SetStatValue(PlayerStatType.MP, Mathf.Min(currentMP + value, maxMP));
    }
    public void SpeedUp(float speed)
    {
        //float currentSpeed = _stats.GetStatValue(PlayerStatType.Speed);
        //_stats.SetStatValue(PlayerStatType.Speed, currentSpeed + speed);
        _stats.ModifyStat(PlayerStatType.Speed, speed);
    }
    public void TakeDamage(int damage)
    {
        float currentHP = _stats.GetStatValue(PlayerStatType.HP);
        _stats.SetStatValue(PlayerStatType.HP, Mathf.Max(currentHP - damage, 0));
    }
    public void Hit()
    {
        float baseAttack = _stats.GetStatValue(PlayerStatType.Attack);
        float critChance = _stats.GetStatValue(PlayerStatType.CriticalChance);
        float critDamage = _stats.GetStatValue(PlayerStatType.CriticalDamage);

        bool isCrit = UnityEngine.Random.Range(0f, 100f) < critChance;
        float finalDamage = isCrit ? baseAttack * critDamage : baseAttack;

        Collider[] hits = Physics.OverlapSphere(transform.position, 2.5f); // 2.5f 범위 안의 적
        foreach (Collider col in hits)
        {
            BaseEntity enemy = col.GetComponent<BaseEntity>();
            if (enemy != null && !ReferenceEquals(enemy, this))
            {
                enemy.TakeDamage((int)finalDamage);
                Debug.Log($"{enemy}에게 {finalDamage} 데미지 ({(isCrit ? "CRI!" : "Normal")})");
            }
        }
    }
    public void AttackUp(float attack)
    {
        _stats.ModifyStat(PlayerStatType.Attack, attack);
    }

    public void DMGReductionUp(float damageReduction)
    {
        float currentDMGReduction = _stats.GetStatValue(PlayerStatType.DMGReduction);
        _stats.SetStatValue(PlayerStatType.DMGReduction, currentDMGReduction + damageReduction);
    }
    public void CriticalChanceUp(float criticalChance)
    {
        float currentCriticalChance = _stats.GetStatValue(PlayerStatType.CriticalChance);
        _stats.SetStatValue(PlayerStatType.CriticalChance, currentCriticalChance + criticalChance);
    }
    public void CriticalDamageUp(float criticalDamage)
    {
        float currentCriticalDamage = _stats.GetStatValue(PlayerStatType.CriticalDamage);
        _stats.SetStatValue(PlayerStatType.CriticalDamage, currentCriticalDamage + criticalDamage);
    }

    public bool IsDead()
    {
        return _stats.GetStatValue(PlayerStatType.HP) <= 0f;
    }

    //public float GetCurrentHP()
    //{
    //    return _stats.GetStatValue(PlayerStatType.HP);
    //}

    //public void EquipItem(Item item)
    //{
    //    foreach (var statBonus in item.BaseEntity)
    //    {
    //        stats.AddEquipmentBonus(statBonus.type, statBonus.value);
    //    }
    //}
}

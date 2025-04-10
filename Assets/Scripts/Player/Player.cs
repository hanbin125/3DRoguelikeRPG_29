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
}

public class Player : MonoBehaviour, BaseEntity
{
    [SerializeField] private PlayerStatData statData;
    public PlayerStat _playerStat;

    [SerializeField] FloatingJoystick _floatingJoystick;
    [SerializeField] Rigidbody _rb;
    [SerializeField] private LayerMask obstacleLayer;
    public float lastFlashTime = -Mathf.Infinity;

    private CurrencyManager currency;
    public CurrencyManager Currency => currency;

    private void Awake()
    {
        _playerStat = GetComponent<PlayerStat>();
        currency = GetComponent<CurrencyManager>();
        // 골드 기본값 
        currency.AddCurrency(CurrencyType.Gold, 1000);
    }
    private void Start()
    {
        _playerStat.InitBaseStat(statData);
    }
    public void DirectionCheck()
    {
        Vector3 direction = Vector3.forward * _floatingJoystick.Vertical + Vector3.right * _floatingJoystick.Horizontal;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 keyboardInput = Vector3.forward * v + Vector3.right * h;

        Vector3 inputDir = keyboardInput.sqrMagnitude > 0.01f ? keyboardInput : direction;
        if (inputDir.sqrMagnitude > 0.01f)
        {
            inputDir = inputDir.normalized;
            _rb.velocity = inputDir * _playerStat.GetStatValue(PlayerStatType.Speed);
        }
        else
        {
            _rb.velocity = Vector3.zero;
        }
    }
    public void FixedUpdate()
    {
        DirectionCheck();
    }

    public void MaxHPUp(float value)
    {
        _playerStat.ModifyStat(PlayerStatType.MaxHP, value);
        _playerStat.ModifyStat(PlayerStatType.HP, value);
    }
    public void Healing(int value)
    {
        float maxHP = _playerStat.GetStatValue(PlayerStatType.MaxHP);
        float currentHP = _playerStat.GetStatValue(PlayerStatType.HP);
        _playerStat.SetStatValue(PlayerStatType.HP, Mathf.Min(currentHP + value, maxHP));
    }
    public void MaxMPUp(float value)
    {
        _playerStat.ModifyStat(PlayerStatType.MaxMP, value);
        _playerStat.ModifyStat(PlayerStatType.MP, value);
    }
    public void BaseMPUp(float value)
    {
        float maxMP = _playerStat.GetStatValue(PlayerStatType.MaxMP);
        float currentMP = _playerStat.GetStatValue(PlayerStatType.MP);
        _playerStat.SetStatValue(PlayerStatType.MP, Mathf.Min(currentMP + value, maxMP));
    }
    public void SpeedUp(float speed)
    {
        //float currentSpeed = _stats.GetStatValue(PlayerStatType.Speed);
        //_stats.SetStatValue(PlayerStatType.Speed, currentSpeed + speed);
        _playerStat.ModifyStat(PlayerStatType.Speed, speed);
    }
    public void TakeDamage(int damage)
    {
        float currentHP = _playerStat.GetStatValue(PlayerStatType.HP);
        _playerStat.SetStatValue(PlayerStatType.HP, Mathf.Max(currentHP - damage, 0));
    }
    public void Hit()
    {
        float baseAttack = _playerStat.GetStatValue(PlayerStatType.Attack);
        float critChance = _playerStat.GetStatValue(PlayerStatType.CriticalChance);
        float critDamage = _playerStat.GetStatValue(PlayerStatType.CriticalDamage);

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
        _playerStat.ModifyStat(PlayerStatType.Attack, attack);
    }

    public void DMGReductionUp(float damageReduction)
    {
        float currentDMGReduction = _playerStat.GetStatValue(PlayerStatType.DMGReduction);
        _playerStat.SetStatValue(PlayerStatType.DMGReduction, currentDMGReduction + damageReduction);
    }
    public void CriticalChanceUp(float criticalChance)
    {
        float currentCriticalChance = _playerStat.GetStatValue(PlayerStatType.CriticalChance);
        _playerStat.SetStatValue(PlayerStatType.CriticalChance, currentCriticalChance + criticalChance);
    }
    public void CriticalDamageUp(float criticalDamage)
    {
        float currentCriticalDamage = _playerStat.GetStatValue(PlayerStatType.CriticalDamage);
        _playerStat.SetStatValue(PlayerStatType.CriticalDamage, currentCriticalDamage + criticalDamage);
    }

    public void Dash()
    {
        if (Time.time >= lastFlashTime + 5)
        {
            Vector3 direction = Vector3.forward * _floatingJoystick.Vertical + Vector3.right * _floatingJoystick.Horizontal;

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 keyboardInput = Vector3.forward * v + Vector3.right * h;

            Vector3 inputDir = keyboardInput.sqrMagnitude > 0.01f ? keyboardInput : direction;
            direction = direction.normalized;

            Vector3 targetPos = transform.position + direction * 5;

            if (!Physics.Raycast(transform.position, direction, 5, obstacleLayer))
            {
                _rb.MovePosition(targetPos);
                lastFlashTime = Time.time;
                Debug.Log("Flash!");
            }
        }
        else
        {
            Debug.Log("Flash blocked by obstacle.");
        }
    }

    public bool IsDead()
    {
        return _playerStat.GetStatValue(PlayerStatType.HP) <= 0f;
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

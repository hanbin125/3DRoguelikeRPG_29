using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : BaseStat<EnemyStatType>
{
    [SerializeField] private EnemyStatData statData;

    private void Awake()
    {
        InitializeStats();

        if(statData != null )
        {
            SetStatValue(EnemyStatType.MaxHP, statData.MaxHP);
            SetStatValue(EnemyStatType.Speed, statData.Speed);
            SetStatValue(EnemyStatType.Attack, statData.Attack);
            SetStatValue(EnemyStatType.Currency, statData.Currency);
            SetStatValue(EnemyStatType.AttackRange, statData.AttackRange);
            SetStatValue(EnemyStatType.AttackCooldown, statData.AttackCooldown);
        }
    }
}

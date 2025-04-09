using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyStat Stat { get; private set; }

    private void Awake()
    {
        Stat = GetComponent<EnemyStat>();
    }

    public void TakeDamage(int damage)
    {
        Stat.ModifyStat(EnemyStatType.HP, damage);
        
        if(Stat.GetStatValue(EnemyStatType.HP) <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        int drop = (int)Stat.GetStatValue(EnemyStatType.Currency);
        
        //임시코드
        Debug.Log($"사망, 골드 {drop} 드랍");
        Destroy(gameObject);
    }
}

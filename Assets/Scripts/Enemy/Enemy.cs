using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public EnemyStat Stat { get; private set; }
    public Transform PlayerTarget {  get; private set; }

    private void Awake()
    {
        Stat = GetComponent<EnemyStat>();
        CachePlayer();
    }
    /// <summary>
    /// 플레이어 찾기
    /// </summary>
    private void CachePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerTarget = player.transform;
        }
    }
    /// <summary>
    /// 플레이어를 찾지 못하면 다시 시도
    /// </summary>
    public Transform GetPlayerTarget()
    {
        if (PlayerTarget == null)
        {
            Debug.Log("플레이어를 찾지 못함");
            CachePlayer();
        }
        return PlayerTarget;
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
    }

}

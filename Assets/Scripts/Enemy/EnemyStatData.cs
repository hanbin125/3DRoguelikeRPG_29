using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatData", menuName = "Enemy/EnemyStat")]
public class EnemyStatData : ScriptableObject
{
    [Header("기본 정보")]
    [SerializeField] private int _index;
    [SerializeField] private string _enemyName;
    [SerializeField] private EnemyType _enemyType;

    [Header("스탯 정보")]
    [SerializeField] private int _maxHP;
    [SerializeField] private int _HP;
    [SerializeField] private int _attack;
    [SerializeField] private int _speed;
    [SerializeField] private int _currency;

    public int Index => _index;
    public string EnemyName => _enemyName;
    public EnemyType EnemyType => _enemyType;
    public int MaxHP => _maxHP;
    public int HP => _HP;
    public int Attack => _attack;
    public int Speed => _speed;
    public int Currency => _currency;
}

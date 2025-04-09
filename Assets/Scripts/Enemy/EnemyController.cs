using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void EnterState(EnemyController controller);
    void UpdateState(EnemyController controller);
    void ExitState(EnemyController controller);
}

public class EnemyController : MonoBehaviour
{
    private Enemy _enemy;
    private IEnemyState _currentState;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        ChageState(new EnemyIdleState());
    }

    private void Update()
    {
        _currentState?.UpdateState(this);
    }

    public void ChageState(IEnemyState newState)
    {
        _currentState?.ExitState(this);
        _currentState = newState;
        _currentState?.EnterState(this);
    }

    public float GetSpeed() => _enemy.Stat.GetStatValue(EnemyStatType.Speed);
    public float GetAttack() => _enemy.Stat.GetStatValue(EnemyStatType.Attack);
    public float GetHP() => _enemy.Stat.GetStatValue(EnemyStatType.MaxHP);
}

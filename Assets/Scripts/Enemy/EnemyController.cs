using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public Animator animator {  get; private set; }
    public NavMeshAgent agent { get; private set; }

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.speed = GetSpeed();
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
    public float GetStat(EnemyStatType type) => _enemy.Stat.GetStatValue(type);
}

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
public enum EnemyStateType
{
    Idle,
    //Patrol,
    Chase,
    Attack,
    //Hit,
    //Dead
}
public class EnemyController : MonoBehaviour
{
    private Enemy _enemy;
    private IEnemyState _currentState;

    public EnemyStateType CurrentStateType { get; private set; } = EnemyStateType.Idle;
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
        //agent.speed = GetSpeed();
        ChageState(EnemyStateType.Idle);
    }

    private void Update()
    {
        _currentState?.UpdateState(this);
    }

    public void ChageState(EnemyStateType newStateType)
    {
        if (CurrentStateType == newStateType)
            return;

        _currentState?.ExitState(this);
        _currentState = CreateStateByType(newStateType);
        _currentState?.EnterState(this);
        CurrentStateType = newStateType;
    }
    
    public float GetSpeed() => _enemy.Stat.GetStatValue(EnemyStatType.Speed);//속도를 가져옴
    public float GetAttack() => _enemy.Stat.GetStatValue(EnemyStatType.Attack);//공격력을 가져옴
    public float GetHP() => _enemy.Stat.GetStatValue(EnemyStatType.MaxHP);//최대 체력을 가져옴
    public float GetStat(EnemyStatType type) => _enemy.Stat.GetStatValue(type);//그외의 스탯을 가져옴
    public Transform GetTarget()//타겟위치 정보를 가져옴
    {
        return _enemy.GetPlayerTarget();
    }

    private IEnemyState CreateStateByType(EnemyStateType type)
    {
        return type switch
        {
            EnemyStateType.Idle => new EnemyIdleState(),
            EnemyStateType.Chase => new EnemyChaseState(),
            EnemyStateType.Attack => new EnemyAttackState(),
            _ => null
        };
    }
}

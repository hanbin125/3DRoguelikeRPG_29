using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    private Transform _target;
    private float stopDistance = 1.5f; //플레리어를 공격하기 전 멈추는 거리

    public void EnterState(EnemyController controller)
    {        
        _target = controller.GetTarget();
        if(_target == null)
        {
            Debug.Log("타겟이 없어 Idle상태로 전환");
            controller.ChageState(EnemyStateType.Idle);
        }
        stopDistance = controller.GetStat(EnemyStatType.AttackRange);
        //controller.animator?.SetBool("isMoving", true);
        Debug.Log("Chase 상태 진입");
    }

    public void ExitState(EnemyController controller)
    {
        //controller.animator?.SetBool("isMoving",false);
        Debug.Log("Chase 상태 종료");
    }

    public void UpdateState(EnemyController controller)
    {
        if(_target == null)
        {
            return;
        }

        float distance = Vector3.Distance(controller.transform.position, _target.position);

        //플레이어와 거리가 가까워지면 공격 상태로 전환
        if(distance <= stopDistance)
        {
            controller.ChageState(EnemyStateType.Attack);
            return;
        }
        float speed = controller.GetSpeed();
        Vector3 direction = (_target.position - controller.transform.position).normalized;
        controller.transform.position += direction * speed * Time.deltaTime;
    }
}

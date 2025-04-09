using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    private Transform _target;
    private float stopDistance = 1.5f; //플레리어를 공격하기 전 멈추는 거리

    public void EnterState(EnemyController controller)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            _target = player.transform;
        }
        Debug.Log("Chase 상태 진입");
    }

    public void ExitState(EnemyController controller)
    {
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
            //controller.ChageState(new EnemyAttackState());
            return;
        }

        float speed = controller.GetSpeed();
        Vector3 direction = (_target.position - controller.transform.position).normalized;
        controller.transform.position += direction * speed * Time.deltaTime;
    }
}

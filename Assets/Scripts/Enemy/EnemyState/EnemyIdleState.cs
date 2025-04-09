using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public void EnterState(EnemyController controller)
    {
        Debug.Log("Enemy : Idle 상태 진입");
    }
    public void ExitState(EnemyController controller)
    {
        Debug.Log("Enemy : Idle 상태 종료");
    }
    public void UpdateState(EnemyController controller)
    {
        //추격하기 최소거리에 도달하면 추격상태 전환
        if(Vector3.Distance(controller.transform.position, Vector3.zero) < 5f)
        {
            controller.ChageState(new EnemyChaseState());
        }
    }
    
}

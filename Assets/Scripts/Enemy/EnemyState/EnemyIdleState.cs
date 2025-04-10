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

    }
    public void UpdateState(EnemyController controller)
    {
        Transform target = controller.GetTarget();
        if (target == null)
        {
            return;
        }

        float distance = Vector3.Distance(controller.transform.position, target.position);
        //추격하기 최소거리에 도달하면 추격상태 전환
        if (Vector3.Distance(controller.transform.position, target.position) < 5f)
        {
            controller.ChageState(EnemyStateType.Chase);
        }
    }
    
}

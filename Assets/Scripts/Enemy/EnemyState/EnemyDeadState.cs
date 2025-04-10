using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : IEnemyState
{
    private float _deadDuration;//죽은 후 몇 초 뒤 제거
    private float _timer = 0f;
    public void EnterState(EnemyController controller)
    {
        _timer = 0;
    }

    public void ExitState(EnemyController controller)
    {

    }

    public void UpdateState(EnemyController controller)
    {
        _timer += Time.deltaTime;
        if (_timer >= _deadDuration)
        {
            Object.Destroy(controller.gameObject);
            Debug.Log("오브젝트 제거");
        }
    }
}

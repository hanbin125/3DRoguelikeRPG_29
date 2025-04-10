using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyAttackState : IEnemyState
{
    private Transform target;
    private float attackRange;
    private float attackCooldown;
    private float lastAttackTime;

    public void EnterState(EnemyController controller)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        
        attackRange = controller.GetStat(EnemyStatType.AttackRange);
        attackCooldown = controller.GetStat(EnemyStatType.AttackCooldown);
        lastAttackTime = Time.time;
    }

    public void ExitState(EnemyController controller)
    {

    }

    public void UpdateState(EnemyController controller)
    {
        if (target == null)
        {
            return;
        }

        float distance = Vector3.Distance(controller.transform.position, target.position);

        if (distance > attackRange)
        {
            controller.ChageState(new EnemyChaseState());
            return;
        }

        if(Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack(controller);
        }
    }

    private void PerformAttack(EnemyController controller)
    {
        float damage = controller.GetAttack();
        target.GetComponent<Player>()?.TakeDamage((int)damage);
    }
}

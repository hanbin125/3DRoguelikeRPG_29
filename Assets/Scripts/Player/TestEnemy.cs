//using UnityEngine;

//public class Enemy : MonoBehaviour, BaseEntity
//{
//    private EnemyStat _stats;

//    private void Awake()
//    {
//        _stats = GetComponent<EnemyStat>();
//    }

//    public void TakeDamage(int damage)
//    {
//        float hp = _stats.GetStatValue(EnemyStatType.HP);
//        _stats.SetStatValue(EnemyStatType.HP, hp - damage);
//        Debug.Log($"[Enemy] HP: {hp - damage}");

//        if (hp - damage <= 0)
//        {
//            Die();
//        }
//    }

//    public void Healing(int heal) { }
//    public float GetCurrentHP() => _stats.GetStatValue(EnemyStatType.HP);
//    public bool IsDead() => GetCurrentHP() <= 0f;

//    void Die()
//    {
//        Debug.Log("적 사망");
//        Destroy(gameObject);
//    }
//}

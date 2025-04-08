using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] PlayerStat _playerStat;

    public void Damage()
    {
        _playerStat.TakeDamage(50);
    }
}

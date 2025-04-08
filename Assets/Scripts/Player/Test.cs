using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Player _player;
    //private void Start()
    //{
    //    PlayerStat stat = _player.GetComponent<PlayerStat>();
    //    stat.SetBaseHP(300);
    //}

    public void Damage()
    {
        _player.TakeDamage(50);
    }
}

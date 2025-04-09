using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Player _player;
    //[SerializeField] TestPlayerUI testPlayerUI;
    //private void Start()
    //{
    //    PlayerStat stat = _player.GetComponent<PlayerStat>();
    //}

    public void Damage()
    {
        _player.TakeDamage(40);
    }
    public void Healing()
    {
        _player.Healing(40);
    }
    public void SpeedUp()
    {
        _player.SpeedUp(10);
    }
}
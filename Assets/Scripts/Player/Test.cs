using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Player _player;
    public void MaxHPUp()
    {
        _player.MaxHPUp(100);
    }
    public void Healing()
    {
        _player.Healing(40);
    }

    public void MaxMPUp()
    {
        _player.MaxMPUp(100);
    }

    public void BaseMPUp()
    {
        _player.BaseMPUp(20);
    }
    public void SpeedUp()
    {
        _player.SpeedUp(10);
    }
    public void Damage()
    {
        _player.TakeDamage(10);
    }
    public void Hit()
    {
        _player.Hit();
    }
    public void AttackUp()
    {
        _player.AttackUp(10);
    }

    public void DMGReductionUp()
    {
        _player.DMGReductionUp(0.1f);
    }
    public void CriticalChanceUp()
    {
        _player.CriticalChanceUp(5);
    }
    public void CriticalDamageUp()
    {
        _player.CriticalDamageUp(0.25f);
    }
    
    public void Dash()
    {
        _player.Dash();
    }
}
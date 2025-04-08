using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Player player;

    public void Damage()
    {
        player.TakeDamage(50);
    }
}

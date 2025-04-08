using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Player _player;
    public Player Player => _player;

    private void Start()
    {
        init();
    }

    public void init()
    {
        _player = FindObjectOfType<Player>();
    }

}


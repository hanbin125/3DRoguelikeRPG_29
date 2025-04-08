using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]private Player _player;
    public Player Player => _player;

    private void Start()
    {
        init();
    }

    public void init()
    {
        //직접 넣어주자
        //_player = FindObjectOfType<Player>();
    }

}


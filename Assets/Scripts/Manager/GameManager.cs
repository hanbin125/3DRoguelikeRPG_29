using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //싱글톤 인스턴스 

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //인스턴스가 없으면 씬에서 찾음 
                _instance = FindObjectOfType<GameManager>();
                
                //씬에도 없으면 생성 
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    [SerializeField] private InventoryManager _inventoryManager;
    //getset을 사용못하니까 접근용 만들기 
    public InventoryManager InventoryManager => _inventoryManager;

    [SerializeField] private PlayerManager _playerManager;
    public PlayerManager PlayerManager => _playerManager;

    [SerializeField] private EquipMananger _equipMananger;
    public EquipMananger EquipMananger => _equipMananger;
    private void Awake()
    {
        //인스턴스가 존재하는지 확인 + 인스턴스가 다른지 확인 
        //씬에 여러개의 instance가 존재한다면 1개만 살리기 위해서 삭제 
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
}

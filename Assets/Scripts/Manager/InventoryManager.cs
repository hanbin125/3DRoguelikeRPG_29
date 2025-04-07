using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //가방에 대한 데이터 
    public List<SlotItemData> slotItemDatas;

    [SerializeField] private UIInventory UIinventory;

    private void Start()
    {
        //인베토리 관련 초기화 
        Init();
        UIinventory.Init();
    }

    public void Init()
    {
        slotItemDatas = new List<SlotItemData>();
        //빈 공간 만들기 
        for (int i = 0; i < UIinventory.MaxSlots; i++)
        {
            slotItemDatas.Add(new SlotItemData());
        }

        //데이터 넣어주기(저장된 데이터 읽어와서)
    }
}

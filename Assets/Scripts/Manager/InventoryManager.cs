using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    //가방에 대한 데이터 
    public List<SlotItemData> slotItemDatas;
    private const int MAX_SLOTS = 12;  // UI에 의존하지 않도록 상수로 정의

    [SerializeField] private List<SlotItemData> TestItemData;

    private void Start()
    {
        //인베토리 관련 초기화 
        Init();
    }

    public void Init()
    {
        slotItemDatas = new List<SlotItemData>();
        //빈 공간 만들기 
        for (int i = 0; i < MAX_SLOTS; i++)
        {
            slotItemDatas.Add(new SlotItemData());
        }

        //데이터 넣어주기(저장된 데이터 읽어와서)
        //text용 
        AddItemList();
    }

    public void AddItemList()
    {
        for (int i = 0; i < TestItemData.Count; i++)
        {
            AddInventoryItem(TestItemData[i].item);
        }
    }

    public bool AddInventoryItem(ItemData itemData)
    {
        var emptySlot = slotItemDatas.Find(slot => slot.IsEmpty);
        if (emptySlot != null)
        {
            emptySlot.AddItem(itemData);
            return true;
        }
        return false;
    }

    // UI에게 데이터를 전달
    public void LinkUI(UIInventory uiInventory)
    {
        uiInventory.Init(this);
    }
}

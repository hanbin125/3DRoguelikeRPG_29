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

    /// <summary>
    /// test로 기본 아이템 넣는 곳 
    /// </summary>
    public void AddItemList()
    {
        for (int i = 0; i < TestItemData.Count; i++)
        {
            AddInventoryItem(TestItemData[i].item);
        }
        
    }
    /// <summary>
    /// 아이템 더해주기
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public bool AddInventoryItem(ItemData itemData)
    {
        var emptySlot = slotItemDatas.Find(slot => slot.IsEmpty);
        if (emptySlot != null)
        {
            emptySlot.AddItem(itemData);
            ArrayInventory();
            return true;
        }
        
        return false;
    }
    /// <summary>
    /// 아이템 삭제하기 
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public bool RemoveInventoryitme(ItemData itemData)
    {

        // item 이 null인데 find를 찾을려고 해서 버그
        // null 검사를 하고 id를 비교해야됨 
        var ExistItme = slotItemDatas.Find(slotitem =>
        slotitem != null &&
        slotitem.item != null &&
        slotitem.item.id == itemData.id);
        if (ExistItme != null)
        {
            ExistItme.RemoveItem(null);
            
            //remove를 해버리면 슬롯 자체가 사라지기 때문에 removeitme으로 null과 0으로 초기화 한다고 생각하면 된다.
            //slotItemDatas.Remove(ExistItme);
            //OnInventoryChanged?.Invoke();
            ArrayInventory();
            return true;
        }
        
        return false;
    }
    /// <summary>
    /// 아이템 개수 return 
    /// </summary>
    /// <returns></returns>
    public int CountingSlotItemData()
    {
        int count = 0;
        for (int i = 0; i < slotItemDatas.Count; i++)
        {
            if (!slotItemDatas[i].IsEmpty)
            {
                count++;
            }
        }
        return count;
    }
    /// <summary>
    /// inventory 정렬하기 
    /// </summary>
    public void ArrayInventory()
    { 
        //아이템 복사 
        List<ItemData> copyitems= new List<ItemData>();
        List<int> copyitemamounts = new List<int>();

        //유효한 아이템 정보만 따로 저장 
        foreach (var slot in slotItemDatas)
        {
            if (!slot.IsEmpty)
            {
                copyitems.Add(slot.item);
                copyitemamounts.Add(slot.amount);
            }
        }

        //모든 슬롯 초기화 
        for (int i = 0; i < slotItemDatas.Count; i++)
        {
            slotItemDatas[i].RemoveItem(null);
        }

        //저장해둔 아이템을 앞에서 부터 채우기 
        for (int i = 0; i < copyitems.Count; i++)
        {
            slotItemDatas[i].AddItem(copyitems[i], copyitemamounts[i]);
        }

    }
    // UI에게 데이터를 전달
    public void LinkUI(UIInventory uiInventory)
    {
        // 나머지는 데이터만 업데이트 
        uiInventory.UpdateInventory(this);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UISlot> slots;
    public int MaxSlots = 12;
    [SerializeField] private UISlot uiSlotPrefab;
    [SerializeField] private Transform SlotParent;


    public void Init()
    {
        //슬롯 데이터 
        slots =new List<UISlot>();
        //슬롯 생성 
        for (int i = 0; i < MaxSlots; i++)
        {
            UISlot slotobj=Instantiate(uiSlotPrefab, SlotParent);
            slots.Add(slotobj);
        }

        //데이터 넣어주기 
        UpdateUI();

        //아이템 클릭이벤트를 여기서 관리하기 위해서 이벤트를 연결 
        foreach (var slot in slots)
        {
            slot.OnItemClicked += HandleItemOneClick;
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i <GameManager.Instance.InventoryManager.slotItemDatas.Count)
            {
                slots[i].SetSlotData(GameManager.Instance.InventoryManager.slotItemDatas[i]);
            }
        }
    }

    /// <summary>
    /// 아이템 클릭시 발생하는 이벤트
    /// </summary>
    /// <param name="slotItemData"></param>
    private void HandleItemOneClick(SlotItemData slotItemData)
    {

    }
}

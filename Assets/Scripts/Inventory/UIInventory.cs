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
        slots = new List<UISlot>();
        //슬롯 생성 
        for (int i = 0; i < MaxSlots; i++)
        {
            UISlot slotobj = Instantiate(uiSlotPrefab, SlotParent);
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
            if (i < GameManager.Instance.InventoryManager.slotItemDatas.Count)
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
        //장착 무기 
        if (slotItemData.item.itemType == ItemType.Equipment)
        {
            if (slotItemData.item.equipType != EquipType.None && slotItemData.item.useType == UseType.None)
            {
                // 그 부위에 아이템이 장착 되어있나 없나 
                if (GameManager.Instance.EquipMananger.EqipDicionary.TryGetValue(slotItemData.item.equipType, out ItemData item))
                {
                    //그 부위에 장착 아이템의 ID를 비교 
                    if (item.id == slotItemData.item.id)
                    {
                        //장착중인아이템 
                        //장착itempopup만 띄워주면된다.

                    }
                    else
                    {
                        //같은 아이템이 아니라면 장착+select item popup까지 같이 띄워준다.

                    }
                }
            }
            else
            {
                Debug.Log("ItemType은 Equip인데 equipType이 잘못된 type을 가지고 있습니다.");
            }
        }
        else if (slotItemData.item.itemType == ItemType.Consumable)
        {
            // 사용타입 (물약)
            if (slotItemData.item.equipType == EquipType.None && slotItemData.item.useType != UseType.None)
            {
                //포션이 클릭으로 장착되어있는가 ? 아닌가 ?

            }
            else
            {
                Debug.Log("ItemType은 Consumable인데 useType이 잘못된 type을 가지고 있습니다.");
            }
        }
        else
        {
            Debug.Log("잘못된 ItemType을 가지고 있습니다.");
        }

    }
}

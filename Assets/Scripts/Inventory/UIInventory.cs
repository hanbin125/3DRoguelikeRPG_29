using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public List<UISlot> slots;
    public int MaxSlots = 12;
    [SerializeField] private UISlot uiSlotPrefab;
    [SerializeField] private Transform SlotParent;

    private InventoryManager inventoryManager;
    private bool isInitialized = false;

    // 초기화 (한 번만 실행)
    public void InitSlots()
    {
        if (!isInitialized)
        {
            slots = new List<UISlot>();
            for (int i = 0; i < MaxSlots; i++)
            {
                UISlot slotobj = Instantiate(uiSlotPrefab, SlotParent);
                slots.Add(slotobj);
                slotobj.OnItemClicked += HandleItemOneClick;
            }
            isInitialized = true;
        }
    }

    // 데이터 업데이트 (필요할 때마다 실행)
    public void UpdateInventory(InventoryManager manager)
    {
        inventoryManager = manager;
        
        // UI가 초기화되지 않았다면 초기화
        if (!isInitialized)
        {
            InitSlots();
        }
        
        // 데이터 업데이트
        UpdateUI();
    }
    /// <summary>
    /// 여기서는 데이터를 가지고 show 보여주기만 하는 곳 >> 데이터를 관리 >inventoryManager
    /// </summary>
    private void UpdateUI()
    {
        if (inventoryManager == null) return;

        for (int i = 0; i < slots.Count; i++)
        {
            if (i < inventoryManager.slotItemDatas.Count)
            {
                slots[i].SetSlotData(inventoryManager.slotItemDatas[i]);
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

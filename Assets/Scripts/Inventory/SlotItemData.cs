using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New SlotItemData")]
public class SlotItemData : ScriptableObject
{
    [Header("아이템")]
    public ItemData item;
    [Header("수량")]
    public int amount;

    public bool IsEmpty => item == null;

    public SlotItemData()
    {
        item = null;
        amount = 0;
    }

    public void AddItem(ItemData item, int count = 1)
    {
        this.item = item;
        amount += count;
    }
    public void minusItem(ItemData item, int count = 1)
    {
        this.item = item;
        amount -= count;
        if (amount <= 0)
        {
            amount = 0;
        }
    }

    public void RemoveItem(ItemData item, int count = 1)
    {
        this.item = null;
        amount = 0;
    }

}

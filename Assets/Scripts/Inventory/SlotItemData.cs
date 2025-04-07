using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItemData : MonoBehaviour
{
    public ItemData item;
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

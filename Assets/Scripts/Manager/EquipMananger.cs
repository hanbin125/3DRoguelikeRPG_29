using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipMananger : MonoBehaviour
{
    public Dictionary<EquipType, ItemData> EqipDicionary { get; private set; }

    private void Start()
    {
        init();
    }


    public void init()
    {
        EqipDicionary= new Dictionary<EquipType, ItemData>();
    }

    public void Eqipitem(ItemData itemData)
    {

    }
    public void UnEquipitem(ItemData itemData)
    {

    }
}

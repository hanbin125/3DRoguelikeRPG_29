using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipMananger : MonoBehaviour
{
    public Dictionary<EquipType, ItemData> EquipDicionary { get; private set; }

    private void Start()
    {
        init();
    }


    public void init()
    {
        EquipDicionary = new Dictionary<EquipType, ItemData>();
    }

    public void Equipitem(ItemData itemData)
    {
        // 장작된 아이템이 있는가 ?
        if (EquipDicionary.TryGetValue(itemData.equipType, out ItemData Equipeditemed))
        {
            //장착된 아이템을 제거 
            UnEquipitem(Equipeditemed);
        }

        //장착 
        EquipDicionary.Add(itemData.equipType, itemData);
        // 능력치 더해주고 

        // 장착 관련 이벤트를 발생

    }
    public void UnEquipitem(ItemData itemData)
    {
        if (EquipDicionary.TryGetValue(itemData.equipType,out ItemData eqipeditemed)
        {

        }
    }
}

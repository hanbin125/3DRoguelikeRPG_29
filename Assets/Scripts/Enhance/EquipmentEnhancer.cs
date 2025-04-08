using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentEnhancer : MonoBehaviour
{
    [Range(0f, 1f)]
    public float successRate = 0.9f;

    public bool Enhance(ItemData equipment)
    {
        if (equipment == null)           
        {
            return false;
        }

        //아이텝타입확인
        if (equipment.itemType != ItemType.Equipment)
        {
            return false;
        }
        
        //최대강화레벨도달여부
        if(equipment.enhancementLevel >= equipment.maxEnhancementLevel)
        {
            return false;
        }

        //현재강화비용
        float currentCost = equipment.enhancementCost * Mathf.Pow(equipment.enhancementCostMultiplier, equipment.enhancementLevel);
        Debug.Log($"현재 강화 비용 : {currentCost}");

        if(Random.value <= successRate)
        {
            equipment.enhancementLevel++;
            Debug.Log($"강화 성공! 강화 레벨 : {equipment.enhancementLevel}");

            return true;
        }

        else
        {
            Debug.Log("강화 실패.");

            return false;
        }
    }

}

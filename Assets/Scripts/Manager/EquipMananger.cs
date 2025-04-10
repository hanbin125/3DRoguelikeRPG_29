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
        if (EquipDicionary.TryGetValue(itemData.equipType, out ItemData eqipeditemed))
        {

        }
    }
    private void AddStats()
    {
        // 종합 stats값 
        Dictionary<PlayerStatType, float> totalconditionTypes = new Dictionary<PlayerStatType, float>();

        // 모든 장비 스탯 보너스 초기화 (이전 장비 효과 제거)
        PlayerStat playerStat = GameManager.Instance.PlayerManager.Player._playerStat;
        //playerStat.ClearEquipmentBonuses();  // 이 메서드는 PlayerStat에 추가해야 함

        // 모든 장착된 아이템에서 스탯 보너스 계산
        foreach (var item in EquipDicionary.Values)
        {

            // 아이템의 모든 옵션을 순회
            foreach (var option in item.options)
            {
                // ConditionType을 PlayerStatType으로 변환
                ConditionType conditionType = option.type;
                PlayerStatType statType = ConvertToPlayerStatType(conditionType);
                float value = option.GetValueWithLevel(item.enhancementLevel);

                totalconditionTypes[statType] += value;
            }
        }

        //playerStat.AddEquipmentBonus(totalconditionTypes);

    }

    // ConditionType을 PlayerStatType으로 변환하는 유틸리티 메서드
    private PlayerStatType ConvertToPlayerStatType(ConditionType conditionType)
    {
        switch (conditionType)
        {
            case ConditionType.Power:
                return PlayerStatType.Attack;
            case ConditionType.Health:
                return PlayerStatType.HP;
            case ConditionType.Mana:
                return PlayerStatType.MP;
            case ConditionType.Speed:
                return PlayerStatType.Speed;
            case ConditionType.reduction:
                return PlayerStatType.DMGReduction;
            case ConditionType.CriticalChance:
                return PlayerStatType.CriticalChance;
            case ConditionType.CriticalDamage:
                return PlayerStatType.CriticalDamage;
            // 기타 조건 추가
            default:
                Debug.LogWarning($"Unsupported ConditionType: {conditionType}");
                return PlayerStatType.MaxHP;  // 기본값 반환
        }
    }
}

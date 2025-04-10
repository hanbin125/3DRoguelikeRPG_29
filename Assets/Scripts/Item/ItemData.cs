using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 기본 타입
public enum ItemType
{
    Equipment,    // 장비
    Consumable,   // 소모품
    Quest,        // 퀘스트 아이템
    Material      // 재료
}

public enum EquipType
{
    None,         // 장비 아님
    Weapon,
    Coat,
    Shoes,
    Glove
}

public enum UseType
{
    None,         // 소모품 아님
    HpPotion,
    MpPotion,
    StatBoost,    // 일시적 스텟 증가
    Buff          // 지속 버프
}

public enum ConditionType
{
    Power,
    Mana,
    Health,
    Speed,
    reduction,
    CriticalChance,
    CriticalDamage
}

[Serializable]
public class ItemOption
{
    public ConditionType type;
    public float baseValue;
    public float increasePerLevel;

    public float GetValueWithLevel(int level)
    {
        return baseValue + (increasePerLevel * level);
    }
}

[Serializable]
public class ConsumableEffect
{
    public ConditionType type;
    public float value;           // 회복량 또는 효과 수치
    public float duration;        // 지속 시간 (0이면 즉시 효과)
}

public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public int id;
    public ItemType itemType;     // 아이템 기본 타입
    public EquipType equipType;
    public UseType useType;
    public string itemName;
    public string description;

    [Header("장비 옵션")]
    public int enhancementLevel = 0;
    public int maxEnhancementLevel = 10;
    public List<ItemOption> options = new List<ItemOption>();

    [Header("소모품 효과")]
    public List<ConsumableEffect> consumableEffects = new List<ConsumableEffect>();
    public int maxStack = 99;     // 최대 중첩 개수

    [Header("경제")]
    public int gold;
    public int enhancementCost = 100;
    public float enhancementCostMultiplier = 1.5f;

    [Header("시각 효과")]
    public Sprite Icon;
    public GameObject itemObj;

    /// <summary>
    /// 장비 옵션 값 얻기
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public float GetOptionValue(ConditionType type)
    {
        foreach (ItemOption option in options)
        {
            if (option.type == type)
            {
                return option.GetValueWithLevel(enhancementLevel);
            }
        }
        return 0f;
    }

   
    // 아이템 사용 (소모품용)
    public List<ConsumableEffect> Use()
    {
        if (itemType == ItemType.Consumable)
        {
            return consumableEffects;
        }
        return null;
    }

    // 아이템 초기화 (새 아이템 생성 시)
    public void Initialize()
    {
        // 타입에 따라 기본값 설정
        if (itemType == ItemType.Equipment)
        {
            if (useType != UseType.None)
                useType = UseType.None;
        }
        else if (itemType == ItemType.Consumable)
        {
            if (equipType != EquipType.None)
                equipType = EquipType.None;
        }
    }
}

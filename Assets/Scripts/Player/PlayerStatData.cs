using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewPlayerStatData", menuName = "Game/Stats/Player Stat Data")]
public class PlayerStatData : ScriptableObject
{
    [System.Serializable]
    public class StatValue
    {
        public PlayerStatType statType;
        public float baseValue;
    }

    public List<StatValue> stats = new List<StatValue>();

    // 특정 스탯의 기본값 가져오기
    public float GetBaseValue(PlayerStatType statType)
    {
        foreach (var stat in stats)
        {
            if (stat.statType == statType)
                return stat.baseValue;
        }
        return 0f;
    }
}
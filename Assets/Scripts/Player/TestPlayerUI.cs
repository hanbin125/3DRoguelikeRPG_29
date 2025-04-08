using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestPlayerUI : MonoBehaviour
{
    PlayerStat playerStat;

    [SerializeField] TextMeshProUGUI _hpText;
    [SerializeField] TextMeshProUGUI _mpText;
    [SerializeField] TextMeshProUGUI _attackText;
    [SerializeField] TextMeshProUGUI _speedText;
    [SerializeField] TextMeshProUGUI _dmgReductionText;
    [SerializeField] TextMeshProUGUI _criticalChanceText;
    [SerializeField] TextMeshProUGUI _criticalDamageText;

    private void Start()
    {
        SetBaseHP();
    }
    public void SetBaseHP()
    {
        _hpText.text = playerStat.GetStatValue(StatType.HP).ToString("F0");
    }
}

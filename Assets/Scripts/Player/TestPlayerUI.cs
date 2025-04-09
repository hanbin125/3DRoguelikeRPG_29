using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestPlayerUI : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] PlayerStat _playerStat;

    [SerializeField] TextMeshProUGUI _hpText;
    [SerializeField] TextMeshProUGUI _mpText;
    [SerializeField] TextMeshProUGUI _attackText;
    [SerializeField] TextMeshProUGUI _speedText;
    [SerializeField] TextMeshProUGUI _dmgReductionText;
    [SerializeField] TextMeshProUGUI _criticalChanceText;
    [SerializeField] TextMeshProUGUI _criticalDamageText;

    private void Start()
    {
        _playerStat.OnHPChanged += UpdateHP;
        _playerStat.OnSpeedChanged += UpdateSpeed;
        UpdateHP(_playerStat.GetStatValue(StatType.HP));
        UpdateSpeed(_playerStat.GetStatValue(StatType.Speed));
    }
    public void UpdateHP(float newHP)
    {
        _hpText.text = newHP.ToString("F0");
    }
    public void UpdateSpeed(float newSpeed)
    {
        _speedText.text = newSpeed.ToString("F0");
    }
}

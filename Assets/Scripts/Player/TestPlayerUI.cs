using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestPlayerUI : MonoBehaviour
{
    [SerializeField] Player _player;
    [SerializeField] PlayerStat _playerStat;

    [SerializeField] TextMeshProUGUI _maxHPText;
    [SerializeField] TextMeshProUGUI _hpText;
    [SerializeField] TextMeshProUGUI _maxMPText;
    [SerializeField] TextMeshProUGUI _mpText;
    [SerializeField] TextMeshProUGUI _speedText;
    [SerializeField] TextMeshProUGUI _attackText;
    [SerializeField] TextMeshProUGUI _dmgReductionText;
    [SerializeField] TextMeshProUGUI _criticalChanceText;
    [SerializeField] TextMeshProUGUI _criticalDamageText;

    private void Start()
    {
        _playerStat.OnMaxHPChanged += UpdateMaxHP;
        _playerStat.OnHPChanged += UpdateHP;
        _playerStat.OnMaxMPChanged += UpdateMaxMP;
        _playerStat.OnMPChanged += UpdateMP;
        _playerStat.OnSpeedChanged += UpdateSpeed;
        _playerStat.OnAttackChanged += UpdateAttack;
        _playerStat.OnDMGReductionChanged += UpdateDMGReduction;
        _playerStat.OnCriticalChanceChanged += UpdateCriticalChance;
        _playerStat.OnCriticalDamageChanged += UpdateCriticalDamage;

        UpdateMaxHP(_playerStat.GetStatValue(PlayerStatType.MaxHP));
        UpdateHP(_playerStat.GetStatValue(PlayerStatType.HP));
        UpdateMaxMP(_playerStat.GetStatValue(PlayerStatType.MaxMP));
        UpdateMP(_playerStat.GetStatValue(PlayerStatType.MP));
        UpdateSpeed(_playerStat.GetStatValue(PlayerStatType.Speed));
        UpdateAttack(_playerStat.GetStatValue(PlayerStatType.Attack));
        UpdateDMGReduction(_playerStat.GetStatValue(PlayerStatType.DMGReduction));
        UpdateCriticalChance(_playerStat.GetStatValue(PlayerStatType.CriticalChance));
        UpdateCriticalDamage(_playerStat.GetStatValue(PlayerStatType.CriticalDamage));
    }
    public void UpdateMaxHP(float newMaxHP)
    {
        //_maxHPText.text = newMaxHP.ToString("F0");
    }
    public void UpdateHP(float newHP)
    {
        _hpText.text = newHP.ToString("F0");
    }
    public void UpdateMaxMP(float newMaxMP)
    {
        //_maxMPText.text = newMaxMP.ToString("F0");
    }
    public void UpdateMP(float newMP)
    {
        //_mpText.text = newMP.ToString("F0");
    }
    public void UpdateSpeed(float newSpeed)
    {
        _speedText.text = newSpeed.ToString("F0");
    }
    public void UpdateAttack(float newAttack)
    {
        //_attackText.text = newAttack.ToString("F0");
    }
    public void UpdateDMGReduction(float newDMGReduction)
    {
        //_dmgReductionText.text = newDMGReduction.ToString("F0");
    }
    public void UpdateCriticalChance(float newCriticalChance)
    {
        //_criticalChanceText.text = newCriticalChance.ToString("F0");
    }
    public void UpdateCriticalDamage(float newCriticalDamage)
    {
        //_criticalDamageText.text = newCriticalDamage.ToString("F0");
    }

}

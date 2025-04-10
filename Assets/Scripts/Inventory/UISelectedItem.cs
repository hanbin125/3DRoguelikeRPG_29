using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISelectedItem : PopupUI
{
    [SerializeField] private Image itemimage;
    [SerializeField] private TextMeshProUGUI power;
    [SerializeField] private TextMeshProUGUI mana;
    [SerializeField] private TextMeshProUGUI health;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI Reduction;
    [SerializeField] private TextMeshProUGUI CriticalChance;
    [SerializeField] private TextMeshProUGUI CriticalDamage;
    [SerializeField] private Button btn_equip;
    [SerializeField] private Button btn_Release;

    private ItemData currentItem;

    protected override void Awake()
    {
        base.Awake();
        UIManager.Instance.RegisterUI(this);
        btn_equip.onClick.AddListener(OnEquipButtonClicked);
        btn_Release.onClick.AddListener(OnReleaseButtonClicked);
    }

    public void Initialize()
    {
        // 초기화 로직
    }

    public void Show(ItemData item)
    {
        if (item == null) return;

        currentItem = item;
        base.Show();
        UpdateUI();
    }

    public override void Hide()
    {
        currentItem = null;
        base.Hide();
    }

    private void UpdateUI()
    {
        if (currentItem == null) return;

        // 아이템 이미지 설정
        if (currentItem.Icon != null)
            itemimage.sprite = currentItem.Icon;

        // 능력치 표시
        power.text = $"공격력: {currentItem.GetOptionValue(ConditionType.Power)}";
        mana.text = $"마나: {currentItem.GetOptionValue(ConditionType.Mana)}";
        health.text = $"체력: {currentItem.GetOptionValue(ConditionType.Health)}";
        speed.text = $"속도: {currentItem.GetOptionValue(ConditionType.Speed)}";
        Reduction.text = $"피해감소: {currentItem.GetOptionValue(ConditionType.reduction)}";
        CriticalChance.text = $"치명타확률: {currentItem.GetOptionValue(ConditionType.CriticalChance)}";
        CriticalDamage.text = $"치명타피해: {currentItem.GetOptionValue(ConditionType.CriticalDamage)}";
    }

    private void OnEquipButtonClicked()
    {
        if (currentItem != null)
        {
            // GameManager.Instance 확인
            if (GameManager.Instance == null)
            {
                Debug.LogError("GameManager.Instance is null!");
                return;
            }
            // EquipMananger 확인
            if (GameManager.Instance.EquipMananger == null)
            {
                Debug.LogError("EquipMananger is null!");
                return;
            }
            // 직접 장착 로직 구현 
            GameManager.Instance.EquipMananger.Equipitem(currentItem);
            // 팝업 닫기
            UIManager.Instance.ClosePopupUI(this);
            Debug.Log($"{currentItem.itemName} 장착");
        }
        else
        {
            Debug.Log("선택된 아이템의 정보가 없습니다.");
        }
    }

    private void OnReleaseButtonClicked()
    {
        if (currentItem != null)
        {
            // 직접 해제 로직 구현
            GameManager.Instance.EquipMananger.UnEquipitem(currentItem);
            Debug.Log($"{currentItem.itemName} 해제");
            // 팝업 닫기
            UIManager.Instance.ClosePopupUI(this);
        }
    }

    protected override void Clear()
    {
        base.Clear();
        currentItem = null;
    }
}

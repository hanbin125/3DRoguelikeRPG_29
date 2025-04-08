using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedItem : PopupUI
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
    private System.Action<ItemData> onEquipCallback;
    private System.Action<ItemData> onReleaseCallback;

    protected override void Awake()
    {
        base.Awake();
        btn_equip.onClick.AddListener(OnEquipButtonClicked);
        btn_Release.onClick.AddListener(OnReleaseButtonClicked);
    }

    public void Initialize(System.Action<ItemData> equipCallback, System.Action<ItemData> releaseCallback)
    {
        //초기화에서 이벤트를 받아와서 연결시켜주는 것도 좋은 방식인것같다 .
        //어차피 장착 해제에대한 로직은 여기서 안할것이기때문에 
        // 아 그니까 이게 세부 로직도 여기서 구현을 하는게 아니라 popupinventory까지 올라서 한다? 맞지 그게 
        onEquipCallback = equipCallback;
        onReleaseCallback = releaseCallback;
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
            onEquipCallback?.Invoke(currentItem);
        }
    }

    private void OnReleaseButtonClicked()
    {
        if (currentItem != null)
        {
            onReleaseCallback?.Invoke(currentItem);
        }
    }

    protected override void Clear()
    {
        base.Clear();
        currentItem = null;
    }
}

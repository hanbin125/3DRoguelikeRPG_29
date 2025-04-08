using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInventory : PopupUI
{
    [SerializeField] private TextMeshProUGUI inventoryVolume;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private Image posionUI;
    [SerializeField] private Image slotEquipWeapon;
    [SerializeField] private Image slotEquipCoat;
    [SerializeField] private Image slotEquipShoes;
    [SerializeField] private Image slotEquipGlove;
    [SerializeField] private EquipItem equipedItem;
    [SerializeField] private SelectedItem selectedItem;

    protected override void Init()
    {
        base.Init();
        selectedItem.Initialize(OnEquipItem, OnReleaseItem);
    }

    public void OnItemSelected(ItemData item)
    {
        if (item == null) return;

        selectedItem.Show(item);

        if (item.equipType != EquipType.None)
        {
            equipedItem.Show(item);
        }
    }

    private void OnEquipItem(ItemData item)
    {
        // 아이템 장착 로직
        Debug.Log($"{item.itemName} 장착");
    }

    private void OnReleaseItem(ItemData item)
    {
        // 아이템 해제 로직
        Debug.Log($"{item.itemName} 해제");
    }

    protected override void Clear()
    {
        base.Clear();
        selectedItem.Hide();
        equipedItem.Hide();
    }
} 
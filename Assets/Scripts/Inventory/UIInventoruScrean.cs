using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoruScrean : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inventoryVlume;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Gold;
    [SerializeField] private Image Posion_UI;
    [SerializeField] private Image Slot_Equip_Weapon;
    [SerializeField] private Image Slot_Equip_Coat;
    [SerializeField] private Image Slot_Equip_Shoes;
    [SerializeField] private Image Slot_Equip_Glove;
    [SerializeField] private GameObject EquipedItem;
    [SerializeField] private GameObject SelectedItem;


    public void EquipedItemPopup()
    {
        //켜져있으면 꺼주고 꺼져있으면 켜주고 ? 
        EquipedItem.gameObject.SetActive(!EquipedItem.gameObject.activeInHierarchy);

    }




}

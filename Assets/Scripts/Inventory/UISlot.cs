using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Button SlotButton;
    [SerializeField] private TextMeshProUGUI text_equip;
    [SerializeField] private TextMeshProUGUI amount;

    //슬롯별로 데이터를 가지고 있어야한다.
    //핸드폰 게임이니까 클릭을 했을때 ui를 1개 더 만들어서 띄우자

    public SlotItemData currentItemData;

    //아이템 클릭 이벤트 
    public Action<SlotItemData> OnItemClicked;

    private void Start()
    {
        text_equip.gameObject.SetActive(false);
        amount.gameObject.SetActive(false);
        Init();
    }


    public void Init()
    {
        SlotButton.onClick.AddListener(onSlotClick);

    }

    private void onSlotClick()
    {
        if (currentItemData ==null ||currentItemData.IsEmpty)
        {
            Debug.Log("빈 슬롯 클릭");
            return;
        }

        OnItemClicked?.Invoke(currentItemData);

    }

    public void SetSlotData(SlotItemData slotData)
    {
        currentItemData = slotData;

        // 존재한다면
        if (!slotData.IsEmpty)
        {
            iconImage.sprite = slotData.item.Icon;
            iconImage.enabled= true;
            
        }
        else
        {
            iconImage.sprite = null;
            //iconImage.enabled = false;
            //text_equip.gameObject.SetActive(false);
            //amount.gameObject.SetActive(false);
        }
    }

}

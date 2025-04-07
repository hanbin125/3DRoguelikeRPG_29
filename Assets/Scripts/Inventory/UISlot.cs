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
}

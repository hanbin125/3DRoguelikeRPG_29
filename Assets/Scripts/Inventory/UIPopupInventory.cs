using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPopupInventory : PopupUI
{
    [SerializeField] private TextMeshProUGUI inventoryVolume;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private Image posionUI;
    [SerializeField] private Image slotEquipWeapon;
    [SerializeField] private Image slotEquipCoat;
    [SerializeField] private Image slotEquipShoes;
    [SerializeField] private Image slotEquipGlove;
    [SerializeField] private UIEquipedItem equipedItem;
    [SerializeField] private UISelectedItem selectedItem;
    
    // 탭 버튼들
    [SerializeField] private Button equipmentTabButton;
    [SerializeField] private Button consumableTabButton;
    [SerializeField] private Button materialTabButton;

    //각 class 요소들 
    private UIInventory uIInventory;

    protected virtual void Awake()
    {
        // UI 매니저에 등록
        UIManager.Instance.RegisterUI(this);
        
        uIInventory =GetComponentInChildren<UIInventory>();
        // 탭 버튼 이벤트 등록
        if (equipmentTabButton != null)
            equipmentTabButton.onClick.AddListener(() => OnTabChanged(ItemType.Equipment));
            
        if (consumableTabButton != null)
            consumableTabButton.onClick.AddListener(() => OnTabChanged(ItemType.Consumable));
            
        if (materialTabButton != null)
            materialTabButton.onClick.AddListener(() => OnTabChanged(ItemType.Material));
    }

    protected override void OnEnable()
    {
        //초기화 작업
        base.OnEnable();

        //InventoryManager 와 UIInventory 연결 
        GameManager.Instance.InventoryManager.LinkUI(uIInventory);

        // 인벤토리가 활성화될 때마다 최신 정보로 업데이트
        RefreshInventory();
        
        // 기본 탭 선택
        OnTabChanged(ItemType.Equipment);
    }

    protected override void Init()
    {
        base.Init();
        selectedItem.Initialize(OnEquipItem, OnReleaseItem);
    }

    public void OnItemSelected(ItemData item)
    {
        if (item == null) return;

        selectedItem.Show(item);

        //장착된 아이템이 아니라면 show 보여줘야지 
        if (!GameManager.Instance.EquipMananger.EqipDicionary.ContainsKey(item.equipType))
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
    
    // 탭 변경 처리
    private void OnTabChanged(ItemType type)
    {
        // 선택된 탭에 따라 아이템 필터링 및 UI 업데이트
        Debug.Log($"탭 변경: {type}");
        
        // 버튼 시각적 상태 업데이트
        equipmentTabButton.interactable = (type != ItemType.Equipment);
        consumableTabButton.interactable = (type != ItemType.Consumable);
        materialTabButton.interactable = (type != ItemType.Material);
        
        // 아이템 필터링 및 표시
        // FilterItems(type);
    }
    
    // 인벤토리 새로고침
    private void RefreshInventory()
    {
        // 플레이어 정보 업데이트
        playerName.text = "플레이어1";
        gold.text = "1000 G";
        inventoryVolume.text = "10/50";
        
        // 장비 슬롯 업데이트
        // UpdateEquipmentSlots();
    }

    // 오버라이드: 닫기 버튼 클릭 처리
    protected override void OnCloseButtonClick()
    {
        // 특별한 처리가 필요한 경우 여기에 추가
        Debug.Log("인벤토리 닫힘");
        
        // 부모 클래스의 메서드 호출
        base.OnCloseButtonClick();
    }

    protected override void Clear()
    {
        base.Clear();
        selectedItem.Hide();
        equipedItem.Hide();
    }
} 
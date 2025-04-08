using UnityEngine;
using UnityEngine.UI;

public abstract class PopupUI : BaseUI
{
    protected Canvas popupCanvas;
    [SerializeField] protected Button closeButton;  // 팝업 닫기 버튼

    protected virtual void Init()
    {

    }

    protected virtual void Awake()
    {
        popupCanvas = GetComponent<Canvas>();
        if (popupCanvas != null)
        {
            popupCanvas.sortingOrder = 100; // 팝업은 일반 UI보다 위에 표시
        }
        
        // 닫기 버튼이 있으면 이벤트 등록
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(OnCloseButtonClick);
        }
    }
    
    protected virtual void OnEnable()
    {
        // 활성화될 때마다 초기화 필요한 작업
    }

    public override void Show()
    {
        base.Show();
        transform.SetAsLastSibling(); // 현재 팝업을 최상단으로
    }

    public virtual void Close()
    {
        Hide();
        Clear();
    }
    
    // 닫기 버튼 클릭 이벤트
    protected virtual void OnCloseButtonClick()
    {
        // 자신을 닫음
        UIManager.Instance.ClosePopupUI(this);
    }

} 
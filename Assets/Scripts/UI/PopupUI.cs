using UnityEngine;

public abstract class PopupUI : BaseUI
{
    protected Canvas popupCanvas;

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

} 
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("UIManager");
                    _instance = go.AddComponent<UIManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return _instance;
        }
    }

    private Stack<PopupUI> popupStack = new Stack<PopupUI>();
    private Dictionary<string, BaseUI> uiList = new Dictionary<string, BaseUI>();
    private List<PopupUI> activePopups = new List<PopupUI>(); // 활성화된 모든 팝업 추적

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public T ShowUI<T>() where T : BaseUI
    {
        string key = typeof(T).Name;
        
        if (uiList.TryGetValue(key, out BaseUI ui))
        {
            ui.Show();
            return ui as T;
            //return (T)ui 형변환 대신 이렇게 형변환을하면  예외를 발생  / as를 사용시 null를반환 지금 ui는 
            //실제로 popupinventory형식이지만 baseui로 out에서 꺼내지니까 꺼내서 명확하게 형변환을 시켜주는거지 
        }
        
        return null;
    }

    public T ShowPopupUI<T>() where T : PopupUI
    {
        T popup = ShowUI<T>();
        if (popup != null)
        {
            popupStack.Push(popup);
            if (!activePopups.Contains(popup))
            {
                activePopups.Add(popup);
            }
        }
        return popup;
    }

    // 스택 구조로 가장 최근 팝업 닫기 (ESC 키 등에 사용)
    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        PopupUI popup = popupStack.Pop();
        popup.Close();
        activePopups.Remove(popup);
    }

    // 특정 팝업을 직접 지정해서 닫기 (버튼 클릭 등에 사용)
    public void ClosePopupUI(PopupUI popup)
    {
        if (popup == null)
            return;

        // 스택에서도 제거 (복제본 생성 후 해당 팝업만 제외하고 다시 스택에 넣기)
        Stack<PopupUI> tempStack = new Stack<PopupUI>();
        while (popupStack.Count > 0)
        {
            PopupUI p = popupStack.Pop();
            if (p != popup)
            {
                tempStack.Push(p);
            }
        }
        
        // 다시 원래 스택으로 복원
        while (tempStack.Count > 0)
        {
            popupStack.Push(tempStack.Pop());
        }
        
        // 팝업 닫기
        popup.Close();
        activePopups.Remove(popup);
    }

    // 특정 타입의 팝업 닫기 (타입으로 지정)
    public void ClosePopupUI<T>() where T : PopupUI
    {
        string key = typeof(T).Name;
        
        if (uiList.TryGetValue(key, out BaseUI ui))
        {
            PopupUI popup = ui as PopupUI;
            if (popup != null)
            {
                ClosePopupUI(popup);
            }
        }
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
        
        // 만약을 위해 activePopups도 처리
        foreach (var popup in activePopups.ToArray())
        {
            popup.Close();
        }
        activePopups.Clear();
    }

    public void RegisterUI(BaseUI ui)
    {
        string key = ui.GetType().Name;
        if (!uiList.ContainsKey(key)) // 등록되지 않은 경우에만
            uiList.Add(key, ui);
    }

    private void Update()
    {
        // ESC로 팝업 닫기 (안드로이드의 경우 뒤로가기 버튼)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopupUI();
        }
    }
} 
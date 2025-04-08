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
        }
        return popup;
    }

    public void ClosePopupUI()
    {
        if (popupStack.Count == 0)
            return;

        PopupUI popup = popupStack.Pop();
        popup.Close();
    }

    public void CloseAllPopupUI()
    {
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    public void RegisterUI(BaseUI ui)
    {
        string key = ui.GetType().Name;
        if (!uiList.ContainsKey(key)) // 등록되지 않은 경우에만
            uiList.Add(key, ui);
    }

    private void Update()
    {
        // ESC로 팝업 닫기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePopupUI();
        }
    }
} 
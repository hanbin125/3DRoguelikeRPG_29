using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Dictionary<string, BaseUI> uiList = new Dictionary<string, BaseUI>();
    private Stack<PopupUI> popupStack = new Stack<PopupUI>(); // 원래라면 컴퓨터로 esc누르면 위에 켜져있는 팝업순서대로 삭제를해서 stack이 좋았음 
    private List<PopupUI> activePopups = new List<PopupUI>(); // 활성화된 모든 팝업 추적 ->모바일때문에 생겼음 

    private Canvas mainCanvas; // 메인 Canvas

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        // 메인 Canvas 생성
        CreateMainCanvas();
    }

    private void CreateMainCanvas()
    {
        GameObject canvasObj = new GameObject("MainCanvas");
        canvasObj.transform.SetParent(transform);
        
        mainCanvas = canvasObj.AddComponent<Canvas>();
        mainCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        // Canvas Scaler 추가
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        // Graphic Raycaster 추가
        canvasObj.AddComponent<GraphicRaycaster>();
    }

    private void Start()
    {
        // 씬에 있는 모든 UI 찾기 (비활성화된 오브젝트도 포함)
        var allUIs = FindObjectsOfType<BaseUI>(true);
        foreach (var ui in allUIs)
        {
            RegisterUI(ui);
        }
    }

    public T ShowUI<T>() where T : BaseUI
    {
        string key = typeof(T).Name;
        
        if (uiList.TryGetValue(key, out BaseUI ui))
        {
            ui.Show();
            return ui as T;
        }
        
        // Resources에서 프리팹 로드
        GameObject prefab = Resources.Load<GameObject>($"UI/{key}");
        if (prefab != null)
        {
            // 프리팹 인스턴스화 (메인 Canvas의 자식으로)
            GameObject go = Instantiate(prefab, mainCanvas.transform);
            T newUI = go.GetComponent<T>();
            
            if (newUI != null)
            {
                RegisterUI(newUI);
                return newUI;
            }
            else
            {
                Debug.LogError($"{key} 프리팹에 BaseUI 컴포넌트가 없습니다.");
                Destroy(go);
            }
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            ShowPopupUI<UIPopupInventory>();
        }
    }

    public int GetActivePopupCount()
    {
        return activePopups.Count;
    }
} 
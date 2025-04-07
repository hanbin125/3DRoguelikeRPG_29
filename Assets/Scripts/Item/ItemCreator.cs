using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public class ItemCreator : EditorWindow
{
    private ItemData itemData;
    private string itemName = "New Item";
    private ItemType itemType = ItemType.Equipment;
    private EquipType equipType = EquipType.Weapon;
    private UseType useType = UseType.None;
    private int id = 0;
    private string description = "";
    private int gold = 100;
    private int maxEnhancementLevel = 10;
    private int enhancementCost = 100;
    private float enhancementCostMultiplier = 1.5f;
    private int maxStack = 99;
    private Sprite icon;
    private GameObject itemObj;

    private Vector2 scrollPosition;
    private bool showOptions = true;
    private bool showConsumableEffects = true;
    
    // 장비 옵션 관련
    private ConditionType newOptionType = ConditionType.Power;
    private float newOptionBaseValue = 10f;
    private float newOptionIncreasePerLevel = 1f;
    
    // 소모품 효과 관련
    private ConditionType newEffectType = ConditionType.Health;
    private float newEffectValue = 50f;
    private float newEffectDuration = 0f;

    [MenuItem("Tools/Item Creator")]
    public static void ShowWindow()
    {
        GetWindow<ItemCreator>("아이템 생성기");
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.LabelField("아이템 기본 정보", EditorStyles.boldLabel);
        id = EditorGUILayout.IntField("아이템 ID", id);
        itemName = EditorGUILayout.TextField("아이템 이름", itemName);
        description = EditorGUILayout.TextField("설명", description);
        
        // 아이템 타입 선택
        EditorGUI.BeginChangeCheck();
        itemType = (ItemType)EditorGUILayout.EnumPopup("아이템 타입", itemType);
        if (EditorGUI.EndChangeCheck())
        {
            // 아이템 타입 변경 시 관련 설정 초기화
            if (itemType == ItemType.Equipment)
            {
                equipType = EquipType.Weapon;
                useType = UseType.None;
            }
            else if (itemType == ItemType.Consumable)
            {
                equipType = EquipType.None;
                useType = UseType.HpPotion;
            }
            else
            {
                equipType = EquipType.None;
                useType = UseType.None;
            }
        }
        
        // 아이템 타입에 따라 다른 옵션 표시
        if (itemType == ItemType.Equipment)
        {
            equipType = (EquipType)EditorGUILayout.EnumPopup("장비 타입", equipType);
        }
        else if (itemType == ItemType.Consumable)
        {
            useType = (UseType)EditorGUILayout.EnumPopup("사용 타입", useType);
            maxStack = EditorGUILayout.IntField("최대 중첩 개수", maxStack);
        }
        
        gold = EditorGUILayout.IntField("가격", gold);
        icon = (Sprite)EditorGUILayout.ObjectField("아이콘", icon, typeof(Sprite), false);
        itemObj = (GameObject)EditorGUILayout.ObjectField("아이템 오브젝트", itemObj, typeof(GameObject), false);
        EditorGUILayout.EndVertical();

        // 장비 아이템인 경우에만 강화 설정 표시
        if (itemType == ItemType.Equipment)
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("강화 설정", EditorStyles.boldLabel);
            maxEnhancementLevel = EditorGUILayout.IntField("최대 강화 레벨", maxEnhancementLevel);
            enhancementCost = EditorGUILayout.IntField("기본 강화 비용", enhancementCost);
            enhancementCostMultiplier = EditorGUILayout.FloatField("강화 비용 증가율", enhancementCostMultiplier);
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space(10);

        // 아이템 타입에 따라 옵션/효과 설정 표시
        if (itemType == ItemType.Equipment)
        {
            // 장비 옵션 설정
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            showOptions = EditorGUILayout.Foldout(showOptions, "장비 옵션");
            
            if (showOptions)
            {
                if (itemData != null)
                {
                    EditorGUI.indentLevel++;
                    
                    for (int i = 0; i < itemData.options.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        
                        itemData.options[i].type = (ConditionType)EditorGUILayout.EnumPopup("옵션 유형", itemData.options[i].type);
                        itemData.options[i].baseValue = EditorGUILayout.FloatField("기본값", itemData.options[i].baseValue);
                        itemData.options[i].increasePerLevel = EditorGUILayout.FloatField("레벨당 증가량", itemData.options[i].increasePerLevel);
                        
                        if (GUILayout.Button("제거", GUILayout.Width(60)))
                        {
                            itemData.options.RemoveAt(i);
                            GUIUtility.ExitGUI();
                        }
                        
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUI.indentLevel--;
                    
                    EditorGUILayout.Space(5);
                    EditorGUILayout.LabelField("새 옵션 추가", EditorStyles.boldLabel);
                    EditorGUILayout.BeginHorizontal();
                    newOptionType = (ConditionType)EditorGUILayout.EnumPopup("타입", newOptionType);
                    newOptionBaseValue = EditorGUILayout.FloatField("기본값", newOptionBaseValue);
                    newOptionIncreasePerLevel = EditorGUILayout.FloatField("증가량", newOptionIncreasePerLevel);
                    EditorGUILayout.EndHorizontal();
                    
                    if (GUILayout.Button("옵션 추가"))
                    {
                        ItemOption newOption = new ItemOption
                        {
                            type = newOptionType,
                            baseValue = newOptionBaseValue,
                            increasePerLevel = newOptionIncreasePerLevel
                        };
                        
                        itemData.options.Add(newOption);
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("아이템을 먼저 생성해주세요.", MessageType.Info);
                }
            }
            EditorGUILayout.EndVertical();
        }
        else if (itemType == ItemType.Consumable)
        {
            // 소모품 효과 설정
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            showConsumableEffects = EditorGUILayout.Foldout(showConsumableEffects, "소모품 효과");
            
            if (showConsumableEffects)
            {
                if (itemData != null)
                {
                    EditorGUI.indentLevel++;
                    
                    for (int i = 0; i < itemData.consumableEffects.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        
                        itemData.consumableEffects[i].type = (ConditionType)EditorGUILayout.EnumPopup("효과 유형", itemData.consumableEffects[i].type);
                        itemData.consumableEffects[i].value = EditorGUILayout.FloatField("효과 수치", itemData.consumableEffects[i].value);
                        itemData.consumableEffects[i].duration = EditorGUILayout.FloatField("지속 시간", itemData.consumableEffects[i].duration);
                        
                        if (GUILayout.Button("제거", GUILayout.Width(60)))
                        {
                            itemData.consumableEffects.RemoveAt(i);
                            GUIUtility.ExitGUI();
                        }
                        
                        EditorGUILayout.EndHorizontal();
                    }

                    EditorGUI.indentLevel--;
                    
                    EditorGUILayout.Space(5);
                    EditorGUILayout.LabelField("새 효과 추가", EditorStyles.boldLabel);
                    EditorGUILayout.BeginHorizontal();
                    newEffectType = (ConditionType)EditorGUILayout.EnumPopup("타입", newEffectType);
                    newEffectValue = EditorGUILayout.FloatField("효과 수치", newEffectValue);
                    newEffectDuration = EditorGUILayout.FloatField("지속 시간(초)", newEffectDuration);
                    EditorGUILayout.EndHorizontal();
                    
                    if (GUILayout.Button("효과 추가"))
                    {
                        ConsumableEffect newEffect = new ConsumableEffect
                        {
                            type = newEffectType,
                            value = newEffectValue,
                            duration = newEffectDuration
                        };
                        
                        itemData.consumableEffects.Add(newEffect);
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("아이템을 먼저 생성해주세요.", MessageType.Info);
                }
            }
            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.Space(20);

        if (GUILayout.Button("아이템 생성하기"))
        {
            CreateItem();
        }

        if (itemData != null)
        {
            EditorGUILayout.Space(10);
            if (GUILayout.Button("아이템 저장하기"))
            {
                SaveItem();
            }
        }

        EditorGUILayout.EndScrollView();
    }

    private void CreateItem()
    {
        itemData = ScriptableObject.CreateInstance<ItemData>();
        itemData.id = id;
        itemData.itemName = itemName;
        itemData.description = description;
        itemData.itemType = itemType;
        itemData.equipType = equipType;
        itemData.useType = useType;
        itemData.gold = gold;
        itemData.Icon = icon;
        itemData.itemObj = itemObj;
        
        if (itemType == ItemType.Equipment)
        {
            itemData.maxEnhancementLevel = maxEnhancementLevel;
            itemData.enhancementCost = enhancementCost;
            itemData.enhancementCostMultiplier = enhancementCostMultiplier;
            itemData.options = new System.Collections.Generic.List<ItemOption>();
        }
        else if (itemType == ItemType.Consumable)
        {
            itemData.maxStack = maxStack;
            itemData.consumableEffects = new System.Collections.Generic.List<ConsumableEffect>();
        }
        
        itemData.Initialize(); // 아이템 초기화

        EditorUtility.SetDirty(itemData);
    }

    private void SaveItem()
    {
        if (itemData != null)
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "아이템 저장",
                itemData.itemName,
                "asset",
                "저장할 위치를 선택하세요."
            );

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(itemData, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = itemData;
                
                Debug.Log($"아이템이 저장되었습니다: {path}");
            }
        }
    }
}
#endif 
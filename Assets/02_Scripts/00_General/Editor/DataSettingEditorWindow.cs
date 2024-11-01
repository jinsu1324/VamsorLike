using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

// 데이터 셋팅 바로 가능하도록 에디터 윈도우
public class DataSettingEditorWindow : OdinEditorWindow
{
    // 몬스터 데이터
    [SerializeField]
    private TextAsset _monsterDataTextAsset;

    // 보스 데이터
    [SerializeField]
    private TextAsset _bossDataTextAsset;

    // 히어로 데이터
    [SerializeField]
    private TextAsset _heroDataTextAsset;

    // 레벨 데이터
    [SerializeField]
    private TextAsset _levelDataTextAsset;

    // 메뉴 생성
    [MenuItem("내 메뉴/데이터 셋팅")]
    public static void OpenWindow()
    {
        GetWindow<DataSettingEditorWindow>().Show();
    }


    [Button("레벨 데이터 셋팅", ButtonSizes.Large)]
    public void LevelDataSettingButton()
    {
        //if (_levelDataTextAsset == null)
        //    Debug.Log("레벨데이터 TextAsset이 null입니다.");


        //DataManager dataManager = FindObjectOfType<DataManager>();
        //dataManager.LevelDatas = null;
        

        //LoadCSV.CSV_to_DataList(_levelDataTextAsset);



        //// 경로에 있는 모든 데이터를 가져옴
        //string path = $"Assets/Resources/Data/Level/LevelDataList.asset";
        //ScriptableObject so = AssetDatabase.LoadAssetAtPath<LevelDatas>(path);

        //// 데이터를 못받아왔으면 넘어감
        //if (so == null)
        //{
        //    Debug.LogWarning($" so 의 데이터가 null 입니다");
        //}

        //LevelDatas levelDatas = so as LevelDatas;
        //dataManager.LevelDatas = levelDatas;

        //// 클릭 및 저장
        //EditorUtility.SetDirty(dataManager);
        //AssetDatabase.SaveAssets();

    }


    [Button("몬스터 딕셔너리들 셋팅!", ButtonSizes.Large)]
    public void MonsterSettingButton()
    {
        //if (_monsterDataTextAsset == null)
        //    Debug.Log("몬스터 TextAsset이 null입니다.");

        //// 몬스터CSV를 ScriptableObject로 저장하고 CSV데이터값들도 넣어줌
        ////LoadCSV.CSV_to_Data<MonsterData, MonsterID>(_monsterDataTextAsset, SaveFolderName.Monster);

        //// 몬스터 매니저들 가져와서
        //DataManager dataManager = FindObjectOfType<DataManager>();
        //ObjectManager objectManager = FindObjectOfType<ObjectManager>();

        //// 딕셔너리 클리어
        //dataManager.MonsterDataDict.Clear();
        //objectManager.MonsterObjectDict.Clear();

        //// 딕셔너리에 파일들 넣어주기
        ////Data_to_Dict(dataManager.MonsterDataDict, SaveFolderName.Monster);
        //Object_to_Dict(objectManager.MonsterObjectDict, SaveFolderName.Monster);
    }


    [Button("영웅 딕셔너리들 셋팅!", ButtonSizes.Large)]
    public void HeroSettingButton()
    {
        //if (_heroDataTextAsset == null)
        //    Debug.Log("영웅 TextAsset이 null입니다.");

        //// 영웅 CSV를 -> 데이터로 저장
        ////LoadCSV.CSV_to_Data<HeroData, HeroID>(_heroDataTextAsset, SaveFolderName.Hero);

        //// 영웅 매니저 가져와서
        //DataManager dataManager = FindObjectOfType<DataManager>();
        //ObjectManager objectManager = FindObjectOfType<ObjectManager>();

        //// 딕셔너리 클리어
        //dataManager.HeroDataDict.Clear();
        //objectManager.HeroObjectDict.Clear();

        //// 딕셔너리에 파일들 넣어주기
        ////Data_to_Dict(dataManager.HeroDataDict, SaveFolderName.Hero);
        //Object_to_Dict(objectManager.HeroObjectDict, SaveFolderName.Hero);
    }

    [Button("보스 딕셔너리들 셋팅!", ButtonSizes.Large)]
    public void BossSettingButton()

    {
        //if (_bossDataTextAsset == null)
        //    Debug.Log("몬스터 TextAsset이 null입니다.");

        //// 보스CSV를 ScriptableObject로 저장하고 CSV데이터값들도 넣어줌
        ////LoadCSV.CSV_to_Data<BossData, BossID>(_bossDataTextAsset, SaveFolderName.Boss);

        //// 매니저들 가져와서
        //DataManager dataManager = FindObjectOfType<DataManager>();
        //ObjectManager objectManager = FindObjectOfType<ObjectManager>();

        //// 딕셔너리 클리어
        //dataManager.BossDataDict.Clear();
        //objectManager.BossObjectDict.Clear();

        //// 딕셔너리에 파일들 넣어주기
        ////Data_to_Dict(dataManager.BossDataDict, SaveFolderName.Boss);
        //Object_to_Dict(objectManager.BossObjectDict, SaveFolderName.Boss);
    }


    // 데이터를 딕셔너리에 저장  
    private void Data_to_Dict<EnumKey, DataType>(Dictionary<EnumKey, DataType> dict, SaveFolderName saveFolderName) where EnumKey : Enum  where DataType : ScriptableObject
    {
        // EnumKey의 모든 값들을 가져옴
        EnumKey[] enumKeys = Enum.GetValues(typeof(EnumKey)) as EnumKey[];

        for (int i = 0; i < enumKeys.Length; i++)
        {
            EnumKey enumKey = enumKeys[i];

            // 경로에 있는 모든 데이터를 가져옴
            string path = $"Assets/Resources/Data/{saveFolderName}/{saveFolderName}_{enumKey}.asset";
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<DataType>(path);

            // 데이터를 못받아왔으면 넘어감
            if (so == null)
            {
                Debug.LogWarning($"{enumKey} 의 데이터가 null 입니다");
                continue;
            }

            // 가져온 데이터를를 Dict에 넣기위해 DataType으로 형변환하고 딕셔너리에 추가
            DataType data = so as DataType;
            dict[enumKey] = data;

            // 클릭 및 저장
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();

        }
    }


    // 오브젝트를 딕셔너리에 저장
    private void Object_to_Dict<EnumKey, DataType>(Dictionary<EnumKey, DataType> dict, SaveFolderName saveFolderName) where EnumKey : Enum where DataType : UnityEngine.Object
    {
        // EnumKey의 모든 값들을 가져옴
        EnumKey[] enumKeys = Enum.GetValues(typeof(EnumKey)) as EnumKey[];

        for (int i = 0; i < enumKeys.Length; i++)
        {
            EnumKey enumKey = enumKeys[i];

            // 경로에 있는 모든 프리팹을 가져옴
            string path = $"Assets/Resources/Prefab/{saveFolderName}/{saveFolderName}_{enumKey}.prefab";
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<DataType>(path);

            // 데이터를 못받아왔으면 넘어감
            if (obj == null)
            {
                Debug.LogWarning($"{enumKey} 의 오브젝트가 null 입니다");
                continue;
            }

            // 가져온 오브젝트를 Dict에 넣기위해 DataType으로 형변환하고 딕셔너리에 추가
            DataType data = obj as DataType;
            dict[enumKey] = data;

            // 클릭 및 저장
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
        }
    }
}

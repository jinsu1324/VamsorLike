using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class DataSettingEditorWindow : OdinEditorWindow
{
    // 몬스터 데이터
    [Title("몬스터 데이터 CSV 파일", bold: false)]
    [SerializeField]
    private TextAsset _monsterDataTextAsset;

    // 히어로 데이터
    [Title("히어로 데이터 csv 파일", bold: false)]
    [SerializeField]
    private TextAsset _heroDataTextAsset;

    //// hp 배율
    //[Title("HP 배율", bold: false)]
    //[SerializeField]
    //private float _hpMultiple = 1.0f;


    // 메뉴 생성
    [MenuItem("MyMenu/DataSettingEditorWindow")]
    public static void OpenWindow()
    {
        GetWindow<DataSettingEditorWindow>().Show();
    }

    // 몬스터 Scriptable들을 딕셔너리에 세팅하기
    [Button("Monster Scriptable -> Dict", ButtonSizes.Large)]
    public void MonsterSettingButton()
    {
        if (_monsterDataTextAsset == null)
            Debug.Log("_monsterDataTextAsset이 null입니다.");

        // 몬스터CSV를 ScriptableObject로 저장하고 CSV데이터값들도 넣어줌
        LoadCSV.CSV_to_ScriptableObject<MonsterData, MonsterID>(_monsterDataTextAsset);

        // 하이어라키의 몬스터데이터매니저 속 딕셔너리를 한번 클리어
        MonsterDataManager monsterDataManager = FindObjectOfType<MonsterDataManager>();
        monsterDataManager.MonsterDataDict.Clear();

        // 몬스터ScriptableObject들을 딕셔너리에 세팅
        ScriptableObject_to_Dict<MonsterID, MonsterData>(monsterDataManager.MonsterDataDict);
    }

    // 히어로 Scriptable들을 딕셔너리에 세팅하기
    [Button("Hero Scriptable -> Dict", ButtonSizes.Large)]
    public void HeroSettingButton()
    {
        if (_heroDataTextAsset == null)
            Debug.Log("_heroDataTextAsset이  null입니다.");

        // 히어로CSV를 ScriptableObject로 저장하고 CSV데이터값들도 넣어줌
        LoadCSV.CSV_to_ScriptableObject<HeroData, HeroID>(_heroDataTextAsset);

        // 하이어라키의 히어로데이터매니저 속 딕셔너리를 한번 클리어
        HeroDataManager heroDataManager = FindObjectOfType<HeroDataManager>();
        heroDataManager.HeroDataDict.Clear();

        // 히어로 ScriptableObject들을 딕셔너리에 세팅
        ScriptableObject_to_Dict<HeroID, HeroData>(heroDataManager.HeroDataDict);
    }
    

    // 프로젝트의 몬스터 ScriptableObject 들을 MonsterDataManager속 딕셔너리에 저장  
    private void ScriptableObject_to_Dict<EnumKey, DataType>(Dictionary<EnumKey, DataType> dict) where EnumKey : Enum  where DataType : ScriptableObject
    {
        // MonsterKey enum의 모든 값들을 가져옴
        EnumKey[] enumKeys = Enum.GetValues(typeof(EnumKey)) as EnumKey[];

        for (int i = 0; i < enumKeys.Length; i++)
        {
            EnumKey enumKey = enumKeys[i];

            // 경로에 있는 모든 Scriptable Object를 가져옴
            string path = $"Assets/Resources/{enumKey}.asset";
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<DataType>(path);

            // Scriptable Object 를 못받아왔으면 넘어감
            if (so == null)
            {
                Debug.LogWarning($"{enumKey} 의 Data가 null 입니다");
                continue;
            }

            // 가져온 Scriptable Object 를 Dict에 넣기위해 MonsterData로 형변환
            DataType data = so as DataType;

            //// HP값 변환
            //monsterData.Hp = (int)((float)monsterData.Hp * _hpMultiple);

            // 가져온 몬스터를 딕셔너리에 추가
            dict[enumKey] = data;

            // 클릭 및 저장
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();            
        }
    }
}

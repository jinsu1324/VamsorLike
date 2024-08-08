using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class MonsterDataSettingWindow : EditorWindow
{
    // 몬스터 데이터
    public TextAsset textAsset;

    // hp 배율
    public float hp_Multiple = 1.0f;


    // 메뉴 생성
    [MenuItem("MyMenu/MonsterDataSettingWindow")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MonsterDataSettingWindow));
    }


    // 메뉴에 UI들 표시
    private void OnGUI()
    {
        GUILayout.Label("몬스터데이터 관련");

        // 변수들 UI에 표시
        textAsset = (TextAsset)EditorGUILayout.ObjectField("몬스터 데이터 CSV 파일", textAsset, typeof(TextAsset), true);
        hp_Multiple = EditorGUILayout.FloatField("HP 배율", hp_Multiple);

        // 몬스터Scriptable들을 딕셔너리에 세팅하기
        if (GUILayout.Button("몬스터Scriptable들을 딕셔너리에 세팅하기"))
        {            
            if (textAsset == null)
                Debug.Log("textAsset이 null입니다.");

            // 몬스터CSV를 ScriptableObject로 저장하고 CSV데이터값들도 넣어줌
            LoadCSV.CSV_to_ScriptableObject<MonsterData, MonsterID>(textAsset);

            // 몬스터ScriptableObject들을 딕셔너리에 세팅
            MonsterDataSave();

        }
    }

    /// <summary>
    /// 프로젝트의 몬스터 ScriptableObject 들을 MonsterDataManager속 딕셔너리에 저장
    /// </summary>
    private void MonsterDataSave()
    {        
        MonsterDataManager monsterDataManager = FindObjectOfType<MonsterDataManager>();
        monsterDataManager._monsterDataDict.Clear();

        // MonsterKey enum의 모든 값들을 가져옴
        MonsterID[] monsterKeys = Enum.GetValues(typeof(MonsterID)) as MonsterID[];

        for (int i = 0; i < monsterKeys.Length; i++)
        {
            MonsterID monsterKey = monsterKeys[i];

            // 경로에 있는 모든 Scriptable Object를 가져옴
            string path = $"Assets/Resources/{monsterKey}.asset";
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<MonsterData>(path);

            // Scriptable Object 를 못받아왔으면 넘어감
            if (so == null)
            {
                Debug.LogWarning($"{monsterKey} 의 monsterData가 null 입니다");
                continue;
            }

            // 가져온 Scriptable Object 를 Dict에 넣기위해 MonsterData로 형변환
            MonsterData monsterData = so as MonsterData;

            // HP값 변환
            monsterData.HP = (int)((float)monsterData.HP * hp_Multiple);

            // 가져온 몬스터를 딕셔너리에 추가
            monsterDataManager._monsterDataDict[monsterKey] = monsterData;

            // 클릭 및 저장
            EditorUtility.SetDirty(monsterData);
            AssetDatabase.SaveAssets();
            
        }
    }

   
}

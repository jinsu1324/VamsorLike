using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class MonsterSettingWindow : EditorWindow
{

    public TextAsset textAsset;

    public float hp_Multiple = 1.0f;

    private void OnGUI()
    {
        GUILayout.Label("HI");
        textAsset = (TextAsset)EditorGUILayout.ObjectField("CSV 파일", textAsset, typeof(TextAsset), true);

        hp_Multiple = EditorGUILayout.FloatField("HP 배율", hp_Multiple);

        if (GUILayout.Button("세팅"))
        {
            if (textAsset == null)
            {
                Debug.Log("textAsset null");
            }
            else
            {
                LoadCSV.CSV_to_MonsterData(textAsset);
            }

            MonsterDataSave();

        }
    }

    /// <summary>
    /// Monsterdata Scriptable ----->  MonsterDataDict
    /// </summary>
    private void MonsterDataSave()
    {
        MonsterDataManager monsterDataManager = FindObjectOfType<MonsterDataManager>();

        monsterDataManager._monsterDataDict.Clear();

        // enum의 모든 값들을 가져옴
        MonsterKey[] monsterKeys = Enum.GetValues(typeof(MonsterKey)) as MonsterKey[];


        for (int i = 0; i < monsterKeys.Length; i++)
        {
            MonsterKey monsterKey = monsterKeys[i];

            string path = $"Assets/Resources/{monsterKey}.asset";
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<MonsterData>(path);

            if (so == null)
            {
                Debug.LogWarning($"{monsterKey} 의 monsterData가 null 입니다");
                continue;
            }
                


            MonsterData monsterData = so as MonsterData;

            monsterData.HP = (int)((float)monsterData.HP * hp_Multiple);

            

            monsterDataManager._monsterDataDict.Add(monsterData);


            EditorUtility.SetDirty(monsterData);
            AssetDatabase.SaveAssets();
            
        }
    }

    [MenuItem("Study/My Study")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MonsterSettingWindow));
    }
}

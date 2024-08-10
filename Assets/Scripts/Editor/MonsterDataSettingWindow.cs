using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

public class MonsterDataSettingWindow : EditorWindow
{
    // ���� ������
    [SerializeField]
    private TextAsset TextAsset;

    // hp ����
    [SerializeField]
    private float Hp_Multiple = 1.0f;


    // �޴� ����
    [MenuItem("MyMenu/MonsterDataSettingWindow")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(MonsterDataSettingWindow));
    }


    // �޴��� UI�� ǥ��
    private void OnGUI()
    {
        GUILayout.Label("���͵����� ����");

        // ������ UI�� ǥ��
        TextAsset = (TextAsset)EditorGUILayout.ObjectField("���� ������ CSV ����", TextAsset, typeof(TextAsset), true);
        Hp_Multiple = EditorGUILayout.FloatField("HP ����", Hp_Multiple);

        // ����Scriptable���� ��ųʸ��� �����ϱ�
        if (GUILayout.Button("����Scriptable���� ��ųʸ��� �����ϱ�"))
        {            
            if (TextAsset == null)
                Debug.Log("textAsset�� null�Դϴ�.");

            // ����CSV�� ScriptableObject�� �����ϰ� CSV�����Ͱ��鵵 �־���
            LoadCSV.CSV_to_ScriptableObject<MonsterData, MonsterID>(TextAsset);

            // ����ScriptableObject���� ��ųʸ��� ����
            MonsterDataSave();

        }
    }

    /// <summary>
    /// ������Ʈ�� ���� ScriptableObject ���� MonsterDataManager�� ��ųʸ��� ����
    /// </summary>
    private void MonsterDataSave()
    {        
        MonsterDataManager monsterDataManager = FindObjectOfType<MonsterDataManager>();
        monsterDataManager.MonsterDataDict.Clear();

        // MonsterKey enum�� ��� ������ ������
        MonsterID[] monsterKeys = Enum.GetValues(typeof(MonsterID)) as MonsterID[];

        for (int i = 0; i < monsterKeys.Length; i++)
        {
            MonsterID monsterKey = monsterKeys[i];

            // ��ο� �ִ� ��� Scriptable Object�� ������
            string path = $"Assets/Resources/{monsterKey}.asset";
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<MonsterData>(path);

            // Scriptable Object �� ���޾ƿ����� �Ѿ
            if (so == null)
            {
                Debug.LogWarning($"{monsterKey} �� monsterData�� null �Դϴ�");
                continue;
            }

            // ������ Scriptable Object �� Dict�� �ֱ����� MonsterData�� ����ȯ
            MonsterData monsterData = so as MonsterData;

            // HP�� ��ȯ
            monsterData.Hp = (int)((float)monsterData.Hp * Hp_Multiple);

            // ������ ���͸� ��ųʸ��� �߰�
            monsterDataManager.MonsterDataDict[monsterKey] = monsterData;

            // Ŭ�� �� ����
            EditorUtility.SetDirty(monsterData);
            AssetDatabase.SaveAssets();
            
        }
    }

   
}

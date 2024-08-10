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
    // ���� ������
    [Title("���� ������ CSV ����", bold: false)]
    [SerializeField]
    private TextAsset _monsterDataTextAsset;

    // ����� ������
    [Title("����� ������ csv ����", bold: false)]
    [SerializeField]
    private TextAsset _heroDataTextAsset;

    //// hp ����
    //[Title("HP ����", bold: false)]
    //[SerializeField]
    //private float _hpMultiple = 1.0f;


    // �޴� ����
    [MenuItem("MyMenu/DataSettingEditorWindow")]
    public static void OpenWindow()
    {
        GetWindow<DataSettingEditorWindow>().Show();
    }

    // ���� Scriptable���� ��ųʸ��� �����ϱ�
    [Button("Monster Scriptable -> Dict", ButtonSizes.Large)]
    public void MonsterSettingButton()
    {
        if (_monsterDataTextAsset == null)
            Debug.Log("_monsterDataTextAsset�� null�Դϴ�.");

        // ����CSV�� ScriptableObject�� �����ϰ� CSV�����Ͱ��鵵 �־���
        LoadCSV.CSV_to_ScriptableObject<MonsterData, MonsterID>(_monsterDataTextAsset);

        // ���̾��Ű�� ���͵����͸Ŵ��� �� ��ųʸ��� �ѹ� Ŭ����
        MonsterDataManager monsterDataManager = FindObjectOfType<MonsterDataManager>();
        monsterDataManager.MonsterDataDict.Clear();

        // ����ScriptableObject���� ��ųʸ��� ����
        ScriptableObject_to_Dict<MonsterID, MonsterData>(monsterDataManager.MonsterDataDict);
    }

    // ����� Scriptable���� ��ųʸ��� �����ϱ�
    [Button("Hero Scriptable -> Dict", ButtonSizes.Large)]
    public void HeroSettingButton()
    {
        if (_heroDataTextAsset == null)
            Debug.Log("_heroDataTextAsset��  null�Դϴ�.");

        // �����CSV�� ScriptableObject�� �����ϰ� CSV�����Ͱ��鵵 �־���
        LoadCSV.CSV_to_ScriptableObject<HeroData, HeroID>(_heroDataTextAsset);

        // ���̾��Ű�� ����ε����͸Ŵ��� �� ��ųʸ��� �ѹ� Ŭ����
        HeroDataManager heroDataManager = FindObjectOfType<HeroDataManager>();
        heroDataManager.HeroDataDict.Clear();

        // ����� ScriptableObject���� ��ųʸ��� ����
        ScriptableObject_to_Dict<HeroID, HeroData>(heroDataManager.HeroDataDict);
    }
    

    // ������Ʈ�� ���� ScriptableObject ���� MonsterDataManager�� ��ųʸ��� ����  
    private void ScriptableObject_to_Dict<EnumKey, DataType>(Dictionary<EnumKey, DataType> dict) where EnumKey : Enum  where DataType : ScriptableObject
    {
        // MonsterKey enum�� ��� ������ ������
        EnumKey[] enumKeys = Enum.GetValues(typeof(EnumKey)) as EnumKey[];

        for (int i = 0; i < enumKeys.Length; i++)
        {
            EnumKey enumKey = enumKeys[i];

            // ��ο� �ִ� ��� Scriptable Object�� ������
            string path = $"Assets/Resources/{enumKey}.asset";
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<DataType>(path);

            // Scriptable Object �� ���޾ƿ����� �Ѿ
            if (so == null)
            {
                Debug.LogWarning($"{enumKey} �� Data�� null �Դϴ�");
                continue;
            }

            // ������ Scriptable Object �� Dict�� �ֱ����� MonsterData�� ����ȯ
            DataType data = so as DataType;

            //// HP�� ��ȯ
            //monsterData.Hp = (int)((float)monsterData.Hp * _hpMultiple);

            // ������ ���͸� ��ųʸ��� �߰�
            dict[enumKey] = data;

            // Ŭ�� �� ����
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();            
        }
    }
}

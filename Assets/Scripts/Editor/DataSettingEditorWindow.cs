using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

// ������ ���� �ٷ� �����ϵ��� ������ ������
public class DataSettingEditorWindow : OdinEditorWindow
{
    // ���� ������
    [SerializeField]
    private TextAsset _monsterDataTextAsset;

    // ����� ������
    [SerializeField]
    private TextAsset _heroDataTextAsset;

    // ����� ��ų ������
    [SerializeField]
    private TextAsset _skillDataTextAsset;

    // �޴� ����
    [MenuItem("�� �޴�/������ ����")]
    public static void OpenWindow()
    {
        GetWindow<DataSettingEditorWindow>().Show();
    }


    // ���� �����͸� -> ������, ������Ʈ ��ųʸ��� �����ϱ�
    [Button("���� ��ųʸ��� ����!", ButtonSizes.Large)]
    public void MonsterSettingButton()
    {
        if (_monsterDataTextAsset == null)
            Debug.Log("���� TextAsset�� null�Դϴ�.");

        // ����CSV�� ScriptableObject�� �����ϰ� CSV�����Ͱ��鵵 �־���
        LoadCSV.CSV_to_Data<MonsterData, MonsterID>(_monsterDataTextAsset, SaveFolderName.Monster);

        // ������ �Ŵ����� �����ͼ�
        DataManager dataManager = FindObjectOfType<DataManager>();
        ObjectManager objectManager = FindObjectOfType<ObjectManager>();

        // ��ųʸ� Ŭ����
        dataManager.MonsterDataDict.Clear();        
        objectManager.MonsterObjectDict.Clear();

        // ��ųʸ��� ���ϵ� �־��ֱ�
        Data_to_Dict(dataManager.MonsterDataDict, SaveFolderName.Monster);
        Object_to_Dict(objectManager.MonsterObjectDict, SaveFolderName.Monster);
    }


    // ���� �����͸� -> ������, ������Ʈ ��ųʸ��� �����ϱ�
    [Button("���� ��ųʸ��� ����!", ButtonSizes.Large)]
    public void HeroSettingButton()
    {
        if (_heroDataTextAsset == null)
            Debug.Log("���� TextAsset�� null�Դϴ�.");

        // ���� CSV�� -> �����ͷ� ����
        LoadCSV.CSV_to_Data<HeroData, HeroID>(_heroDataTextAsset, SaveFolderName.Hero);

        // ������ �Ŵ����� �����ͼ�
        DataManager dataManager = FindObjectOfType<DataManager>();
        ObjectManager objectManager = FindObjectOfType<ObjectManager>();

        // ��ųʸ� Ŭ����
        dataManager.HeroDataDict.Clear();
        objectManager.HeroObjectDict.Clear();

        // ��ųʸ��� ���ϵ� �־��ֱ�
        Data_to_Dict(dataManager.HeroDataDict, SaveFolderName.Hero);
        Object_to_Dict(objectManager.HeroObjectDict, SaveFolderName.Hero);
    }


    // ��ų �����͸� -> ������ ��ųʸ��� �����ϱ�
    [Button("��ų ������ ��ųʸ� ����!", ButtonSizes.Large)]
    public void SkillDataSettingButton()
    {
        if (_skillDataTextAsset == null)
            Debug.Log("���� TextAsset�� null�Դϴ�.");

        // ���� CSV�� -> �����ͷ� ����
        LoadCSV.CSV_to_Data<SkillData, SkillID>(_skillDataTextAsset, SaveFolderName.Skill);

        // ������ �Ŵ����� �����ͼ�
        DataManager dataManager = FindObjectOfType<DataManager>();
        //ObjectManager objectManager = FindObjectOfType<ObjectManager>();

        // ��ųʸ� Ŭ����
        dataManager.SkillDataDict.Clear();
        //objectManager.HeroObjectDict.Clear();

        // ��ųʸ��� ���ϵ� �־��ֱ�
        Data_to_Dict(dataManager.SkillDataDict, SaveFolderName.Skill);
        //Object_to_Dict(objectManager.HeroObjectDict, SaveFolderName.Hero);
    }


    // �����͸� ��ųʸ��� ����  
    private void Data_to_Dict<EnumKey, DataType>(Dictionary<EnumKey, DataType> dict, SaveFolderName saveFolderName) where EnumKey : Enum  where DataType : ScriptableObject
    {
        // EnumKey�� ��� ������ ������
        EnumKey[] enumKeys = Enum.GetValues(typeof(EnumKey)) as EnumKey[];

        for (int i = 0; i < enumKeys.Length; i++)
        {
            EnumKey enumKey = enumKeys[i];

            // ��ο� �ִ� ��� �����͸� ������
            string path = $"Assets/Resources/Data/{saveFolderName}/{saveFolderName}_{enumKey}.asset";
            ScriptableObject so = AssetDatabase.LoadAssetAtPath<DataType>(path);

            // �����͸� ���޾ƿ����� �Ѿ
            if (so == null)
            {
                Debug.LogWarning($"{enumKey} �� �����Ͱ� null �Դϴ�");
                continue;
            }

            // ������ �����͸��� Dict�� �ֱ����� DataType���� ����ȯ�ϰ� ��ųʸ��� �߰�
            DataType data = so as DataType;
            dict[enumKey] = data;

            // Ŭ�� �� ����
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();            
        }
    }


    // ������Ʈ�� ��ųʸ��� ����
    private void Object_to_Dict<EnumKey, DataType>(Dictionary<EnumKey, DataType> dict, SaveFolderName saveFolderName) where EnumKey : Enum where DataType : UnityEngine.Object
    {
        // EnumKey�� ��� ������ ������
        EnumKey[] enumKeys = Enum.GetValues(typeof(EnumKey)) as EnumKey[];

        for (int i = 0; i < enumKeys.Length; i++)
        {
            EnumKey enumKey = enumKeys[i];

            // ��ο� �ִ� ��� �������� ������
            string path = $"Assets/Resources/Prefab/{saveFolderName}/{saveFolderName}_{enumKey}.prefab";
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<DataType>(path);

            // �����͸� ���޾ƿ����� �Ѿ
            if (obj == null)
            {
                Debug.LogWarning($"{enumKey} �� ������Ʈ�� null �Դϴ�");
                continue;
            }

            // ������ ������Ʈ�� Dict�� �ֱ����� DataType���� ����ȯ�ϰ� ��ųʸ��� �߰�
            DataType data = obj as DataType;
            dict[enumKey] = data;

            // Ŭ�� �� ����
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
        }
    }
}

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class SkillLevelDataSettingEditorWindow : OdinEditorWindow
{
    // ��ų ���� ������ csv
    [SerializeField]
    private TextAsset TextAsset_SlashAttack;
    [SerializeField]
    private TextAsset TextAsset_Boomerang;
    [SerializeField]
    private TextAsset TextAsset_Sniper;

    // ��ų ���� ������ �����̸�
    private const string FILENAME_SlashAttack = "SkillLevelData_SlashAttack";
    private const string FILENAME_Boomerang = "SkillLevelData_Boomerang";
    private const string FILENAME_Sniper = "SkillLevelData_Sniper";


    // �޴� ����
    [MenuItem("�� �޴�/��ų ���� ������ ����")]
    public static void OpenWindow()
    {
        GetWindow<SkillLevelDataSettingEditorWindow>().Show();
    }



    [Button("�ؽ�Ʈ ���µ� �ε�")]
    private void LoadTextAssets()
    {
        TextAsset_SlashAttack = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/Resources/CSV/{FILENAME_SlashAttack}.csv");
        TextAsset_Boomerang = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/Resources/CSV/{FILENAME_Boomerang}.csv");
        TextAsset_Sniper = AssetDatabase.LoadAssetAtPath<TextAsset>($"Assets/Resources/CSV/{FILENAME_Sniper}.csv");
    }


    [Button("��ų ���� ������ ����", ButtonSizes.Large)]
    public void SkillLevelDataSettingButton()
    {
        // �����ʿ�!
        //CSV_Save_SkillLevelData<SkillData_SlashAttack>(TextAsset_SlashAttack, FILENAME_SlashAttack, SkillID.SlashAttack);
        //CSV_Save_SkillLevelData<SkillData_Boomerang>(TextAsset_Boomerang, FILENAME_Boomerang, SkillID.Boomerang);
        //CSV_Save_SkillLevelData<SkillData_Sniper>(TextAsset_Sniper, FILENAME_Sniper, SkillID.Sniper);
    }


    //// csv�� SkillLevelData ��ũ���ͺ�� ����
    //public static void CSV_Save_SkillLevelData<SkillData>(TextAsset textAsset, string fileName, SkillID skillID) where SkillData : SkillData_Base, new()
    //{
    //    // ��ũ���ͺ� �������� �κ�
    //    string path = $"Assets/Resources/Data/Skill/{fileName}.asset";
    //    ScriptableObject so = AssetDatabase.LoadAssetAtPath<SkillLevelDataSO>(path);

    //    if (so == null)
    //    {
    //        so = ScriptableObject.CreateInstance<SkillLevelDataSO>();

    //        AssetDatabase.CreateAsset(so, path);
    //        AssetDatabase.SaveAssets();
    //    }

    //    SkillLevelDataSO skillLevelDataSO = so as SkillLevelDataSO;
    //    if (skillLevelDataSO.SkillDataList != null)
    //        skillLevelDataSO.SkillDataList.Clear();


    //    // �ؽ�Ʈ �ɰ�
    //    string csv = textAsset.text;
    //    string[] csvLines = csv.Split(System.Environment.NewLine);
        
    //    int headerIndex = 1;
    //    string[] headers = csvLines[headerIndex].Split(',');


    //    // ������ �Ҵ�
    //    for (int i = headerIndex + 1; i < csvLines.Length; i++)
    //    {
    //        // ���÷��� �غ�
    //        Type type = typeof(SkillData);
    //        SkillData skillData = new SkillData();

    //        string[] datas = csvLines[i].Split(',');

    //        for (int k = 0; k < datas.Length; k++)
    //        {
    //            FieldInfo fieldInfo = type.GetField(headers[k]);

    //            if (fieldInfo.FieldType == typeof(int))
    //                fieldInfo.SetValue(skillData, int.Parse(datas[k]));

    //            else if (fieldInfo.FieldType == typeof(float))
    //                fieldInfo.SetValue(skillData, float.Parse(datas[k]));

    //            else if (fieldInfo.FieldType == typeof(string))
    //                fieldInfo.SetValue(skillData, datas[k]);
    //        }            

    //        skillLevelDataSO.SkillDataList.Add(skillData);

    //        // Ŭ�� �� ����
    //        EditorUtility.SetDirty(skillLevelDataSO);
    //        AssetDatabase.SaveAssets();



    //        // ������ �Ŵ����� �����ͼ�
    //        DataManager dataManager = FindObjectOfType<DataManager>();

    //        // ��ųʸ� Ŭ����
    //        if (dataManager.SkillDataDict.ContainsKey(skillID))
    //        {
    //            dataManager.SkillDataDict.Remove(skillID);
    //            dataManager.SkillDataDict[skillID] = skillLevelDataSO;
    //        }
    //        else
    //        {
    //            dataManager.SkillDataDict[skillID] = skillLevelDataSO;
    //        }

    //        // Ŭ�� �� ����
    //        EditorUtility.SetDirty(dataManager);
    //        AssetDatabase.SaveAssets();
    //    }

    //}
}

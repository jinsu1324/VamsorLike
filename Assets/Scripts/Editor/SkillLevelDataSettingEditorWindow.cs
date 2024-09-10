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


    [Button("��ų ���� ������ ����", ButtonSizes.Large)]
    public void SkillLevelDataSettingButton()
    {
        CSV_Save_SkillLevelData<SkillLevelData_SlashAttack, SkillData_SlashAttack>(TextAsset_SlashAttack, FILENAME_SlashAttack);
        CSV_Save_SkillLevelData<SkillLevelData_Boomerang, SkillData_Boomerang>(TextAsset_Boomerang, FILENAME_Boomerang);
        CSV_Save_SkillLevelData<SkillLevelData_Sniper, SkillData_Sniper>(TextAsset_Sniper, FILENAME_Sniper);
    }

    // csv�� SkillLevelData ��ũ���ͺ�� ����
    public static void CSV_Save_SkillLevelData<SkillLevelData, SkillData>(TextAsset textAsset, string fileName) where SkillLevelData : SkillLevelDataBase<SkillData> where SkillData : new()
    {
        // ��ũ���ͺ� �������� �κ�
        string path = $"Assets/Resources/Data/Skill/{fileName}.asset";
        ScriptableObject so = AssetDatabase.LoadAssetAtPath<SkillLevelData>(path);

        if (so == null)
        {
            so = ScriptableObject.CreateInstance<SkillLevelData>();

            AssetDatabase.CreateAsset(so, path);
            AssetDatabase.SaveAssets();
        }

        SkillLevelData skillLevelData = so as SkillLevelData;
        if (skillLevelData.SkillDataList != null)
            skillLevelData.SkillDataList.Clear();


        // �ؽ�Ʈ �ɰ�
        string csv = textAsset.text;
        string[] csvLines = csv.Split(System.Environment.NewLine);

        // ������� ���ηε� �и�
        int headerIndex = 1;
        string[] headers = csvLines[headerIndex].Split(',');




        for (int i = headerIndex + 1; i < csvLines.Length; i++)
        {

            // ���÷��� �غ�
            Type type = typeof(SkillData);
            SkillData skillData = new SkillData();

            string[] datas = csvLines[i].Split(',');

            for (int k = 0; k < datas.Length; k++)
            {
                FieldInfo fieldInfo = type.GetField(headers[k]);

                if (fieldInfo.FieldType == typeof(int))
                    fieldInfo.SetValue(skillData, int.Parse(datas[k]));

                else if (fieldInfo.FieldType == typeof(float))
                    fieldInfo.SetValue(skillData, float.Parse(datas[k]));

                else if (fieldInfo.FieldType == typeof(string))
                    fieldInfo.SetValue(skillData, datas[k]);

            }

            skillLevelData.SkillDataList.Add(skillData);
        }

    }
}

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
    // 스킬 레벨 데이터 csv
    [SerializeField]
    private TextAsset TextAsset_SlashAttack;
    [SerializeField]
    private TextAsset TextAsset_Boomerang;
    [SerializeField]
    private TextAsset TextAsset_Sniper;

    // 스킬 레벨 데이터 파일이름
    private const string FILENAME_SlashAttack = "SkillLevelData_SlashAttack";
    private const string FILENAME_Boomerang = "SkillLevelData_Boomerang";
    private const string FILENAME_Sniper = "SkillLevelData_Sniper";


    // 메뉴 생성
    [MenuItem("내 메뉴/스킬 레벨 데이터 셋팅")]
    public static void OpenWindow()
    {
        GetWindow<SkillLevelDataSettingEditorWindow>().Show();
    }


    [Button("스킬 레벨 데이터 셋팅", ButtonSizes.Large)]
    public void SkillLevelDataSettingButton()
    {
        CSV_Save_SkillLevelData<SkillLevelData_SlashAttack, SkillData_SlashAttack>(TextAsset_SlashAttack, FILENAME_SlashAttack);
        CSV_Save_SkillLevelData<SkillLevelData_Boomerang, SkillData_Boomerang>(TextAsset_Boomerang, FILENAME_Boomerang);
        CSV_Save_SkillLevelData<SkillLevelData_Sniper, SkillData_Sniper>(TextAsset_Sniper, FILENAME_Sniper);
    }

    // csv를 SkillLevelData 스크립터블로 저장
    public static void CSV_Save_SkillLevelData<SkillLevelData, SkillData>(TextAsset textAsset, string fileName) where SkillLevelData : SkillLevelDataBase<SkillData> where SkillData : new()
    {
        // 스크립터블 가져오는 부분
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


        // 텍스트 쪼갬
        string csv = textAsset.text;
        string[] csvLines = csv.Split(System.Environment.NewLine);

        // 헤더줄은 가로로도 분리
        int headerIndex = 1;
        string[] headers = csvLines[headerIndex].Split(',');




        for (int i = headerIndex + 1; i < csvLines.Length; i++)
        {

            // 리플렉션 준비
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

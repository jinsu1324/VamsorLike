using Codice.CM.Common;
using DG.DemiEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class JINSU_DataTableToScriptableObject : OdinEditorWindow
{
    // Google ���������Ʈ�� ���� ID. URL���� ã�� �� ����.
    private readonly string _sheetId = "1_ksfuyQDI4muo28hC8nCtyjwMLohw3oOyGBk8CsqKZE";

    // Google Cloud Console���� �߱޹��� API Ű. �� Ű�� ���� Google Sheets API�� ������ �� ����.
    private readonly string _apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";


    // HeroData
    // �����͸� ������ ��Ʈ �̸��� ���� (��: "Sheet1!A1:G7").
    private readonly string _range_HeroData = "HeroData!A1:G5";
    // �����͸� ������ ��ο� �̸�
    private readonly string _fileName_HeroData = "HeroDatas";


    // LevelData
    // �����͸� ������ ��Ʈ �̸��� ���� (��: "Sheet1!A1:G7").
    private readonly string _range_LevelData = "LevelData!A1:C42";
    // �����͸� ������ ��ο� �̸�
    private readonly string _fileName_LevelData = "LevelDatas";


    // WaveData
    // �����͸� ������ ��Ʈ �̸��� ���� (��: "Sheet1!A1:G7").
    private readonly string _range_WaveData = "WaveData!A1:F32";
    // �����͸� ������ ��ο� �̸�
    private readonly string _fileName_WaveData = "WaveDatas";


    // MonsterData
    // �����͸� ������ ��Ʈ �̸��� ���� (��: "Sheet1!A1:G7").
    private readonly string _range_MonsterData = "MonsterData!A1:E11";
    // �����͸� ������ ��ο� �̸�
    private readonly string _fileName_MonsterData = "MonsterDatas";
      

    // BossData
    // �����͸� ������ ��Ʈ �̸��� ���� (��: "Sheet1!A1:G7").
    private readonly string _range_BossData = "BossData!A1:M3";
    // �����͸� ������ ��ο� �̸�
    private readonly string _fileName_BossData = "BossDatas";



    // SkillData
    // �����͸� ������ ��Ʈ �̸��� ���� (��: "Sheet1!A1:G7").
    private readonly string _range_SkillData = "SkillData!A1:I27";
    // �����͸� ������ ��ο� �̸�
    private readonly string _fileName_SkillData = "SkillDatas";



    /// <summary>
    /// �޴� ����
    /// </summary>
    [MenuItem("My Menu/������ �ֽ�ȭ")]
    public static void OpenWindow()
    {
        GetWindow<JINSU_DataTableToScriptableObject>().Show();
    }


    // �ȳ� �����ڽ�
    [InfoBox("�����͸� �ֽ�ȭ�Ϸ��� �Ʒ� ��ư�� ������� �����ּ���", InfoMessageType.Info)]


    /// <summary>
    /// ������ ��ũ���ͺ� ������Ʈ ��ġ ��ư
    /// </summary>
    [Button("1. ������ ��ũ���ͺ� ������Ʈ �ֽ�ȭ �ϱ�!", ButtonSizes.Large)]
    public void DataFetch()
    {
        FetchAndConvertData<HeroData, HeroDatas>(_range_HeroData, _fileName_HeroData);
        FetchAndConvertData<LevelData, LevelDatas>(_range_LevelData, _fileName_LevelData);
        FetchAndConvertData<WaveData, WaveDatas>(_range_WaveData, _fileName_WaveData);
        FetchAndConvertData<MonsterData, MonsterDatas>(_range_MonsterData, _fileName_MonsterData);
        FetchAndConvertData<BossData, BossDatas>(_range_BossData, _fileName_BossData);
        FetchAndConvertData<SkillData, SkillDatas>(_range_SkillData, _fileName_SkillData);
    }

    /// <summary>
    /// ������ �Ŵ����� ��ũ �ֽ�ȭ ��ư
    /// </summary>
    [Button("2. ������ �Ŵ��� ��ũ �ֽ�ȭ �ϱ�!", ButtonSizes.Large)]
    public void ManagerLinkFetch()
    {
        DataManager dataManager = FindObjectOfType<DataManager>();
        if (dataManager == null)
        {
            Debug.LogError("DataManager�� ���� �����ϴ�.");
            return;
        }

        dataManager.HeroDatas = Resources.Load<HeroDatas>($"Data/{_fileName_HeroData}"); 
        dataManager.LevelDatas = Resources.Load<LevelDatas>($"Data/{_fileName_LevelData}");
        dataManager.WaveDatas = Resources.Load<WaveDatas>($"Data/{_fileName_WaveData}");
        dataManager.MonsterDatas = Resources.Load<MonsterDatas>($"Data/{_fileName_MonsterData}");
        dataManager.BossDatas = Resources.Load<BossDatas>($"Data/{_fileName_BossData}");
        dataManager.SkillDatas = Resources.Load<SkillDatas>($"Data/{_fileName_SkillData}");
        
        EditorUtility.SetDirty(dataManager);
        AssetDatabase.SaveAssets();

        // ���� �޽��� �ڽ�
        EditorUtility.DisplayDialog("����!", $"������ �Ŵ��� ��ũ�� �ֽ�ȭ�Ǿ����ϴ�!", "Ȯ��");
    }

    /// <summary>
    /// �����Ͱ� �������� ��ũ���ͺ� ������Ʈ�� ��ȯ
    /// </summary>
    public async void FetchAndConvertData<Data, DatasSO>(string range, string fileName) where Data : BaseData, new() where DatasSO : DataListSO<Data>
    {
        // Google Sheets API�� �����͸� ��û�� URL. ���⼭ sheetId, range, apiKey�� ����Ͽ� API ��û�� ���� URL�� �ϼ�.
        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{_sheetId}/values/{range}?key={_apiKey}";

        // HttpClient�� HTTP ��û�� ������ ������ ���� �� ����ϴ� Ŭ����.
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Google Sheets API�� GET ��û�� �񵿱������� �����ϰ�, ������ ������ ������ ��ٸ�.
                HttpResponseMessage response = await client.GetAsync(url);

                // ���� ������ ���������� ���θ� Ȯ��.
                if (response.IsSuccessStatusCode)
                {
                    // ���� ������ ���ڿ��� �о��. ���⿡�� JSON ������ ���������Ʈ �����Ͱ� �������.
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // ���� ����� �ֿܼ� ����� Ȯ��.
                    //Debug.Log(responseBody);

                    // ������� JSON �����͸� ScriptableObject�� ��ȯ�ϴ� �޼��带 ȣ��.
                    CreateDataListSO<Data, DatasSO>(responseBody, fileName);
                }
                else
                {
                    // ���� ���� �� ���� �޽����� ���.
                    Debug.LogError($"Failed to fetch data. Error Status Code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                // HTTP ��û �� ���ܰ� �߻��ϸ� ���� �޽����� ���.
                Debug.LogError($"Failed to fetch data. Request Error : {e.Message}");
            }
        }
    }

    /// <summary>
    /// // JSON �����͸� ScriptableObject�� ��ȯ�ϴ� �޼���.
    /// </summary>
    private void CreateDataListSO<Data, DatasSO>(string json, string fileName) where Data : BaseData, new() where DatasSO : DataListSO<Data>
    {
        // JSON �����͸� JsonFormat ��ü�� ��ø��������(���ڿ����� ��ü�� ��ȯ)��.
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // ���ο� ScriptableObject�� ����. ���⿡ �Ľ̵� �����͸� ������ ����.
        DatasSO datasSO = CreateInstance<DatasSO>();
        datasSO.DataList = new List<Data>();

        // �����
        var headerValues = jsonData.values[1];

        // �Ľ̵� JSON �������� ������ �ϳ��� ScriptableObject�� ä�� (2��° �ε������� ������ ����)
        for (int i = 2; i < jsonData.values.Length; i++) 
        {
            // ���� ���� �����͸� ������
            var values = jsonData.values[i];

            // ���÷��� �غ�
            Type type = typeof(Data);
            Data data = new Data();

            // ����� �����͸� �ϳ��� ����
            for (int h = 0; h < headerValues.Length; h++)
            {
                FieldInfo fieldInfo = type.GetField(headerValues[h]);

                // ���� ������� ��� �׳� �������� �������� (-�� ���̺� ǥ���ص�)
                if (values[h] == "-")
                    continue;

                // ���÷������� ������ �� �Ҵ�
                if (fieldInfo.FieldType == typeof(int))
                {
                    fieldInfo.SetValue(data, int.Parse(values[h]));
                }
                else if (fieldInfo.FieldType == typeof(float))
                {
                    fieldInfo.SetValue(data, float.Parse(values[h]));
                }
                else if (fieldInfo.FieldType == typeof(string))
                {
                    fieldInfo.SetValue(data, values[h]);
                }

                else if (fieldInfo.FieldType == typeof(int[]))
                {
                    fieldInfo.SetValue(data, Array.ConvertAll(values[h].Split(','), int.Parse));
                }
                else if (fieldInfo.FieldType == typeof(float[]))
                {
                    fieldInfo.SetValue(data, Array.ConvertAll(values[h].Split(','), float.Parse));
                }
                else if (fieldInfo.FieldType == typeof(string[]))
                {
                    fieldInfo.SetValue(data, values[h].Split(','));
                }
            }

            // ���������̺� �ִ� ������ ������ �� �� data�� -> datasSO DataList�� �߰�
            datasSO.DataList.Add(data);

            // ���� ������ ����Ƽ�� �˸�
            EditorUtility.SetDirty(datasSO);
        }

        // ScriptableObject�� ����Ƽ �������� ����
        string path = $"Assets/Resources/Data/{fileName}.asset";
        DatasSO existingAsset = AssetDatabase.LoadAssetAtPath<DatasSO>(fileName);

        if (existingAsset == null)
        {
            // ������ ������ ���� ����
            AssetDatabase.CreateAsset(datasSO, path);

            // ���� ������ ����Ƽ�� �˸�
            EditorUtility.SetDirty(datasSO);
        }
        else
        {
            // ������ �̹� �����ϸ� ���� ������ ����
            existingAsset= datasSO;

            // ���� ������ ����Ƽ�� �˸�
            EditorUtility.SetDirty(existingAsset);
        }

        // ���� ����
        AssetDatabase.SaveAssets();

        // ������ ���������� ����Ǿ����� �˸��� �޽��� �ڽ��� ���
        EditorUtility.DisplayDialog("����!", $"��ũ���ͺ� ������Ʈ�� ���� or ������Ʈ �Ǿ����ϴ�! {path}", "Ȯ��");
    }
}


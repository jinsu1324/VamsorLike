using Codice.CM.Common;
using DG.DemiEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class Sample : OdinEditorWindow
{
    // Google ���������Ʈ�� ���� ID. URL���� ã�� �� ����.
    private readonly string _sheetId = "1_ksfuyQDI4muo28hC8nCtyjwMLohw3oOyGBk8CsqKZE";

    // �����͸� ������ ��Ʈ �̸��� ���� (��: "Sheet1!A1:G7").
    private readonly string _range = "HeroData!A1:G5";

    // Google Cloud Console���� �߱޹��� API Ű. �� Ű�� ���� Google Sheets API�� ������ �� ����.
    private readonly string _apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";

    /// <summary>
    /// �޴� ����
    /// </summary>
    [MenuItem("�ӽ� �޴�/Json -> ScriptableObject")]
    public static void OpenWindow()
    {
        GetWindow<Sample>().Show();
    }

    /// <summary>
    /// Odin ��ư ����. ��ư�� ������ FetchAndConvertWaveData �޼��尡 ����Ǿ� �����Ͱ� ���������� ��ȯ��.
    /// </summary>
    [Button("Fetch and Convert : HeroData", ButtonSizes.Large)]
    public async void FetchAndConvertHeroData()
    {
        // Google Sheets API�� �����͸� ��û�� URL. ���⼭ sheetId, range, apiKey�� ����Ͽ� API ��û�� ���� URL�� �ϼ�.
        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{_sheetId}/values/{_range}?key={_apiKey}";

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
                    Debug.Log(responseBody);

                    // ������� JSON �����͸� ScriptableObject�� ��ȯ�ϴ� �޼��带 ȣ��.
                    CreateHeroDataSO(responseBody);
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
    private void CreateHeroDataSO(string json)
    {
        Debug.Log("1");
        // JSON �����͸� JsonFormat ��ü�� ��ø��������(���ڿ����� ��ü�� ��ȯ)��.
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        Debug.Log("2");
        // ���ο� ScriptableObject�� ����. ���⿡ �Ľ̵� �����͸� ������ ����.
        //WaveDatas waveDataSO = ScriptableObject.CreateInstance<WaveDatas>();
        //HeroData heroData = ScriptableObject.CreateInstance<HeroData>();

        // ScriptableObject�� ����Ƽ �������� ����      
        //string assetPath = $"Assets/Resources/Data/HeroDataSample/HeroDataSample.asset";


        //// JSON�� �����Ϳ��� ���(ù��° �迭)�� ������ ������ �����͸� �Ľ�        
        //waveDataSO.waveDataArr = new WaveData[jsonData.values.Length - 2]; // ù ��°�� �� �迭, �� ��°�� ����̹Ƿ� ����

        // �����
        var headerValues = jsonData.values[1];




        // �Ľ̵� JSON �������� ������ �ϳ��� ScriptableObject�� ä��
        for (int i = 2; i < jsonData.values.Length; i++) // 2��° �ε������� ������ ����
        {
            Debug.Log("3");
            var values = jsonData.values[i]; // ���� ���� �����͸� ������

            Type type = typeof(HeroData);
            HeroData heroData = CreateInstance<HeroData>();

            for (int h = 0; h < headerValues.Length; h++)
            {
                FieldInfo fieldInfo = type.GetField(headerValues[h]);

                if (fieldInfo.FieldType == typeof(int))
                {
                    int data_int = int.Parse(values[h]);
                    fieldInfo.SetValue(heroData, data_int);
                }
                else if (fieldInfo.FieldType == typeof(float))
                {
                    float data_float = float.Parse(values[h]);
                    fieldInfo.SetValue(heroData, data_float);
                }
                else if (fieldInfo.FieldType == typeof(string))
                {
                    fieldInfo.SetValue(heroData, values[h]);
                }

                Debug.Log($" {i} ��°�� ������ �� Id : {heroData.Id}");
                Debug.Log($" {i} ��°�� ������ �� Name : {heroData.Name}");
                Debug.Log($" {i} ��°�� ������ �� MaxHp : {heroData.MaxHp}");
                Debug.Log($" {i} ��°�� ������ �� Atk : {heroData.Atk}");
                Debug.Log($" {i} ��°�� ������ �� Speed : {heroData.Speed}");
                Debug.Log($" {i} ��°�� ������ �� StartSkill : {heroData.StartSkill}");
                Debug.Log($" {i} ��°�� ������ �� Desc : {heroData.Desc}");
            }


            Debug.Log("4");
            //HeroData heroData = CreateInstance<HeroData>();

            //heroData.Id = values[0];
            //heroData.Name = values[1];
            //heroData.MaxHp = float.Parse(values[2]);
            //heroData.Atk = float.Parse(values[3]);
            //heroData.Speed = float.Parse(values[4]);
            //heroData.StartSkill = values[5];
            //heroData.Desc = values[6];

            //Debug.Log($" {i} ��° ������ �� Id : {heroData.Id}");
            //Debug.Log($" {i} ��° ������ �� Name : {heroData.Name}");
            //Debug.Log($" {i} ��° ������ �� MaxHp : {heroData.MaxHp}");
            //Debug.Log($" {i} ��° ������ �� Atk : {heroData.Atk}");
            //Debug.Log($" {i} ��° ������ �� Speed : {heroData.Speed}");
            //Debug.Log($" {i} ��° ������ �� StartSkill : {heroData.StartSkill}");
            //Debug.Log($" {i} ��° ������ �� Desc : {heroData.Desc}");


            //// WaveData ��ü�� �����ϰ� JSON�� ���� �ش� �ʵ忡 �Ҵ�
            //WaveData waveData = new WaveData
            //{
            //    WaveTime = ConvertTimeStringToFloat(values[0]),
            //    MonsterType = values[1].Split(','),
            //    TotalSpawnCount = Array.ConvertAll(values[2].Split(','), int.Parse),
            //    SpawnInterval = Array.ConvertAll(values[3].Split(','), float.Parse),
            //    BossType = values[4]
            //};

            //// ������� WaveData ��ü�� ScriptableObject�� waves�� �߰�
            //waveDataSO.waveDataArr[i - 2] = waveData;
            //heroData = data;


            // ScriptableObject�� ����Ƽ �������� ����
            Debug.Log("5");
            string assetPath = $"Assets/Resources/Data/HeroDataSample/{heroData.Id}_HeroDataSample.asset";
            HeroData existingAsset = AssetDatabase.LoadAssetAtPath<HeroData>(assetPath);

            if (existingAsset == null)
            {
                // ������ ������ ���� ����
                AssetDatabase.CreateAsset(heroData, assetPath);
                Debug.Log($"�� ��ũ���ͺ� ������Ʈ ����! : {assetPath}");
            }
            else
            {
                // ������ �̹� �����ϸ� ���� ������ ����
                existingAsset = heroData;

                // ���� ������ ����Ƽ�� �˸�
                EditorUtility.SetDirty(existingAsset);
                Debug.Log($"���� ��ũ���ͺ� ������Ʈ ������Ʈ! :  {assetPath}");
            }

            // ���� ����
            Debug.Log("6");
            AssetDatabase.SaveAssets();
        }


        Debug.Log("7");
        // ������ ���������� ����Ǿ����� �˸��� �޽��� �ڽ��� ���
        //EditorUtility.DisplayDialog("Success", $"ScriptableObject saved or updated at {assetPath}", "OK");
        EditorUtility.DisplayDialog("Success", $"ScriptableObject saved or updated", "OK");
    }

    /// <summary>
    /// 00:00:00 ���� string �ð� ������ TimeSpan�� ���� float�� ��ȯ
    /// </summary>
    private float ConvertTimeStringToFloat(string timeString)
    {
        if (string.IsNullOrEmpty(timeString))
        {
            return -1.0f;
        }


        if (TimeSpan.TryParse(timeString, out TimeSpan timeSpan))
        {
            return (float)timeSpan.TotalSeconds;
        }
        else
        {
            Debug.LogError($"ConvertTimeStringToFloat ���� : {timeString}");
            return -1.0f;
        }
    }
}


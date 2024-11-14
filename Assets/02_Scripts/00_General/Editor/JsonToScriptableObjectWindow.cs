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
using UnityEngine;

public class JsonToScriptableObjectWindow : OdinEditorWindow
{
    //// Google ���������Ʈ�� ���� ID. URL���� ã�� �� ����.
    //private readonly string _sheetId = "1_ksfuyQDI4muo28hC8nCtyjwMLohw3oOyGBk8CsqKZE";

    //// �����͸� ������ ��Ʈ �̸��� ���� (��: "Sheet1!A1:G7").
    //private readonly string _range = "WaveData!A1:G7";

    //// Google Cloud Console���� �߱޹��� API Ű. �� Ű�� ���� Google Sheets API�� ������ �� ����.
    //private readonly string _apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";

    /////// <summary>
    /////// �޴� ����
    /////// </summary>
    ////[MenuItem("�� �޴�/Json -> ScriptableObject")]
    ////public static void OpenWindow()
    ////{
    ////    GetWindow<JsonToScriptableObjectWindow>().Show();
    ////}

    ///// <summary>
    ///// Odin ��ư ����. ��ư�� ������ FetchAndConvertWaveData �޼��尡 ����Ǿ� �����Ͱ� ���������� ��ȯ��.
    ///// </summary>
    //[Button("Fetch and Convert : WaveData", ButtonSizes.Large)]
    //public async void FetchAndConvertWaveData()
    //{
    //    // Google Sheets API�� �����͸� ��û�� URL. ���⼭ sheetId, range, apiKey�� ����Ͽ� API ��û�� ���� URL�� �ϼ�.
    //    string url = $"https://sheets.googleapis.com/v4/spreadsheets/{_sheetId}/values/{_range}?key={_apiKey}";

    //    // HttpClient�� HTTP ��û�� ������ ������ ���� �� ����ϴ� Ŭ����.
    //    using (HttpClient client = new HttpClient())
    //    {
    //        try
    //        {
    //            // Google Sheets API�� GET ��û�� �񵿱������� �����ϰ�, ������ ������ ������ ��ٸ�.
    //            HttpResponseMessage response = await client.GetAsync(url);

    //            // ���� ������ ���������� ���θ� Ȯ��.
    //            if (response.IsSuccessStatusCode)
    //            {
    //                // ���� ������ ���ڿ��� �о��. ���⿡�� JSON ������ ���������Ʈ �����Ͱ� �������.
    //                string responseBody = await response.Content.ReadAsStringAsync();

    //                // ���� ����� �ֿܼ� ����� Ȯ��.
    //                Debug.Log(responseBody);

    //                // ������� JSON �����͸� ScriptableObject�� ��ȯ�ϴ� �޼��带 ȣ��.
    //                CreateWaveDataSO(responseBody);
    //            }
    //            else
    //            {
    //                // ���� ���� �� ���� �޽����� ���.
    //                Debug.LogError($"Failed to fetch data. Error Status Code: {response.StatusCode}");
    //            }
    //        }
    //        catch (HttpRequestException e)
    //        {
    //            // HTTP ��û �� ���ܰ� �߻��ϸ� ���� �޽����� ���.
    //            Debug.LogError($"Failed to fetch data. Request Error : {e.Message}");
    //        }
    //    }
    //}

    ///// <summary>
    ///// // JSON �����͸� ScriptableObject�� ��ȯ�ϴ� �޼���.
    ///// </summary>
    //private void CreateWaveDataSO(string json)
    //{
    //   
    //    //// JSON �����͸� JsonFormat ��ü�� ��ø��������(���ڿ����� ��ü�� ��ȯ)��.
    //    //var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

    //    //// ���ο� ScriptableObject�� ����. ���⿡ �Ľ̵� �����͸� ������ ����.
    //    //WaveDatas waveDataSO = ScriptableObject.CreateInstance<WaveDatas>();

    //    //// JSON�� �����Ϳ��� ���(ù��° �迭)�� ������ ������ �����͸� �Ľ�        
    //    //waveDataSO.waveDataArr = new WaveData[jsonData.values.Length - 2]; // ù ��°�� �� �迭, �� ��°�� ����̹Ƿ� ����

    //    //// �Ľ̵� JSON �������� ������ �ϳ��� ScriptableObject�� ä��
    //    //for (int i = 2; i < jsonData.values.Length; i++) // 2��° �ε������� ������ ����
    //    //{
    //    //    var values = jsonData.values[i]; // ���� ���� �����͸� ������

    //    //    // WaveData ��ü�� �����ϰ� JSON�� ���� �ش� �ʵ忡 �Ҵ�
    //    //    WaveData waveData = new WaveData
    //    //    {
    //    //        //WaveTime = ConvertTimeStringToFloat(values[0]),                             
    //    //        WaveTime = values[0],                             
    //    //        MonsterType = values[1].Split(','),                                         
    //    //        TotalSpawnCount = Array.ConvertAll(values[2].Split(','), int.Parse),        
    //    //        SpawnInterval = Array.ConvertAll(values[3].Split(','), float.Parse),        
    //    //        BossType = values[4]
    //    //    };

    //    //    // ������� WaveData ��ü�� ScriptableObject�� waves�� �߰�
    //    //    waveDataSO.waveDataArr[i - 2] = waveData;
    //    //}

    //    //// ScriptableObject�� ����Ƽ �������� ����      
    //    //string assetPath = $"Assets/Resources/Data/Wave/WaveDatas.asset";
    //    //WaveDatas existingAsset = AssetDatabase.LoadAssetAtPath<WaveDatas>(assetPath);

    //    //if (existingAsset == null)
    //    //{
    //    //    // ������ ������ ���� ����
    //    //    AssetDatabase.CreateAsset(waveDataSO, assetPath);
    //    //    Debug.Log($"�� ��ũ���ͺ� ������Ʈ ����! : {assetPath}");
    //    //}
    //    //else
    //    //{
    //    //    // ������ �̹� �����ϸ� ���� ������ ����
    //    //    existingAsset.waveDataArr = waveDataSO.waveDataArr;

    //    //    // ���� ������ ����Ƽ�� �˸�
    //    //    EditorUtility.SetDirty(existingAsset);
    //    //    Debug.Log($"���� ��ũ���ͺ� ������Ʈ ������Ʈ! :  {assetPath}");
    //    //}

    //    //// ���� ����
    //    //AssetDatabase.SaveAssets();

    //    //// ������ ���������� ����Ǿ����� �˸��� �޽��� �ڽ��� ���
    //    //EditorUtility.DisplayDialog("Success", $"ScriptableObject saved or updated at {assetPath}", "OK");
    //}

    
}

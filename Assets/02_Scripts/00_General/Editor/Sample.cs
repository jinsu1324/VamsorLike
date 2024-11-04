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

public class Sample : OdinEditorWindow
{
    // Google 스프레드시트의 고유 ID. URL에서 찾을 수 있음.
    private readonly string _sheetId = "1_ksfuyQDI4muo28hC8nCtyjwMLohw3oOyGBk8CsqKZE";

    // Google Cloud Console에서 발급받은 API 키. 이 키를 통해 Google Sheets API에 접근할 수 있음.
    private readonly string _apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";


    // HeroData
    // 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    private readonly string _range_HeroData = "HeroData!A1:G5";
    // 데이터를 저장할 경로와 이름
    private readonly string _fileName_HeroData = "HeroDatas";


    // LevelData
    // 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    private readonly string _range_LevelData = "LevelData!A1:C13";
    // 데이터를 저장할 경로와 이름
    private readonly string _fileName_LevelData = "LevelDatas";


    // WaveData
    // 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    private readonly string _range_WaveData = "WaveData!A1:F7";
    // 데이터를 저장할 경로와 이름
    private readonly string _fileName_WaveData = "WaveDatas";


    // MonsterData
    // 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    private readonly string _range_MonsterData = "MonsterData!A1:E6";
    // 데이터를 저장할 경로와 이름
    private readonly string _fileName_MonsterData = "MonsterDatas";
      

    // BossData
    // 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    private readonly string _range_BossData = "BossData!A1:M3";
    // 데이터를 저장할 경로와 이름
    private readonly string _fileName_BossData = "BossDatas";


    //// SkillData_SlashAttack
    //// 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    //private readonly string _range_SkillData_SlashAttack = "SkillData_SlashAttack!A1:F5";
    //// 데이터를 저장할 경로와 이름
    //private readonly string _fileName_SkillData_SlashAttack = "SkillDatas_SlashAttack";


    //// SkillData_Boomerang
    //// 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    //private readonly string _range_SkillData_Boomerang = "SkillData_Boomerang!A1:H5";
    //// 데이터를 저장할 경로와 이름
    //private readonly string _fileName_SkillData_Boomerang = "SkillDatas_Boomerang";


    //// SkillData_Sniper
    //// 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    //private readonly string _range_SkillData_Sniper = "SkillData_Sniper!A1:H5";
    //// 데이터를 저장할 경로와 이름
    //private readonly string _fileName_SkillData_Sniper = "SkillDatas_Sniper";








    // SkillData
    // 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    private readonly string _range_SkillData = "SkillData!A1:I11";
    // 데이터를 저장할 경로와 이름
    private readonly string _fileName_SkillData = "SkillDatas";







    /// <summary>
    /// 메뉴 생성
    /// </summary>
    [MenuItem("진수/데이터 최신화")]
    public static void OpenWindow()
    {
        GetWindow<Sample>().Show();
    }

    [Button("데이터 최신화 하기!", ButtonSizes.Large)]
    public void MyButton()
    {
        FetchAndConvertData<HeroData, HeroDatas>(_range_HeroData, _fileName_HeroData);
        FetchAndConvertData<LevelData, LevelDatas>(_range_LevelData, _fileName_LevelData);
        FetchAndConvertData<WaveData, WaveDatas>(_range_WaveData, _fileName_WaveData);
        FetchAndConvertData<MonsterData, MonsterDatas>(_range_MonsterData, _fileName_MonsterData);
        FetchAndConvertData<BossData, BossDatas>(_range_BossData, _fileName_BossData);
        FetchAndConvertData<SkillData, SkillDatas>(_range_SkillData, _fileName_SkillData);
        

        
        // FetchAndConvertData<SkillData_SlashAttack, SkillDatas_SlashAttack>(_range_SkillData_SlashAttack, _fileName_SkillData_SlashAttack);
        // FetchAndConvertData<SkillData_Boomerang, SkillDatas_Boomerang>(_range_SkillData_Boomerang, _fileName_SkillData_Boomerang);
        // FetchAndConvertData<SkillData_Sniper, SkillDatas_Sniper>(_range_SkillData_Sniper, _fileName_SkillData_Sniper);
    }

    /// <summary>
    /// 데이터가 가져오고 스크립터블 오브젝트로 변환
    /// </summary>
    public async void FetchAndConvertData<Data, DatasSO>(string range, string fileName) where Data : BaseData, new() where DatasSO : DataListSO<Data>
    {
        // Google Sheets API에 데이터를 요청할 URL. 여기서 sheetId, range, apiKey를 사용하여 API 요청을 보낼 URL을 완성.
        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{_sheetId}/values/{range}?key={_apiKey}";

        // HttpClient는 HTTP 요청을 보내고 응답을 받을 때 사용하는 클래스.
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Google Sheets API로 GET 요청을 비동기적으로 전송하고, 서버가 응답할 때까지 기다림.
                HttpResponseMessage response = await client.GetAsync(url);

                // 서버 응답이 성공적인지 여부를 확인.
                if (response.IsSuccessStatusCode)
                {
                    // 응답 본문을 문자열로 읽어옴. 여기에는 JSON 형식의 스프레드시트 데이터가 들어있음.
                    string responseBody = await response.Content.ReadAsStringAsync();

                    // 응답 결과를 콘솔에 출력해 확인.
                    Debug.Log(responseBody);

                    // 응답받은 JSON 데이터를 ScriptableObject로 변환하는 메서드를 호출.
                    CreateDataListSO<Data, DatasSO>(responseBody, fileName);
                }
                else
                {
                    // 응답 실패 시 에러 메시지를 출력.
                    Debug.LogError($"Failed to fetch data. Error Status Code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                // HTTP 요청 중 예외가 발생하면 에러 메시지를 출력.
                Debug.LogError($"Failed to fetch data. Request Error : {e.Message}");
            }
        }
    }

    /// <summary>
    /// // JSON 데이터를 ScriptableObject로 변환하는 메서드.
    /// </summary>
    private void CreateDataListSO<Data, DatasSO>(string json, string fileName) where Data : BaseData, new() where DatasSO : DataListSO<Data>
    {
        // JSON 데이터를 JsonFormat 객체로 디시리얼라이즈(문자열에서 객체로 변환)함.
        var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

        // 새로운 ScriptableObject를 생성. 여기에 파싱된 데이터를 저장할 것임.
        DatasSO datasSO = CreateInstance<DatasSO>();
        datasSO.DataList = new List<Data>();

        // 헤더들
        var headerValues = jsonData.values[1];

        // 파싱된 JSON 데이터의 값들을 하나씩 ScriptableObject에 채움 (2번째 인덱스부터 데이터 시작)
        for (int i = 2; i < jsonData.values.Length; i++) 
        {
            // 현재 행의 데이터를 가져옴
            var values = jsonData.values[i];

            // 리플렉션 준비
            Type type = typeof(Data);
            Data data = new Data();

            // 헤더와 데이터를 하나씩 매핑
            for (int h = 0; h < headerValues.Length; h++)
            {
                FieldInfo fieldInfo = type.GetField(headerValues[h]);

                // 셀이 비어있을 경우 그냥 지나가고 다음으로 (-로 테이블에 표시해둠)
                if (values[h] == "-")
                    continue;

                // 리플렉션으로 변수에 값 할당
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

            // 데이터테이블에 있던 값들이 변수에 다 들어간 data를 -> datasSO DataList에 추가
            datasSO.DataList.Add(data);

            // 변경 사항을 유니티에 알림
            EditorUtility.SetDirty(datasSO);
        }

        // ScriptableObject을 유니티 에셋으로 저장
        string path = $"Assets/Resources/Data/{fileName}.asset";
        DatasSO existingAsset = AssetDatabase.LoadAssetAtPath<DatasSO>(fileName);

        if (existingAsset == null)
        {
            // 에셋이 없으면 새로 생성
            AssetDatabase.CreateAsset(datasSO, path);

            // 변경 사항을 유니티에 알림
            EditorUtility.SetDirty(datasSO);
        }
        else
        {
            // 에셋이 이미 존재하면 기존 에셋을 갱신
            existingAsset = datasSO;

            // 변경 사항을 유니티에 알림
            EditorUtility.SetDirty(existingAsset);
        }

        // 에셋 저장
        AssetDatabase.SaveAssets();

        // 에셋이 성공적으로 저장되었음을 알리는 메시지 박스를 띄움
        EditorUtility.DisplayDialog("Success", $"ScriptableObject saved or updated at {path}", "OK");
    }
}


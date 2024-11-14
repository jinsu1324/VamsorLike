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
    //// Google 스프레드시트의 고유 ID. URL에서 찾을 수 있음.
    //private readonly string _sheetId = "1_ksfuyQDI4muo28hC8nCtyjwMLohw3oOyGBk8CsqKZE";

    //// 데이터를 가져올 시트 이름과 범위 (예: "Sheet1!A1:G7").
    //private readonly string _range = "WaveData!A1:G7";

    //// Google Cloud Console에서 발급받은 API 키. 이 키를 통해 Google Sheets API에 접근할 수 있음.
    //private readonly string _apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";

    /////// <summary>
    /////// 메뉴 생성
    /////// </summary>
    ////[MenuItem("내 메뉴/Json -> ScriptableObject")]
    ////public static void OpenWindow()
    ////{
    ////    GetWindow<JsonToScriptableObjectWindow>().Show();
    ////}

    ///// <summary>
    ///// Odin 버튼 생성. 버튼을 누르면 FetchAndConvertWaveData 메서드가 실행되어 데이터가 가져와지고 변환됨.
    ///// </summary>
    //[Button("Fetch and Convert : WaveData", ButtonSizes.Large)]
    //public async void FetchAndConvertWaveData()
    //{
    //    // Google Sheets API에 데이터를 요청할 URL. 여기서 sheetId, range, apiKey를 사용하여 API 요청을 보낼 URL을 완성.
    //    string url = $"https://sheets.googleapis.com/v4/spreadsheets/{_sheetId}/values/{_range}?key={_apiKey}";

    //    // HttpClient는 HTTP 요청을 보내고 응답을 받을 때 사용하는 클래스.
    //    using (HttpClient client = new HttpClient())
    //    {
    //        try
    //        {
    //            // Google Sheets API로 GET 요청을 비동기적으로 전송하고, 서버가 응답할 때까지 기다림.
    //            HttpResponseMessage response = await client.GetAsync(url);

    //            // 서버 응답이 성공적인지 여부를 확인.
    //            if (response.IsSuccessStatusCode)
    //            {
    //                // 응답 본문을 문자열로 읽어옴. 여기에는 JSON 형식의 스프레드시트 데이터가 들어있음.
    //                string responseBody = await response.Content.ReadAsStringAsync();

    //                // 응답 결과를 콘솔에 출력해 확인.
    //                Debug.Log(responseBody);

    //                // 응답받은 JSON 데이터를 ScriptableObject로 변환하는 메서드를 호출.
    //                CreateWaveDataSO(responseBody);
    //            }
    //            else
    //            {
    //                // 응답 실패 시 에러 메시지를 출력.
    //                Debug.LogError($"Failed to fetch data. Error Status Code: {response.StatusCode}");
    //            }
    //        }
    //        catch (HttpRequestException e)
    //        {
    //            // HTTP 요청 중 예외가 발생하면 에러 메시지를 출력.
    //            Debug.LogError($"Failed to fetch data. Request Error : {e.Message}");
    //        }
    //    }
    //}

    ///// <summary>
    ///// // JSON 데이터를 ScriptableObject로 변환하는 메서드.
    ///// </summary>
    //private void CreateWaveDataSO(string json)
    //{
    //   
    //    //// JSON 데이터를 JsonFormat 객체로 디시리얼라이즈(문자열에서 객체로 변환)함.
    //    //var jsonData = JsonConvert.DeserializeObject<JsonFormat>(json);

    //    //// 새로운 ScriptableObject를 생성. 여기에 파싱된 데이터를 저장할 것임.
    //    //WaveDatas waveDataSO = ScriptableObject.CreateInstance<WaveDatas>();

    //    //// JSON의 데이터에서 헤더(첫번째 배열)를 제외한 나머지 데이터를 파싱        
    //    //waveDataSO.waveDataArr = new WaveData[jsonData.values.Length - 2]; // 첫 번째는 빈 배열, 두 번째는 헤더이므로 무시

    //    //// 파싱된 JSON 데이터의 값들을 하나씩 ScriptableObject에 채움
    //    //for (int i = 2; i < jsonData.values.Length; i++) // 2번째 인덱스부터 데이터 시작
    //    //{
    //    //    var values = jsonData.values[i]; // 현재 행의 데이터를 가져옴

    //    //    // WaveData 객체를 생성하고 JSON의 값을 해당 필드에 할당
    //    //    WaveData waveData = new WaveData
    //    //    {
    //    //        //WaveTime = ConvertTimeStringToFloat(values[0]),                             
    //    //        WaveTime = values[0],                             
    //    //        MonsterType = values[1].Split(','),                                         
    //    //        TotalSpawnCount = Array.ConvertAll(values[2].Split(','), int.Parse),        
    //    //        SpawnInterval = Array.ConvertAll(values[3].Split(','), float.Parse),        
    //    //        BossType = values[4]
    //    //    };

    //    //    // 만들어진 WaveData 객체를 ScriptableObject의 waves에 추가
    //    //    waveDataSO.waveDataArr[i - 2] = waveData;
    //    //}

    //    //// ScriptableObject을 유니티 에셋으로 저장      
    //    //string assetPath = $"Assets/Resources/Data/Wave/WaveDatas.asset";
    //    //WaveDatas existingAsset = AssetDatabase.LoadAssetAtPath<WaveDatas>(assetPath);

    //    //if (existingAsset == null)
    //    //{
    //    //    // 에셋이 없으면 새로 생성
    //    //    AssetDatabase.CreateAsset(waveDataSO, assetPath);
    //    //    Debug.Log($"새 스크립터블 오브젝트 생성! : {assetPath}");
    //    //}
    //    //else
    //    //{
    //    //    // 에셋이 이미 존재하면 기존 에셋을 갱신
    //    //    existingAsset.waveDataArr = waveDataSO.waveDataArr;

    //    //    // 변경 사항을 유니티에 알림
    //    //    EditorUtility.SetDirty(existingAsset);
    //    //    Debug.Log($"기존 스크립터블 오브젝트 업데이트! :  {assetPath}");
    //    //}

    //    //// 에셋 저장
    //    //AssetDatabase.SaveAssets();

    //    //// 에셋이 성공적으로 저장되었음을 알리는 메시지 박스를 띄움
    //    //EditorUtility.DisplayDialog("Success", $"ScriptableObject saved or updated at {assetPath}", "OK");
    //}

    
}

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEditor;
using UnityEngine;

public class Test_SpreadSheetEditorWindow : OdinEditorWindow
{
    private string sheetId = "1_ksfuyQDI4muo28hC8nCtyjwMLohw3oOyGBk8CsqKZE";// Google SpreadSheet의 고유 ID
    private string range = "MonsterData!A1:E6";                             // Google SpreadSheet의 시트 이름과 데이터 범위
    private string apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";      // Google Cloud Console에서 발급받은 API 키


    /// <summary>
    /// 메뉴 생성
    /// </summary>
    [MenuItem("내 메뉴/스프레드시트 테스트")]
    public static void OpenWindow()
    {
        GetWindow<Test_SpreadSheetEditorWindow>().Show();
    }


    [Button("Fetch Data", ButtonSizes.Large)]
    public async void FetchSheetData()
    {
        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{sheetId}/values/{range}?key={apiKey}";

        using (HttpClient client = new HttpClient()) // HttpClient 객체 생성 (이 객체를 사용해 인터넷을 통해 Google Sheets API에 요청)
        {
            try // 서버로 데이터를 요청하는 코드
            {
                HttpResponseMessage response = await client.GetAsync(url); // GET 요청 보내기 (비동기적이며, 서버가 응답할 때까지 기다림)
                if (response.IsSuccessStatusCode) // 요청이 성공했는지 확인
                {
                    string responseBody = await response.Content.ReadAsStringAsync(); // 서버로부터 받은 데이터를 문자열로 반환
                    Debug.Log(responseBody);
                }
                else
                {
                    Debug.LogError($"Failed to fetch data. Error Status Code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e) // HTTP 요청 중 오류가 발생한 경우 실행
            {
                Debug.LogError($"Failed to fetch data. Request Error : {e.Message}");
            }
        }
    }
}

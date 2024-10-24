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

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Debug.Log(responseBody); // 에디터 콘솔에 데이터를 출력
                }
                else
                {
                    Debug.LogError($"Failed to fetch data. Error Status Code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Debug.LogError($"Failed to fetch data. Request Error : {e.Message}");
            }
        }
    }
}

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEditor;
using UnityEngine;

public class Test_SpreadSheetEditorWindow : OdinEditorWindow
{
    private string sheetId = "1_ksfuyQDI4muo28hC8nCtyjwMLohw3oOyGBk8CsqKZE";// Google SpreadSheet�� ���� ID
    private string range = "MonsterData!A1:E6";                             // Google SpreadSheet�� ��Ʈ �̸��� ������ ����
    private string apiKey = "AIzaSyATyhPBwN65Vbkg9ppq6NBOo3nLwHuqkJU";      // Google Cloud Console���� �߱޹��� API Ű


    /// <summary>
    /// �޴� ����
    /// </summary>
    [MenuItem("�� �޴�/���������Ʈ �׽�Ʈ")]
    public static void OpenWindow()
    {
        GetWindow<Test_SpreadSheetEditorWindow>().Show();
    }


    [Button("Fetch Data", ButtonSizes.Large)]
    public async void FetchSheetData()
    {
        string url = $"https://sheets.googleapis.com/v4/spreadsheets/{sheetId}/values/{range}?key={apiKey}";

        using (HttpClient client = new HttpClient()) // HttpClient ��ü ���� (�� ��ü�� ����� ���ͳ��� ���� Google Sheets API�� ��û)
        {
            try // ������ �����͸� ��û�ϴ� �ڵ�
            {
                HttpResponseMessage response = await client.GetAsync(url); // GET ��û ������ (�񵿱����̸�, ������ ������ ������ ��ٸ�)
                if (response.IsSuccessStatusCode) // ��û�� �����ߴ��� Ȯ��
                {
                    string responseBody = await response.Content.ReadAsStringAsync(); // �����κ��� ���� �����͸� ���ڿ��� ��ȯ
                    Debug.Log(responseBody);
                }
                else
                {
                    Debug.LogError($"Failed to fetch data. Error Status Code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e) // HTTP ��û �� ������ �߻��� ��� ����
            {
                Debug.LogError($"Failed to fetch data. Request Error : {e.Message}");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    public static string LoadSceneName;         // �̵��� �� �̸��� ����

    /// <summary>
    /// �̵��� �� �̸� �Ҵ��ϰ� �ε������� �̵�
    /// </summary>
    public static void LoadScene(string sceneName)
    {
        LoadSceneName = sceneName;
        SceneManager.LoadScene("02_LoadingScene"); // �ε������� �̵�
    }
}

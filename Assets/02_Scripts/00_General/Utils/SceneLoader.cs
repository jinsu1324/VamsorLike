using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    public static string LoadSceneName;         // 이동할 씬 이름을 저장

    /// <summary>
    /// 이동할 씬 이름 할당하고 로딩씬으로 이동
    /// </summary>
    public static void LoadScene(string sceneName)
    {
        LoadSceneName = sceneName;
        SceneManager.LoadScene("02_LoadingScene"); // 로딩씬으로 이동
    }
}

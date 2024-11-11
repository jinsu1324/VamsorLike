using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopup : MonoBehaviour
{
    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public void OpenPopup()
    {
        PlaySceneManager.Instance.IsGameStartChange(false);

        gameObject.SetActive(true);

    }

    /// <summary>
    /// 팝업 끄기
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// 게임으로 돌아가기 버튼 클릭 시 호출
    /// </summary>
    public void OnClickResumeButton()
    {
        PlaySceneManager.Instance.IsGameStartChange(true);
        ClosePopup();
    }

    /// <summary>
    /// 로비로 버튼 클릭 시 호출
    /// </summary>
    public void OnClickLobbyButton()
    {
        SceneLoader.LoadScene("01_LobbyScene");
    }
}

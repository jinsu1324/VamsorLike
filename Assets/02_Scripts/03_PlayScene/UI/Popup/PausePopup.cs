using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopup : MonoBehaviour
{
    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public void OpenPopup()
    {
        PlaySceneManager.Instance.IsGameStartChange(false);

        gameObject.SetActive(true);

    }

    /// <summary>
    /// �˾� ����
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }


    /// <summary>
    /// �������� ���ư��� ��ư Ŭ�� �� ȣ��
    /// </summary>
    public void OnClickResumeButton()
    {
        PlaySceneManager.Instance.IsGameStartChange(true);
        ClosePopup();
    }

    /// <summary>
    /// �κ�� ��ư Ŭ�� �� ȣ��
    /// </summary>
    public void OnClickLobbyButton()
    {
        SceneLoader.LoadScene("01_LobbyScene");
    }
}

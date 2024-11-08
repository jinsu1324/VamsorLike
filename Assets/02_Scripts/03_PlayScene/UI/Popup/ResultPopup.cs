using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : SerializedMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _killCountText;             // ų ī��Ʈ
    [SerializeField]
    private TextMeshProUGUI _goldText;                  // ȹ���� ���
    [SerializeField]
    private TextMeshProUGUI _playTimeText;              // �÷��� �ð�

    [SerializeField]
    private Button _lobbyButton;                        // �κ� ��ư    


    /// <summary>
    /// �˾�â ������ �ʱ�ȭ
    /// </summary>
    private void InfoInitialize()
    {
        _killCountText.text = PlaySceneCanvas.Instance.PlayAchivementUI.KillCount.ToString();
        _goldText.text = PlaySceneCanvas.Instance.PlayAchivementUI.Gold.ToString();
        _playTimeText.text = PlaySceneCanvas.Instance.PlayTimeUI.PlayTimeText.text;
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public void OpenPopup()
    {
        PlaySceneManager.Instance.IsGameStartChange(false);

        InfoInitialize();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// �˾� �ݱ�
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �κ��ư Ŭ�� �� 
    /// </summary>
    public void OnClickLobbyButton()
    {
        SceneLoader.LoadScene("01_LobbyScene");
    }
}

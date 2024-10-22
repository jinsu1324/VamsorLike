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
    private TextMeshProUGUI _earnedGoldText;            // ȹ���� ���
    [SerializeField]
    private TextMeshProUGUI _playTimeText;              // �÷��� �ð�

    [SerializeField]
    private Button _lobbyButton;                        // �κ� ��ư    


    /// <summary>
    /// �˾�â ������ �ʱ�ȭ
    /// </summary>
    private void InfoInitialize()
    {
        _killCountText.text = PlaySceneManager.Instance.PlayAchivement.TotalKillCount.ToString();
        _earnedGoldText.text = PlaySceneManager.Instance.PlayAchivement.TotalGold.ToString();
        _playTimeText.text = PlaySceneManager.Instance.PlaySceneCanvas.PlayTimeUI.PlayTimeText.text;
    }

    /// <summary>
    /// �˾� �ѱ�
    /// </summary>
    public void OpenPopup()
    {
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
        SceneSwitcher.LoadScene("01_LobbyScene");
    }
}

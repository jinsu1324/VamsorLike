using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : SerializedMonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _killCountText;             // 킬 카운트
    [SerializeField]
    private TextMeshProUGUI _goldText;                  // 획득한 골드
    [SerializeField]
    private TextMeshProUGUI _playTimeText;              // 플레이 시간

    [SerializeField]
    private Button _lobbyButton;                        // 로비 버튼    


    /// <summary>
    /// 팝업창 정보들 초기화
    /// </summary>
    private void InfoInitialize()
    {
        _killCountText.text = PlaySceneCanvas.Instance.PlayAchivementUI.KillCount.ToString();
        _goldText.text = PlaySceneCanvas.Instance.PlayAchivementUI.Gold.ToString();
        _playTimeText.text = PlaySceneCanvas.Instance.PlayTimeUI.PlayTimeText.text;
    }

    /// <summary>
    /// 팝업 켜기
    /// </summary>
    public void OpenPopup()
    {
        PlaySceneManager.Instance.IsGameStartChange(false);

        InfoInitialize();
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 로비버튼 클릭 시 
    /// </summary>
    public void OnClickLobbyButton()
    {
        SceneLoader.LoadScene("01_LobbyScene");
    }
}

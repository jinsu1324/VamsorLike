using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAchivementUI : SerializedMonoBehaviour
{
    [SerializeField]
    public MoveIconManager MoveIconManager { get; set; }    // 아이콘 움직이게 할 매니저

    [SerializeField]
    private TextMeshProUGUI _killCountText;         // 킬 수 텍스트
    [SerializeField]
    private TextMeshProUGUI _goldText;              // 획득한 골드 텍스트

    public int KillCount { get; set; }              // 킬 수
    public int Gold { get; set; }                   // 획득한 골드

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        KillCount = 0;
        Gold = 0;
        UIRefresh();
    }

    /// <summary>
    /// 킬수 증가
    /// </summary>
    public void AddKillCount()
    {
        KillCount++;
        UIRefresh();

        if (KillCount % 100 == 0)
        {
            PlaySceneCanvas.Instance.ToastMessageUI.Initialize(KillCount);
            AddGold(10);
        }
    }

    /// <summary>
    /// 획득한 골드 증가
    /// </summary>
    public void AddGold(int amount)
    {
        Gold += amount;
        UIRefresh();
    }

    /// <summary>
    /// UI 업데이트
    /// </summary>
    private void UIRefresh()
    {
        _killCountText.text = KillCount.ToString();
        _goldText.text = Gold.ToString();
    }
}

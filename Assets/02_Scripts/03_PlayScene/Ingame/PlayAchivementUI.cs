using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAchivementUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _killCountText;         // ų �� �ؽ�Ʈ
    [SerializeField]
    private TextMeshProUGUI _goldText;              // ȹ���� ��� �ؽ�Ʈ

    public int KillCount { get; set; }                // ų ��
    public int Gold { get; set; }                     // ȹ���� ���

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        KillCount = 0;
        Gold = 0;
        UIRefresh();
    }

    /// <summary>
    /// ų�� ����
    /// </summary>
    public void AddKillCount()
    {
        KillCount++;
        UIRefresh();
    }

    /// <summary>
    /// ȹ���� ��� ����
    /// </summary>
    public void AddGold(int amount)
    {
        Gold += amount;
        UIRefresh();
    }

    /// <summary>
    /// UI ������Ʈ
    /// </summary>
    private void UIRefresh()
    {
        _killCountText.text = KillCount.ToString();
        _goldText.text = Gold.ToString();
    }
}

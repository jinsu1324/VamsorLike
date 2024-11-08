using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAchivementUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _killCountText;         // Å³ ¼ö ÅØ½ºÆ®
    [SerializeField]
    private TextMeshProUGUI _goldText;              // È¹µæÇÑ °ñµå ÅØ½ºÆ®

    public int KillCount { get; set; }                // Å³ ¼ö
    public int Gold { get; set; }                     // È¹µæÇÑ °ñµå

    /// <summary>
    /// Start ÇÔ¼ö
    /// </summary>
    private void Start()
    {
        KillCount = 0;
        Gold = 0;
        UIRefresh();
    }

    /// <summary>
    /// Å³¼ö Áõ°¡
    /// </summary>
    public void AddKillCount()
    {
        KillCount++;
        UIRefresh();
    }

    /// <summary>
    /// È¹µæÇÑ °ñµå Áõ°¡
    /// </summary>
    public void AddGold(int amount)
    {
        Gold += amount;
        UIRefresh();
    }

    /// <summary>
    /// UI ¾÷µ¥ÀÌÆ®
    /// </summary>
    private void UIRefresh()
    {
        _killCountText.text = KillCount.ToString();
        _goldText.text = Gold.ToString();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    public int TotalKillCount { get; set; }                // ≈Î«’ ≈≥ ºˆ
    public int TotalGold { get; set; }                     // »πµÊ«— ∞ÒµÂ

    /// <summary>
    /// Start «‘ºˆ
    /// </summary>
    private void Start()
    {
        TotalKillCount = 0;
        TotalGold = 0;
    }

    /// <summary>
    /// ≈Î«’ ≈≥ºˆ ¡ı∞°
    /// </summary>
    public void AddKillCount()
    {
        TotalKillCount++;
    }

    /// <summary>
    /// »πµÊ«— ∞ÒµÂ ¡ı∞°
    /// </summary>
    public void AddGold(int amount)
    {
        TotalGold += amount;
    }
}

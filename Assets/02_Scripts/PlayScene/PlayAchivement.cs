using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAchivement : MonoBehaviour
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

        Debug.Log($"TotalKillCount : {TotalKillCount}");
        Debug.Log($"TotalGold : {TotalGold}");

        MonsterObject.OnMonsterDeath += AddKillCount;
        GoldObject.OnGetGold += AddGold;
    }

    /// <summary>
    /// ≈Î«’ ≈≥ºˆ ¡ı∞°
    /// </summary>
    public void AddKillCount(MonsterObject monsterObject)
    {
        TotalKillCount++;
        Debug.Log($"TotalKillCount : {TotalKillCount}");
    }

    /// <summary>
    /// »πµÊ«— ∞ÒµÂ ¡ı∞°
    /// </summary>
    public void AddGold()
    {
        TotalGold++;
        Debug.Log($"TotalGold : {TotalGold}");
    }

}

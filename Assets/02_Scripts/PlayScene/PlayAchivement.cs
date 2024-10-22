using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAchivement : MonoBehaviour
{
    private int _totalKillCount = 0;                // ≈Î«’ ≈≥ ºˆ
    private int _totalGold = 0;                     // »πµÊ«— ∞ÒµÂ

    /// <summary>
    /// Start «‘ºˆ
    /// </summary>
    private void Start()
    {
        MonsterObject.OnMonsterDeath += AddKillCount;
        GoldObject.OnGetGold += AddGold;
    }

    /// <summary>
    /// ≈Î«’ ≈≥ºˆ ¡ı∞°
    /// </summary>
    public void AddKillCount(MonsterObject monsterObject)
    {
        _totalKillCount++;
        Debug.Log($"TotalKillCount : {_totalKillCount}");
    }

    /// <summary>
    /// »πµÊ«— ∞ÒµÂ ¡ı∞°
    /// </summary>
    public void AddGold()
    {
        _totalGold++;
        Debug.Log($"TotalGold : {_totalGold}");
    }

}

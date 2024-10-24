using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAchivement : MonoBehaviour
{
    public int TotalKillCount { get; set; }                // 통합 킬 수
    public int TotalGold { get; set; }                     // 획득한 골드

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        TotalKillCount = 0;
        TotalGold = 0;

        MonsterObjectBase.OnMonsterDeath += AddKillCount;
        GoldObject.OnGetGold += AddGold;
    }

    /// <summary>
    /// 통합 킬수 증가
    /// </summary>
    public void AddKillCount(MonsterObjectBase monster)
    {
        TotalKillCount++;
    }

    /// <summary>
    /// 획득한 골드 증가
    /// </summary>
    public void AddGold()
    {
        TotalGold++;
    }

    /// <summary>
    /// 씬 전환되거나 오브젝트 파괴될 때 이벤트 제거
    /// </summary>
    public void OnDisable()
    {
        MonsterObjectBase.OnMonsterDeath -= AddKillCount;
        GoldObject.OnGetGold -= AddGold;
    }

}

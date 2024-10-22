using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAchivement : MonoBehaviour
{
    public int TotalKillCount { get; set; }                // ���� ų ��
    public int TotalGold { get; set; }                     // ȹ���� ���

    /// <summary>
    /// Start �Լ�
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
    /// ���� ų�� ����
    /// </summary>
    public void AddKillCount(MonsterObject monsterObject)
    {
        TotalKillCount++;
        Debug.Log($"TotalKillCount : {TotalKillCount}");
    }

    /// <summary>
    /// ȹ���� ��� ����
    /// </summary>
    public void AddGold()
    {
        TotalGold++;
        Debug.Log($"TotalGold : {TotalGold}");
    }

}

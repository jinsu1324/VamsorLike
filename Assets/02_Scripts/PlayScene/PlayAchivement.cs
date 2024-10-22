using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayAchivement : MonoBehaviour
{
    private int _totalKillCount = 0;                // ���� ų ��
    private int _totalGold = 0;                     // ȹ���� ���

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        MonsterObject.OnMonsterDeath += AddKillCount;
        GoldObject.OnGetGold += AddGold;
    }

    /// <summary>
    /// ���� ų�� ����
    /// </summary>
    public void AddKillCount(MonsterObject monsterObject)
    {
        _totalKillCount++;
        Debug.Log($"TotalKillCount : {_totalKillCount}");
    }

    /// <summary>
    /// ȹ���� ��� ����
    /// </summary>
    public void AddGold()
    {
        _totalGold++;
        Debug.Log($"TotalGold : {_totalGold}");
    }

}

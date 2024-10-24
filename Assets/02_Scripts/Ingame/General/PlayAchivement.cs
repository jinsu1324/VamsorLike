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

        MonsterObjectBase.OnMonsterDeath += AddKillCount;
        GoldObject.OnGetGold += AddGold;
    }

    /// <summary>
    /// ���� ų�� ����
    /// </summary>
    public void AddKillCount(MonsterObjectBase monster)
    {
        TotalKillCount++;
    }

    /// <summary>
    /// ȹ���� ��� ����
    /// </summary>
    public void AddGold()
    {
        TotalGold++;
    }

    /// <summary>
    /// �� ��ȯ�ǰų� ������Ʈ �ı��� �� �̺�Ʈ ����
    /// </summary>
    public void OnDisable()
    {
        MonsterObjectBase.OnMonsterDeath -= AddKillCount;
        GoldObject.OnGetGold -= AddGold;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AchivementManager : MonoBehaviour
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
    }

    /// <summary>
    /// ���� ų�� ����
    /// </summary>
    public void AddKillCount()
    {
        TotalKillCount++;
    }

    /// <summary>
    /// ȹ���� ��� ����
    /// </summary>
    public void AddGold(int amount)
    {
        TotalGold += amount;
    }
}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : SerializedMonoBehaviour
{
    private int _earnedGold = 0;                       // �÷��̿��� ȹ���� ���

    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        RefreshGoldInvenUIText();
    }

    /// <summary>
    /// ��� ȹ������ �� ó��
    /// </summary>
    public void GoldUp(int amount)
    {
        _earnedGold += amount;
        RefreshGoldInvenUIText();
    }

    /// <summary>
    /// ��� �κ��丮 UI �ؽ�Ʈ ���ΰ�ħ
    /// </summary>
    public void RefreshGoldInvenUIText()
    {
        PlaySceneCanvas.Instance.GoldInvenUI.RefreshGoldText(_earnedGold);
    }
}

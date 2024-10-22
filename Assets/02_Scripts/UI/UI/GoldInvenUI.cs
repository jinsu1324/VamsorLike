using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldInvenUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _goldText;          // ȹ���� ��� �ؽ�Ʈ

    /// <summary>
    /// ��� �ؽ�Ʈ ���ΰ�ħ
    /// </summary>
    public void RefreshGoldText(int earnedGold)
    {
        _goldText.text = earnedGold.ToString();
    }
}

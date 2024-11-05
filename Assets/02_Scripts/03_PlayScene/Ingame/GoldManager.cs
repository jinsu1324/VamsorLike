using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : SerializedMonoBehaviour
{
    private int _earnedGold = 0;                       // 플레이에서 획득한 골드

    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        RefreshGoldInvenUIText();
    }

    /// <summary>
    /// 골드 획득했을 때 처리
    /// </summary>
    public void GoldUp(int amount)
    {
        _earnedGold += amount;
        RefreshGoldInvenUIText();
    }

    /// <summary>
    /// 골드 인벤토리 UI 텍스트 새로고침
    /// </summary>
    public void RefreshGoldInvenUIText()
    {
        PlaySceneCanvas.Instance.GoldInvenUI.RefreshGoldText(_earnedGold);
    }
}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInvenUI_Slot : MonoBehaviour
{
    [SerializeField]
    private Image _icon;                    // 스킬 아이콘

    [SerializeField]
    private TextMeshProUGUI _levelText;     // 레벨 텍스트


    /// <summary>
    /// 슬롯 정보들 셋팅 + ON / OFF
    /// </summary>
    public void SetSlot(bool isOnOFF, int level, Sprite icon)
    {
        _levelText.text = level.ToString();
        _icon.sprite = icon;
        gameObject.SetActive(isOnOFF);
    }
}

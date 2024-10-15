using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HaveSkillUISlot : SerializedMonoBehaviour
{
    [SerializeField]
    private Image _icon;                    // 스킬 아이콘

    [SerializeField]
    private TextMeshProUGUI _levelText;     // 레벨 텍스트

    public void SlotONOFF(bool isCount)
    {
        gameObject.SetActive(isCount);
    }
}

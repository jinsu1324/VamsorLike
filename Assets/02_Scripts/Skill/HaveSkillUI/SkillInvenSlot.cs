using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInvenSlot : SerializedMonoBehaviour
{
    [SerializeField]
    private Image _icon;                    // ��ų ������

    [SerializeField]
    private TextMeshProUGUI _levelText;     // ���� �ؽ�Ʈ


    /// <summary>
    /// ���� ������ ���� + ON / OFF
    /// </summary>
    public void SetSlot(bool isOnOFF, int level, Sprite icon)
    {
        _levelText.text = (level + 1).ToString();
        _icon.sprite = icon;
        gameObject.SetActive(isOnOFF);
    }
}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInvenUI_Slot : MonoBehaviour
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
        _levelText.text = level.ToString();
        _icon.sprite = icon;
        gameObject.SetActive(isOnOFF);
    }
}

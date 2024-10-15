using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HaveSkillUISlot : SerializedMonoBehaviour
{
    [SerializeField]
    private Image _icon;                    // ��ų ������

    [SerializeField]
    private TextMeshProUGUI _levelText;     // ���� �ؽ�Ʈ

    public void SlotONOFF(bool isCount)
    {
        gameObject.SetActive(isCount);
    }
}

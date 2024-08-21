using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// ��ų ���ý���
public class SkillChoiceSlot : SerializedMonoBehaviour, IPointerClickHandler
{
    // �� ���Կ� ���� ��Ű �̸�
    [SerializeField]
    public TextMeshProUGUI SkillNameText { get; set; }

    // �� ���Կ� ���� ��ų ID
    public SkillID SkillID { get; set; }

    // ���� Ŭ������ ��
    public void OnPointerClick(PointerEventData eventData)
    {
        AddEquipedSkillList(SkillID);
    }

    // ���� ������ ����
    public void SetSlotInfos(SkillID skillID)
    {
        SkillID = skillID;
        SkillNameText.text = Managers.Instance.DataManager.SkillDataDict[skillID].Name;
    }

    // ������ ��ų����Ʈ�� �߰�
    private void AddEquipedSkillList(SkillID skillID)
    {
        HeroEquipedSkill.Instance.AddSkill(skillID);
    }

}

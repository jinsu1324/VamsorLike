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
    public SKILLID SkillID { get; set; }

    // ���� Ŭ������ ��
    public void OnPointerClick(PointerEventData eventData)
    {
        AddEquipedSkillList(SkillID);
    }

    // ���� ������ ����
    public void SetSlotInfos(SKILLID skillID)
    {
        SkillID = skillID;
        SkillNameText.text = DataManager.Instance.SkillDataDict[skillID].Name;
    }

    // ������ ��ų����Ʈ�� �߰�
    private void AddEquipedSkillList(SKILLID skillID)
    {
        HeroEquipedSkill heroEquipedSKill = new HeroEquipedSkill();
        heroEquipedSKill.AddSkill(skillID);
    }

}

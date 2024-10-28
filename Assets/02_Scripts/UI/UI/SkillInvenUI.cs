using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvenUI : MonoBehaviour
{
    [SerializeField]
    private List<SkillInvenUI_Slot> _skillInvenSlotList;            // ��ų UI ���� ����Ʈ


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        SlotDefaultOFF();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void SlotRefresh(List<Skill_Base> haveSkillList)
    {        
        for (int i = 0; i < haveSkillList.Count; i++)
        {
            int skillLevel = SkillManager.Instance.GetSkillLevel(haveSkillList[i].Id);
            Sprite skillIcon = SkillManager.Instance.GetSkillIcon(haveSkillList[i].Id);


            _skillInvenSlotList[i].SetSlot(true, skillLevel, skillIcon);
        }
    }

    /// <summary>
    /// ���Ե� �⺻�� �������� �����ϴ� �Լ�
    /// </summary>
    private void SlotDefaultOFF()
    {
        for (int i = 0; i < _skillInvenSlotList.Count; i++)
        {
            _skillInvenSlotList[i].gameObject.SetActive(false);
        }
    }
}

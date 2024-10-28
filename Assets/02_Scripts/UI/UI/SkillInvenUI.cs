using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInvenUI : MonoBehaviour
{
    [SerializeField]
    private List<SkillInvenUI_Slot> _skillInvenSlotList;            // 스킬 UI 슬롯 리스트


    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        SlotDefaultOFF();
    }

    /// <summary>
    /// 슬롯 갱신
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
    /// 슬롯들 기본값 꺼짐으로 설정하는 함수
    /// </summary>
    private void SlotDefaultOFF()
    {
        for (int i = 0; i < _skillInvenSlotList.Count; i++)
        {
            _skillInvenSlotList[i].gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveSkillUI : MonoBehaviour
{
    [SerializeField]
    private List<HaveSkillUISlot> _haveSkillUISlotsList;            // 보유한 스킬 UI 슬롯 리스트


    /// <summary>
    /// Start 함수
    /// </summary>
    private void Start()
    {
        SkillManager.Instance.OnRefreshHaveSkillUI += Refresh;
    }

    /// <summary>
    /// UI 갱신
    /// </summary>
    public void Refresh(List<Skill_Base> haveSkillList)
    {
        for (int i = 0; i < _haveSkillUISlotsList.Count; i++)
        {
            if (i < haveSkillList.Count)
            {
                _haveSkillUISlotsList[i].SlotONOFF(true);
            }
            else
            {
                _haveSkillUISlotsList[i].SlotONOFF(false);
            }
        }
    }
}

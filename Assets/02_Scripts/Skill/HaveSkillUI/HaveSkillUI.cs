using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveSkillUI : MonoBehaviour
{
    [SerializeField]
    private List<HaveSkillUISlot> _haveSkillUISlotsList;            // ������ ��ų UI ���� ����Ʈ


    /// <summary>
    /// Start �Լ�
    /// </summary>
    private void Start()
    {
        SkillManager.Instance.OnRefreshHaveSkillUI += Refresh;
    }

    /// <summary>
    /// UI ����
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

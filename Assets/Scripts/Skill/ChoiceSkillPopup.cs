using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��ų���ý��Ե� �ִ� �˾� : ��ų���� �迭 / ��ų���ý��Կ� �����ϰ� ��ų �־���
public class ChoiceSkillPopup : MonoBehaviour
{
    [SerializeField]
    private ChoiceSkillSlot[] _choiceSkillSlotArr;

    private void Start()
    {
        SettingChoiceSkillSlot();
    }

    private void SettingChoiceSkillSlot()
    {
        for (int i = 0; i < _choiceSkillSlotArr.Length; i++)
        {
            // SkillID�� �������� �ϳ��� �̾ƿ�
            SkillID[] skillIDs = System.Enum.GetValues(typeof(SkillID)) as SkillID[];
            SkillID skillID = (SkillID)skillIDs.GetValue(Random.Range(0, skillIDs.Length));

            _choiceSkillSlotArr[i].SetSlotInfos(skillID);
        }
    }
}

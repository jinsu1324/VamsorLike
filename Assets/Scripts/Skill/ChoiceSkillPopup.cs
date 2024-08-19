using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬선택슬롯들 있는 팝업 : 스킬슬롯 배열 / 스킬선택슬롯에 랜덤하게 스킬 넣어줌
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
            // SkillID중 랜덤으로 하나만 뽑아옴
            SkillID[] skillIDs = System.Enum.GetValues(typeof(SkillID)) as SkillID[];
            SkillID skillID = (SkillID)skillIDs.GetValue(Random.Range(0, skillIDs.Length));

            _choiceSkillSlotArr[i].SetSlotInfos(skillID);
        }
    }
}

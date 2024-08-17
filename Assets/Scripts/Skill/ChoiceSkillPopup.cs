using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            _choiceSkillSlotArr[i].Skill = new SkillBoomerang(Managers.Instance.DataManager.SkillDataDict[SkillID.Boomerang], transform.position);
        }
    }
}

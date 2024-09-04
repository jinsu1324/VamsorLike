using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 스킬선택슬롯들 있는 팝업 : 스킬슬롯 배열 / 스킬선택슬롯에 랜덤하게 스킬 넣어줌
public class SkillChoicePopup : MonoBehaviour
{
    // 선택하는 스킬 슬롯들
    [SerializeField]
    private SkillChoiceSlot[] _skillChoiceSlotArr;

    private void Start()
    {
        SettingSkillChoiceSlot();
    }

    // 스킬슬롯들 초기화
    private void SettingSkillChoiceSlot()
    {
        // 스킬 테스트용 임시
        _skillChoiceSlotArr[0].SetSlotInfos(SkillID.SlashAttack, ClosePopup);
        _skillChoiceSlotArr[1].SetSlotInfos(SkillID.Boomerang, ClosePopup);
        _skillChoiceSlotArr[2].SetSlotInfos(SkillID.Sniper, ClosePopup);
        _skillChoiceSlotArr[3].SetSlotInfos(SkillID.Heal, ClosePopup);


        // 랜덤으로 해주기

        //for (int i = 0; i < _skillChoiceSlotArr.Length; i++)
        //{
        //    // SkillID중 랜덤으로 하나만 뽑아옴
        //    SkillID[] skillIDs = System.Enum.GetValues(typeof(SkillID)) as SkillID[];
        //    SkillID skillID = (SkillID)skillIDs.GetValue(Random.Range(0, skillIDs.Length));

        //    _skillChoiceSlotArr[i].SetSlotInfos(skillID);
        //}
    }


    // 팝업 열기
    public void OpenPopup()
    {
        gameObject.SetActive(true);
    }


    // 팝업 닫기
    public void ClosePopup()
    {
        gameObject.SetActive(false);
    }
}

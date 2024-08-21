using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// 스킬 선택슬롯
public class SkillChoiceSlot : SerializedMonoBehaviour, IPointerClickHandler
{
    // 이 슬롯에 들어가는 스키 이름
    [SerializeField]
    public TextMeshProUGUI SkillNameText { get; set; }

    // 이 슬롯에 들어가는 스킬 ID
    public SkillID SkillID { get; set; }

    // 슬롯 클릭했을 때
    public void OnPointerClick(PointerEventData eventData)
    {
        AddEquipedSkillList(SkillID);
    }

    // 슬롯 정보들 셋팅
    public void SetSlotInfos(SkillID skillID)
    {
        SkillID = skillID;
        SkillNameText.text = Managers.Instance.DataManager.SkillDataDict[skillID].Name;
    }

    // 장착된 스킬리스트에 추가
    private void AddEquipedSkillList(SkillID skillID)
    {
        HeroEquipedSkill.Instance.AddSkill(skillID);
    }

}

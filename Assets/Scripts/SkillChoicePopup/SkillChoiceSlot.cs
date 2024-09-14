using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// 스킬 선택슬롯
public class SkillChoiceSlot : SerializedMonoBehaviour, IPointerClickHandler
{
    // 이 슬롯에 들어가는 스킬 이름
    [SerializeField]
    public TextMeshProUGUI SkillNameText { get; set; }

    // 이 슬롯에 들어가는 스킬 ID
    public SkillID SkillID { get; set; }

    // 선택 완료했을 때 액션
    private Action _onSelectFinish;


    // 슬롯 클릭했을 때
    public void OnPointerClick(PointerEventData eventData)
    {
        // 영웅 장착 스킬리스트에 추가
        AddEquipedSkillList(SkillID);

        // 스킬 선택팝업 닫기
        _onSelectFinish();

        // 게임시작 안되어있었다면
        if (PlaySceneManager.Instance.IsGameStart == false)
        {
            // 게임시작을 true로
            PlaySceneManager.Instance.IsGameStartChange(true);

            // 몬스터 스폰 시작
            MonsterSpawner.Instance.StartMonsterSpawn();            
        }
    }

    // 슬롯 정보들 셋팅
    public void SetSlotInfos(SkillID skillID, Action popupClose)
    {
        SkillID = skillID;      

        switch (skillID)
        {
            case SkillID.SlashAttack:
                SkillData_SlashAttack slashAttack = DataManager.Instance.SkillDataDict[SkillID.SlashAttack].SkillDataList[0] as SkillData_SlashAttack;
                SkillNameText.text = slashAttack.Name;
                break;
            case SkillID.Sniper:
                SkillData_Sniper sniper = DataManager.Instance.SkillDataDict[SkillID.Sniper].SkillDataList[0] as SkillData_Sniper;
                SkillNameText.text = sniper.Name;
                break;
            case SkillID.Boomerang:
                SkillData_Boomerang boomerang = DataManager.Instance.SkillDataDict[SkillID.Boomerang].SkillDataList[0] as SkillData_Boomerang;
                SkillNameText.text = boomerang.Name;
                break;
            default:
                break;
        }
        
        _onSelectFinish += popupClose;
    }

    // 장착된 스킬리스트에 추가
    private void AddEquipedSkillList(SkillID skillID)
    {
        HeroEquipedSkill heroEquipedSKill = new HeroEquipedSkill();
        heroEquipedSKill.AddSkill(skillID);
    }

}

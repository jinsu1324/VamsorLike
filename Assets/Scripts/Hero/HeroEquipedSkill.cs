using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 영웅이 장착하고있는 스킬들 : 장착중인 스킬 리스트 / 리스트에 스킬 더하기 / 리스트에 스킬 빼기
public class HeroEquipedSkill
{
    // 영웅이 장착하고있는 스킬들
    public static List<SkillBase> EquipedSkillList { get; set; } = new List<SkillBase>();
       

    // 장착 스킬 리스트에 스킬 추가
    public void AddSkill(SkillID choicedSkillID)
    {
        SkillBase choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Add(choicedSkill);
    }

    // 장착 스킬 리스트에 스킬 삭제
    public void RemoveSkill(SkillID choicedSkillID)
    {
        SkillBase choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Remove(choicedSkill);
    }

    // SkillID에 따라 Skill을 반환해주는 함수
    private SkillBase ReturnSkillByID(SkillID skillID)
    {
        // 스킬 데이터 딕셔너리
        //Dictionary<SkillID, SkillDataBase> skillDataDict = DataManager.Instance.SkillDataDict;
        //// 이번게임 플레이중인 영웅
        //HeroObject thisGameHeroObject = PlaySceneManager.ThisGameHeroObject;

        //// 스킬ID에 따라 Skill 인스턴스화 해서 반환
        //switch (skillID)
        //{
        //    case SkillID.SlashAttack:
        //        SkillSlashAttack skillSlashAttack = new SkillSlashAttack(skillDataDict[skillID], thisGameHeroObject.transform.position);
        //        Debug.Log("skillSlashAttack 선택!");
        //        return skillSlashAttack;

        //    case SkillID.Sniper:
        //        SkillSniper skillSniper = new SkillSniper(skillDataDict[skillID], thisGameHeroObject.transform.position);
        //        Debug.Log("skillSniper 선택!");
        //        return skillSniper;

        //    case SkillID.Boomerang:
        //        SkillBoomerang skillBoomerang = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
        //        Debug.Log("skillBoomerang 선택!");
        //        return skillBoomerang;
        //}

        //Debug.Log("ReturnSkillByID 아무것도 반환하지 못했습니다");
        return null;
    }
}

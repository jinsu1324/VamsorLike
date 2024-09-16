using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 영웅이 장착하고있는 스킬들 : 장착중인 스킬 리스트 / 리스트에 스킬 더하기 / 리스트에 스킬 빼기
public class HeroEquipedSkill
{
    // 영웅이 장착하고있는 스킬들
    public static List<Skill_Base> EquipedSkillList { get; set; } = new List<Skill_Base>();
       

    // 장착 스킬 리스트에 스킬 추가
    public void AddSkill(SkillID choicedSkillID)
    {
        Skill_Base choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Add(choicedSkill);
    }

    // 장착 스킬 리스트에 스킬 삭제
    public void RemoveSkill(SkillID choicedSkillID)
    {
        Skill_Base choicedSkill = ReturnSkillByID(choicedSkillID);
        EquipedSkillList.Remove(choicedSkill);
    }

    // SkillID에 따라 Skill을 반환해주는 함수
    private Skill_Base ReturnSkillByID(SkillID skillID)
    {
        HeroObject thisGameHeroObject = PlaySceneManager.ThisGameHeroObject;

        // 스킬ID에 따라 Skill 인스턴스화 해서 반환
        switch (skillID)
        {
            case SkillID.SlashAttack:
                Skill_SlashAttack skillSlashAttack = 
                    new Skill_SlashAttack(
                        DataManager.Instance.SkillData_as_SkillDataDict<SkillData_SlashAttack>(SkillID.SlashAttack, 0), 
                        thisGameHeroObject.transform.position);
                Debug.Log("skillSlashAttack 선택!");
                return skillSlashAttack;

            //case SkillID.Sniper:
            //    SkillSniper skillSniper = new SkillSniper(skillDataDict[skillID], thisGameHeroObject.transform.position);
            //    Debug.Log("skillSniper 선택!");
            //    return skillSniper;

            //case SkillID.Boomerang:
            //    SkillBoomerang skillBoomerang = new SkillBoomerang(skillDataDict[skillID], thisGameHeroObject.transform.position);
            //    Debug.Log("skillBoomerang 선택!");
            //    return skillBoomerang;
        }

        Debug.Log("ReturnSkillByID 아무것도 반환하지 못했습니다");
        return null;
    }


    
}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 스킬 인벤토리 클래스 : 가지고 있는 스킬 리스트 / 리스트에 스킬 더하기 / 리스트에 스킬 빼기
/// </summary>
public class PlayerSkillManager
{
    // 가지고 있는 스킬들
    public static List<Skill_Base> PlayerSkillsList { get; set; } = new List<Skill_Base>();

    /// <summary>
    /// 스킬 인벤토리에 스킬 추가해주는 함수
    /// </summary>
    public void AddSkill(SkillID newSkillID)
    {
        // 리스트에서 이미 가지고 있는 스킬인지 확인
        Skill_Base foundSkill = PlayerSkillsList.Find(skill => skill.Id == newSkillID);

        // 이미 가지고 있는 스킬이라면 레벨업
        if (foundSkill != null)
        {
            foundSkill.LevelUp();
        }
        // 가지고 있지 않다면 새로 추가
        else
        {
            Skill_Base skill = SkillFactory.CreateSkillClass(newSkillID, 1);
            PlayerSkillsList.Add(skill);
        }
    }

    /// <summary>
    /// 스킬 보유 여부 확인하는 함수
    /// </summary>
    public bool HasSkill(SkillID skillID)
    {
        // 리스트에서 이미 가지고 있는 스킬인지 확인해서 bool 반환
        return PlayerSkillsList.Exists(x => x.Id == skillID);
    }

    /// <summary>
    /// 스킬 레벨 가져오는 함수
    /// </summary>
    public int GetSkillLevel(SkillID skillID)
    {
        // 리스트에서 이미 가지고 있는 스킬인지 확인해서 레벨 반환
        Skill_Base foundSkill = PlayerSkillsList.Find(x => x.Id == skillID);
        return foundSkill != null ? foundSkill.CurrentLevel : 0;
    }
}

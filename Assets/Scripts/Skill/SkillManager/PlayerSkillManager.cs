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
    public static List<Skill_Base> playerSkillsList { get; set; } = new List<Skill_Base>();

    /// <summary>
    /// 스킬 인벤토리에 스킬 추가해주는 함수
    /// </summary>
    public void AddSkill(Skill_Base newSkill)
    {
        Skill_Base foundSkill = playerSkillsList.Find(skill => skill.Id == newSkill.Id);

        if (foundSkill != null)
        {
            foundSkill.LevelUp();
        }
        else
        {
            playerSkillsList.Add(newSkill);
        }
    }

    /// <summary>
    /// 스킬 보유 여부 확인하는 함수
    /// </summary>
    public bool HasSkill(Skill_Base skill)
    {
        return playerSkillsList.Exists(x => x.Id == skill.Id);
    }

    /// <summary>
    /// 스킬 레벨 가져오는 함수
    /// </summary>
    public int GetSkillLevel(Skill_Base skill)
    {
        Skill_Base foundSkill = playerSkillsList.Find(x => x.Id == skill.Id);
        return foundSkill != null ? foundSkill.CurrentLevel : 0;
    }
}

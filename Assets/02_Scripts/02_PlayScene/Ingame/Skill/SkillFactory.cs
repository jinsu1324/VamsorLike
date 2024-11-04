using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillFactory 
{
    public static Skill_Base CreateSkillClass(SkillID skillID, int level)
    {
        if (skillID == SkillID.SlashAttack)
        {
            SkillData skillData_SlashAttack
                = PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(SkillID.SlashAttack, level);

            Skill_SlashAttack skill_SlashAttack = new Skill_SlashAttack(skillData_SlashAttack);
            return skill_SlashAttack;
        }

        else if (skillID == SkillID.Sniper)
        {
            SkillData skillData_Sniper
                = PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(SkillID.Sniper, level);

            Skill_Sniper skill_Sniper = new Skill_Sniper(skillData_Sniper);
            return skill_Sniper;
        }

        else if (skillID == SkillID.Boomerang)
        {
            SkillData skillData_Boomerang
                = PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(SkillID.Boomerang, level);

            Skill_Boomerang skill_Boomerang = new Skill_Boomerang(skillData_Boomerang);
            return skill_Boomerang;
        }

        else
        {
            return null;
        }
    }
}

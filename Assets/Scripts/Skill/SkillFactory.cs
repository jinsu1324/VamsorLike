using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillFactory 
{
    public static Skill_Base CreateSkillClass(SkillID skillID, int level)
    {
        if (skillID == SkillID.SlashAttack)
        {
            SkillData_SlashAttack skillData_SlashAttack
                = SkillManager.Instance.SkillData_as_Dict<SkillData_SlashAttack>(SkillID.SlashAttack, level);

            Skill_SlashAttack skill_SlashAttack = new Skill_SlashAttack(skillData_SlashAttack);
            return skill_SlashAttack;
        }

        else if (skillID == SkillID.Sniper)
        {
            SkillData_Sniper skillData_Sniper
                = SkillManager.Instance.SkillData_as_Dict<SkillData_Sniper>(SkillID.Sniper, level);

            Skill_Sniper skill_Sniper = new Skill_Sniper(skillData_Sniper);
            return skill_Sniper;
        }

        else if (skillID == SkillID.Boomerang)
        {
            SkillData_Boomerang skillData_Boomerang
                = SkillManager.Instance.SkillData_as_Dict<SkillData_Boomerang>(SkillID.Boomerang, level);

            Skill_Boomerang skill_Boomerang = new Skill_Boomerang(skillData_Boomerang);
            return skill_Boomerang;
        }

        else
        {
            return null;
        }
    }
}

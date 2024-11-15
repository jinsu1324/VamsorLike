using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SkillFactory 
{
    public static Skill_Base CreateSkillClass(SkillID skillID, int level)
    {
        if (skillID == SkillID.SlashAttack)
        {
            SkillData skillData
                = PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(SkillID.SlashAttack, level);

            Skill_SlashAttack skill_SlashAttack = new Skill_SlashAttack(skillData);
            return skill_SlashAttack;
        }

        else if (skillID == SkillID.Sniper)
        {
            SkillData skillData
                = PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(SkillID.Sniper, level);

            Skill_Sniper skill_Sniper = new Skill_Sniper(skillData);
            return skill_Sniper;
        }

        else if (skillID == SkillID.Boomerang)
        {
            SkillData skillData
                = PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(SkillID.Boomerang, level);

            Skill_Boomerang skill_Boomerang = new Skill_Boomerang(skillData);
            return skill_Boomerang;
        }

        else if (skillID == SkillID.Meteor)
        {
            SkillData skillData
                = PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(SkillID.Meteor, level);

            Skill_Meteor skill_Meteor = new Skill_Meteor(skillData);
            return skill_Meteor;
        }

        else if (skillID == SkillID.Axe)
        {
            SkillData skillData
                = PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(SkillID.Axe, level);

            Skill_Axe skill_Axe = new Skill_Axe(skillData);
            return skill_Axe;
        }

        else
        {
            return null;
        }
    }
}

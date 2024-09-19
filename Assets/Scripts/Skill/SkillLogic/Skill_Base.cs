using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public struct SkillAttackArgs
{
    public Vector3 StartSkillPos;   
}


/// <summary>
/// 모든 스킬의 부모 클래스
/// </summary>
public abstract class Skill_Base
{
    public string Id;
    public string Name;
    public string Desc;
    public int CurrentLevel;
    public int MaxLevel;

    protected float _time;    

    /// <summary>
    /// 스킬 레벨업 함수
    /// </summary>
    public bool LevelUp()
    {
        if (CurrentLevel < MaxLevel)
        {
            CurrentLevel++;
            return true;
        }
        else
        {
            return false;
        }
    }

    public abstract bool SkillCooltime();

    public abstract void UseSkill(SkillAttackArgs skillAttackArgs);

}




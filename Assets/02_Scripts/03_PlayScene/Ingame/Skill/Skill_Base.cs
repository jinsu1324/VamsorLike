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
    public SkillID Id;
    public int CurrentLevel;
    public int MaxLevel;

    protected float _time;    

    public abstract void LevelUp();

    public abstract bool SkillCooltime();

    public abstract void UseSkill(SkillAttackArgs skillAttackArgs);

}




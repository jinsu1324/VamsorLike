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
    protected SkillData_Base _skillData;

    protected float _time;

    public abstract bool SkillUpdate();

    public abstract void AttackFunc(SkillAttackArgs skillAttackArgs);

}




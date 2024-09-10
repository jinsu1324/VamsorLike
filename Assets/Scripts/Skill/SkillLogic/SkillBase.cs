using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public struct SkillAttackArgs
{
    public Vector3 StartSkillPos;   
}

// 스킬 베이스
public abstract class SkillBase
{
    protected SkillDataBase _skillData;

    protected float _time;

    public abstract bool SkillUpdate();

    public abstract void AttackFunc(SkillAttackArgs skillAttackArgs);

}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

// ��ų ���̽�
public abstract class Skill
{
    protected SkillData _skillData = new SkillData();

    protected float _time;

    public abstract bool SkillUpdate();

    public abstract void AttackFunc(Vector3 skillPos);

}




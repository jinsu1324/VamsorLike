using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


// �̳����� ��ųŸ�Ը��� �� �̳��� ��ư���� �ְ�, �� �̳��̸� �� �̷�������?
// ��ų ���̽�
public abstract class Skill
{
    protected SkillData _skillData;

    protected float _time;

    public abstract bool SkillUpdate();

    public abstract void AttackFunc(Vector3 skillPos);

}




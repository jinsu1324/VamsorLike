using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


// 이넘으로 스킬타입만들어서 그 이넘을 버튼ㅇ네 넣고, 그 이넘이면 뭐 이런식으로?
// 스킬 베이스
public abstract class Skill
{
    protected SkillData _skillData;

    protected float _time;

    public abstract bool SkillUpdate();

    public abstract void AttackFunc(Vector3 skillPos);

}




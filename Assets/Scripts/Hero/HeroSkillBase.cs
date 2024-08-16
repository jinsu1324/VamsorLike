using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HeroSkillStatus
{
    public int ATK;
    public float Range;
    public float Delay;
}


public abstract class HeroSkillBase
{
    protected HeroSkillStatus _heroSkillStatus = new HeroSkillStatus();

    protected float _time;

    public abstract bool SkillUpdate();

    public abstract void AttackFunc(Vector3 skillPos);

}




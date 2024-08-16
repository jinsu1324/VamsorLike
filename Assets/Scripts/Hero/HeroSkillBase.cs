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


public class HeroSKillSword : HeroSkillBase
{

    public HeroSKillSword(int atk, float range, float delay)
    {
        _heroSkillStatus.ATK = atk;
        _heroSkillStatus.Range = range;
        _heroSkillStatus.Delay = delay;
    }
    
    public override bool SkillUpdate()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _heroSkillStatus.Delay)
        {
            _time %= _heroSkillStatus.Delay;
            return true;            
        }            
        else
        {
            return false;

        }
            
    }

    public override void AttackFunc(Vector3 skillPos)
    {
        List<MonsterObject> closeMonsterList = new List<MonsterObject>();
        closeMonsterList = SceneSingleton<MonsterManager>.Instance.GetMonstersByLength(skillPos, _heroSkillStatus.Range);

        for (int i = 0; i < closeMonsterList.Count; i++)
        {
            closeMonsterList[i].HPMinus(_heroSkillStatus.ATK);
        }

        Debug.Log("attack");
    }


}

public class HeroSKillBow : HeroSkillBase
{

    public HeroSKillBow(int atk, float range, float delay)
    {
        _heroSkillStatus.ATK = atk;
        _heroSkillStatus.Range = range;
        _heroSkillStatus.Delay = delay;
    }

    public override bool SkillUpdate()
    {
        _time += Time.fixedDeltaTime;

        if (_time > 0.1f)
        {
            _time %= 0.1f;
            return true;
        }
        else
        {
            return false;

        }
    }

    public override void AttackFunc(Vector3 skillPos)
    {
        List<MonsterObject> closeMonsterList = new List<MonsterObject>();
        closeMonsterList = SceneSingleton<MonsterManager>.Instance.GetMonstersByLength(skillPos, _heroSkillStatus.Range);

        for (int i = 0; i < closeMonsterList.Count; i++)
        {
            closeMonsterList[i].HPMinus(_heroSkillStatus.ATK);
            break;
        }

        Debug.Log("attack");
    }

}


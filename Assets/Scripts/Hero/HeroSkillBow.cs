using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        closeMonsterList[Random.Range(0, closeMonsterList.Count)].HPMinus(_heroSkillStatus.ATK);

        //for (int i = 0; i < closeMonsterList.Count; i++)
        //{
        //    closeMonsterList[i].HPMinus(_heroSkillStatus.ATK);
        //    break;
        //}
    }
}

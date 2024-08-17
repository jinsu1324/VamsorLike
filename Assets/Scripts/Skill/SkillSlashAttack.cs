using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillSlashAttack : Skill
{

    public SkillSlashAttack(int atk, float range, float delay)
    {
        _skillData.Atk = atk;
        _skillData.Range = range;
        _skillData.Delay = delay;
    }

    public override bool SkillUpdate()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _skillData.Delay)
        {
            _time %= _skillData.Delay;
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
        closeMonsterList = SceneSingleton<MonsterManager>.Instance.GetMonstersByLength(skillPos, _skillData.Range);

        for (int i = 0; i < closeMonsterList.Count; i++)
        {
            closeMonsterList[i].HPMinus(_skillData.Atk);
        }

        Debug.Log("attack");
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이 스킬은 어떤 스킬인지 정의해 주는 역할
public class SkillSniper : Skill
{
    public SkillSniper(int atk, float range, float delay)
    {
        _skillData.Atk = atk;
        _skillData.Range = range;
        _skillData.Delay = delay;
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
        closeMonsterList = SceneSingleton<MonsterManager>.Instance.GetMonstersByLength(skillPos, _skillData.Range);

        closeMonsterList[Random.Range(0, closeMonsterList.Count)].HPMinus(_skillData.Atk);

        //for (int i = 0; i < closeMonsterList.Count; i++)
        //{
        //    closeMonsterList[i].HPMinus(_heroSkillStatus.ATK);
        //    break;
        //}
    }
}

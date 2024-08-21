using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class SkillSlashAttack : Skill
{
    // 프로젝타일
    private ProjectileSlashAttack _spawnedProjectileSlashAttack;

    // 생성자
    public SkillSlashAttack(SkillData skillData, Vector3 pos)
    {
        // 스킬 슬래시어택이 인스턴스화 되는 순간, 매개변수로 받아온 데이터값을 이 SkillSlashAttack의 데이터로 넣어줌
        _skillData = skillData;    
    }

    // 스킬 쿨타임 관리 (스킬 공격 가능한지 true false 반환)
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

    // 공격 로직
    public override void AttackFunc(Vector3 skillPos)
    {
        // 프로젝타일 생성
        _spawnedProjectileSlashAttack =
            Object.Instantiate(_skillData.Projectile, skillPos, Quaternion.identity) as ProjectileSlashAttack;

        // 프로젝타일에 공격력 건네줌
        _spawnedProjectileSlashAttack.TakeSkillAtk(_skillData.Atk);


        //List<MonsterObject> closeMonsterList = new List<MonsterObject>();
        //closeMonsterList = SceneSingleton<MonsterManager>.Instance.GetMonstersByLength(skillPos, _skillData.Range);

        //for (int i = 0; i < closeMonsterList.Count; i++)
        //{
        //    closeMonsterList[i].HPMinus(_skillData.Atk);
        //}

        //Debug.Log("attack");
    }
}

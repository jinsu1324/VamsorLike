using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

// 이 스킬은 어떤 스킬인지 정의해 주는 역할
public class SkillSniper : Skill
{
    // 프로젝타일
    private ProjectileSniper _spawnedProjectileSniper;

    // 생성자
    public SkillSniper(SkillData skillData, Vector3 pos)
    {
        // SkillSniper가 인스턴스화 되는 순간, 매개변수로 받아온 데이터값을 이 SkillSlashAttack의 데이터로 넣어줌
        _skillData = skillData;
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

    // 공격 로직
    public override void AttackFunc(Vector3 skillPos)
    {
        // 사거리 내 랜덤한 몬스터 찾기
        //List<MonsterObject> closeMonsterList = new List<MonsterObject>();
        //closeMonsterList = SceneSingleton<MonsterManager>.Instance.GetMonstersByDistance(skillPos, _skillData.Range);

        //MonsterObject targetMonster = closeMonsterList[Random.Range(0, closeMonsterList.Count)];


        // 사거리 내 가장 가까운 몬스터 찾기
        MonsterObject closestTargetMonster = 
            SceneSingleton<MonsterManager>.Instance.GetClosestMonstersByDistance(skillPos, _skillData.Range);

        // 프로젝타일 생성
        _spawnedProjectileSniper =
            Object.Instantiate(_skillData.Projectile, skillPos, Quaternion.identity) as ProjectileSniper;

        // 공격력 건네줌
        _spawnedProjectileSniper.TakeSkillAtk(_skillData.Atk);

        // 프로젝타일 타겟몬스터로 이동
        _spawnedProjectileSniper.SettingProjectileInfo(closestTargetMonster);                


        //for (int i = 0; i < closeMonsterList.Count; i++)
        //{
        //    closeMonsterList[i].HPMinus(_heroSkillStatus.ATK);
        //    break;
        //}
    }
}

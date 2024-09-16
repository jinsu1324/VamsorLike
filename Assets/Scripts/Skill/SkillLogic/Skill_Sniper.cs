using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


/// <summary>
/// Sniper 스킬 클래스 : 쿨타임 bool 판별 / 공격로직
/// </summary>
public class Skill_Sniper : Skill_Base
{
    private SkillData_Sniper _skillData_Sniper = new SkillData_Sniper();
    // 프로젝타일
    private ProjectileSniper _spawnedProjectileSniper;

    // 생성자
    public Skill_Sniper() : base("Sniper", "스나이퍼 총", "원거리 공격을 합니다.", 3)
    {

    }


    /// <summary>
    /// 스킬 쿨타임 관리 (시간 지남에 따라 스킬 공격 가능한지 true false 반환)
    /// </summary>
    public override bool SkillCooltime()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _skillData_Sniper.Delay)
        {
            _time %= _skillData_Sniper.Delay;
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 스킬 공격
    /// </summary>
    public override void UseSkill(SkillAttackArgs skillAttackArgs)
    {
        //// 사거리 내 가장 가까운 몬스터 찾기
        //MonsterObject closestTargetMonster =
        //    MonsterManager.Instance.GetClosestMonstersByDistance(skillAttackArgs.StartSkillPos, _skillData_Sniper.Range);

        //// 프로젝타일 생성
        //_spawnedProjectileSniper =
        //    Object.Instantiate(_skillData_Sniper.Projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSniper;

        //// 공격력 건네줌
        //_spawnedProjectileSniper.TakeSkillAtk(_skillData_Sniper.Atk);

        //// 프로젝타일 타겟몬스터로 이동
        //_spawnedProjectileSniper.SettingProjectileInfo(closestTargetMonster);

        Debug.Log("Sniper 스킬 공격");
    }
}

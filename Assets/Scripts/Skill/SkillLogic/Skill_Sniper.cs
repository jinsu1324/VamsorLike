using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


/// <summary>
/// Sniper 스킬 클래스 : 쿨타임 bool 판별 / 공격로직
/// </summary>
public class Skill_Sniper : Skill_Base
{
    private int _atk;
    private float _projectileSpeed;
    private float _range;
    private float _delay;


    private SkillData_Sniper _skillData_Sniper = new SkillData_Sniper();
    // 프로젝타일
    private ProjectileSniper _spawnedProjectileSniper;



    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData_Sniper skillData_Sniper)
    {
        _atk = skillData_Sniper.Atk;
        _projectileSpeed = skillData_Sniper.ProjectileSpeed;
        _range = skillData_Sniper.Range;
        _delay = skillData_Sniper.Delay;
    }


    /// <summary>
    /// 레벨업 함수 : 생성되어있는 얘를 다른 데이터로 덮어씌우기 위함
    /// </summary>
    public override void LevelUp()
    {
        SkillData_Sniper skillData_Sniper =
            DataManager.Instance.SkillData_as_SkillDataDict<SkillData_Sniper>(Id, ++CurrentLevel);

        StatSetting(skillData_Sniper);

    }

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Sniper(SkillData_Sniper skillData_Sniper)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_Sniper.Id);
        CurrentLevel = 1;
        MaxLevel = 3;

        StatSetting(skillData_Sniper);

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

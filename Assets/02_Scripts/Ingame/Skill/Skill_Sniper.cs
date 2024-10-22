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
    // 스탯들
    private float _skillAtk;
    private float _projectileSpeed;
    private float _range;
    private float _delay;
    private ProjectileBase _projectile;

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Sniper(SkillData_Sniper skillData_Sniper)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_Sniper.Id);
        CurrentLevel = 0;
        MaxLevel = 2;

        StatSetting(skillData_Sniper);
    }

    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData_Sniper skillData_Sniper)
    {
        _skillAtk = skillData_Sniper.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _projectileSpeed = skillData_Sniper.ProjectileSpeed;
        _range = skillData_Sniper.Range;
        _delay = skillData_Sniper.Delay;
        _projectile = skillData_Sniper.Projectile;
    }

    /// <summary>
    /// 레벨업 함수 : 생성되어있는 얘를 다른 데이터로 덮어씌우기 위함
    /// </summary>
    public override void LevelUp()
    {
        SkillData_Sniper skillData_Sniper =
            SkillManager.Instance.SkillData_as_Dict<SkillData_Sniper>(Id, ++CurrentLevel);

        StatSetting(skillData_Sniper);
    }

    /// <summary>
    /// 스킬 쿨타임 관리 (시간 지남에 따라 스킬 공격 가능한지 true false 반환)
    /// </summary>
    public override bool SkillCooltime()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _delay)
        {
            _time %= _delay;
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
        // 사거리 내 가장 가까운 몬스터 찾기
        MonsterObject closestTargetMonster =
            MonsterManager.Instance.GetClosestMonstersByDistance(skillAttackArgs.StartSkillPos, _range);

        // 프로젝타일 생성
        ProjectileSniper _spawnedProjectile =
            GameObject.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSniper;

        // 공격력 건네줌
        _spawnedProjectile.SetAtk(_skillAtk);

        // 프로젝타일 타겟몬스터로 이동
        _spawnedProjectile.SettingProjectileInfo(closestTargetMonster);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Sniper : Skill_Base
{
    private float _skillAtk;                // 스킬 공격력
    private float _projectileSpeed;         // 프로젝타일 스피드
    private float _range;                   // 범위
    private float _delay;                   // 딜레이
    private ProjectileBase _projectile;     // 스킬 프로젝타일

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Sniper(SkillData skillData)
    {
        // 기본 정보 할당
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);

        // 스킬 스탯 셋팅
        StatSetting(skillData);
    }

    /// <summary>
    /// 스킬 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        _skillAtk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _projectileSpeed = skillData.ProjectileSpeed;
        _range = skillData.Range;
        _delay = skillData.Delay;
        _projectile = ResourceManager.Instance.SkillProjectileDict[ID];
    }

    /// <summary>
    /// 스킬 레벨업
    /// </summary>
    public override void LevelUp()
    {
        // 현재레벨을 1 증가시키고, 그 값으로 스킬데이터를 다시 가져옴
        SkillData skillData =
            PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(ID, ++CurrentLevel);

        // 레벨업된 스탯으로 다시 셋팅
        StatSetting(skillData);
    }

    /// <summary>
    /// 스킬 쿨타임 관리
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
        //// 사거리 내 가장 가까운 몬스터 찾기
        //Enemy closestTargetMonster =
        //    PlaySceneManager.Instance.EnemyManager.Get_ClosestEnemy_In_Distance(skillAttackArgs.StartSkillPos, _range);

        //// 프로젝타일 생성
        //ProjectileSniper _spawnedProjectile =
        //    GameObject.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSniper;

        //// 공격력 건네줌
        //_spawnedProjectile.SetAtk(_skillAtk);

        //// 프로젝타일 타겟몬스터로 이동
        //_spawnedProjectile.SettingProjectileInfo(closestTargetMonster);
    }
}

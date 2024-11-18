using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Skill_Sniper : Skill_Base
{
    private ProjectileSniperArgs _statArgs = new ProjectileSniperArgs();    // 프로젝타일 스나이퍼에 필요한 스탯들
    private float _range;                                                   // 투사체 인지 범위
    private float _delay;                                                   // 딜레이
    private ProjectileSniper _projectile;                                   // 스킬 프로젝타일

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Sniper(SkillData skillData)
    {
        // 기본 정보 할당
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);
        _projectile = ResourceManager.Instance.SkillProjectileDict[ID] as ProjectileSniper;

        // 스킬 스탯 셋팅
        StatSetting(skillData);
    }

    /// <summary>
    /// 스킬 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        _statArgs.Atk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _statArgs.Speed = skillData.ProjectileSpeed;
        _range = skillData.Range;
        _delay = skillData.Delay;
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
        // 사거리 내 가장 가까운 몬스터 1마리 찾기
        List<Enemy> targetEnemyList =
            PlaySceneManager.Instance.EnemyManager.Get_ClosestEnemys_In_Distance(
                skillAttackArgs.StartSkillPos, 
                _range, 
                1);

        if (targetEnemyList.Count == 0)
            return;

        // 투사체 날릴 방향 설정
        Vector3 dir = targetEnemyList[0].transform.position - skillAttackArgs.StartSkillPos;
        dir.Normalize();
        _statArgs.Dir = dir;

        // 프로젝타일 생성
        ProjectileSniper projectile = GameObject.Instantiate(
            _projectile, skillAttackArgs.StartSkillPos, Quaternion.identity);

        // 프로젝타일 스탯 셋팅
        projectile.SetStats(_statArgs);
    }
}

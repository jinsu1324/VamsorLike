using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

/// <summary>
/// Boomerang 스킬 클래스 : 쿨타임 bool 판별 / 공격로직
/// </summary>
public class Skill_Boomerang : Skill_Base
{
    // 스킬 데이터 변수
    private float _skillAtk;
    private float _projectileSpeed;
    private int _projectileCount;
    private float _range;
    private ProjectileBoomerang _spawnedProjectile;

    // 부메랑 시작해도 되는지
    private bool _isBoomerangStarted = false;

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Boomerang(SkillData_Boomerang skillData_Boomerang)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_Boomerang.ID);
        CurrentLevel = 0;
        MaxLevel = 2;

        StatSetting(skillData_Boomerang);
    }

    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData_Boomerang skillData_Boomerang)
    {
        _skillAtk = skillData_Boomerang.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _projectileSpeed = skillData_Boomerang.ProjectileSpeed;
        _projectileCount = skillData_Boomerang.ProjectileCount;
        _range = skillData_Boomerang.Range;

        CreateBoomerangProjectile(skillData_Boomerang);
    }
    
    /// <summary>
    /// 레벨업 함수 : 생성되어있는 얘를 다른 데이터로 덮어씌우기 위함
    /// </summary>
    public override void LevelUp()
    {
        // 수정필요!
        //SkillData_Boomerang skillData_Boomerang =
        //   PlaySceneManager.Instance.SkillManager.SkillData_as_Dict<SkillData_Boomerang>(Id, ++CurrentLevel);

        //StatSetting(skillData_Boomerang);
    }

    /// <summary>
    /// 부메랑 프로젝타일 생성
    /// </summary>
    private void CreateBoomerangProjectile(SkillData_Boomerang skillData_Boomerang)
    {
        // 프로젝타일 생성
        _spawnedProjectile = GameObject.Instantiate(
                skillData_Boomerang.Projectile,
                PlaySceneManager.Instance.MyHeroObj.transform.position,
                Quaternion.identity) as ProjectileBoomerang;

        // 부메랑은 생성되면 계속 공격하게끔 만들 예정
        _isBoomerangStarted = true;
    } 

    /// <summary>
    /// 스킬 쿨타임 관리 (시간 지남에 따라 스킬 공격 가능한지 true false 반환)
    /// </summary>
    public override bool SkillCooltime()
    {
        if (_isBoomerangStarted)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 스킬 공격
    /// </summary>
    public override void UseSkill(SkillAttackArgs skillAttackArgs)
    {
        _spawnedProjectile.AroundBoomerang(skillAttackArgs.StartSkillPos);
        _spawnedProjectile.SetAtk(_skillAtk);
    }    
}

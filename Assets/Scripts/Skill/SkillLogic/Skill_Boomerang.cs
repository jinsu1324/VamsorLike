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
    // 부메랑 시작해도 되는지
    private bool _isBoomerangStarted = false;

    private SkillData_Boomerang _skillData_Boomerang = new SkillData_Boomerang();

    // 프로젝타일
    private ProjectileBoomerang _SpawnedProjectileBoomerang;


    // 스킬 데이터 변수
    private int _atk;
    private float _projectileSpeed;
    private int _projectileCount;
    private float _range;


    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData_Boomerang skillData_Boomerang)
    {
        _atk = skillData_Boomerang.Atk;
        _projectileSpeed = skillData_Boomerang.ProjectileSpeed;
        _projectileCount = skillData_Boomerang.ProjectileCount;
        _range = skillData_Boomerang.Range;
    }

    /// <summary>
    /// 레벨업 함수 : 생성되어있는 얘를 다른 데이터로 덮어씌우기 위함
    /// </summary>
    public override void LevelUp()
    {
        SkillData_Boomerang skillData_Boomerang =
            DataManager.Instance.SkillData_as_SkillDataDict<SkillData_Boomerang>(Id, ++CurrentLevel);

        StatSetting(skillData_Boomerang);
    }

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Boomerang(SkillData_Boomerang skillData_Boomerang)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_Boomerang.Id);
        CurrentLevel = 1;
        MaxLevel = 3;

        StatSetting(skillData_Boomerang);



        // // 프로젝타일 생성
        // _SpawnedProjectileBoomerang = Object.Instantiate(
        //    _skillData_Boomerang.Projectile,
        //    PlaySceneManager.ThisGameHeroObject.transform.position,
        //    Quaternion.identity)
        //as ProjectileBoomerang;

        // // 부메랑은 생성되면 계속 공격하게끔 만들 예정
        // _isBoomerangStarted = true;
    }

    


    /// <summary>
    /// 스킬 쿨타임 관리 (시간 지남에 따라 스킬 공격 가능한지 true false 반환)
    /// </summary>
    public override bool SkillCooltime()
    {
        // 부메랑은 생성되면 계속 돌아야한다.
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
        //_SpawnedProjectileBoomerang.AroundBoomerang(skillAttackArgs.StartSkillPos);
        //_SpawnedProjectileBoomerang.TakeSkillAtk(_skillData_Boomerang.Atk);

        Debug.Log("Boomerang 스킬 공격");
    }    
}

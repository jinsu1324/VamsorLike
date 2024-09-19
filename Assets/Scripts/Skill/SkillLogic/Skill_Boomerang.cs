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

    // 생성자
    public Skill_Boomerang(/*SkillData_Boomerang skillData_Boomerang*/)
    {      
        //// 프로젝타일 생성
        // _SpawnedProjectileBoomerang = Object.Instantiate(
        //    _skillData_Boomerang.Projectile, 
        //    PlaySceneManager.ThisGameHeroObject.transform.position, 
        //    Quaternion.identity) 
        //as ProjectileBoomerang;

        //// 부메랑은 생성되면 계속 공격하게끔 만들 예정
        //_isBoomerangStarted = true;
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

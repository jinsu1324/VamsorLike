using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// SlashAttack 스킬 클래스 : 쿨타임 bool 판별 / 공격로직
/// </summary>
public class Skill_SlashAttack : Skill_Base
{

    private SkillData_SlashAttack _skillData_SlashAttack = new SkillData_SlashAttack();

    // 프로젝타일
    private ProjectileSlashAttack _spawnedProjectileSlashAttack;



    // 생성자
    public Skill_SlashAttack() : base("SlashAttack", "슬래시 어택", "앞으로 빠르게 베어 공격합니다.", 3)
    {
        
    }


    /// <summary>
    /// 스킬 쿨타임 관리 (시간 지남에 따라 스킬 공격 가능한지 true false 반환)
    /// </summary>
    public override bool SkillCooltime()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _skillData_SlashAttack.Delay)
        {
            _time %= _skillData_SlashAttack.Delay;
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
        //// 프로젝타일 생성
        //_spawnedProjectileSlashAttack =
        //    Object.Instantiate(_skillData_SlashAttack.Projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSlashAttack;

        //// 프로젝타일에 공격력 건네줌
        //_spawnedProjectileSlashAttack.TakeSkillAtk(_skillData_SlashAttack.Atk);

        Debug.Log("SlashAttack 스킬 공격");

    }
}

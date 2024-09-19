using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// SlashAttack 스킬 클래스 : 쿨타임 bool 판별 / 공격로직
/// </summary>
public class Skill_SlashAttack : Skill_Base
{
    // 스킬 데이터 변수
    private int _atk;
    private float _delay;
    private ProjectileBase _projectile;

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_SlashAttack(/*SkillData_SlashAttack skillData_SlashAttack*/)
    {

        //Id = skillData_SlashAttack.Id;
        //Name = skillData_SlashAttack.Name;
        //Desc = skillData_SlashAttack.Desc;
        //CurrentLevel = 0;
        //MaxLevel = 3;

        //_atk = skillData_SlashAttack.Atk;
        //_delay = skillData_SlashAttack.Delay;
        //_projectile = skillData_SlashAttack.Projectile;

        Id = "SlashAttack";
        Name = "슬래시어택";
        Desc = "근거리 공격입니다";
        CurrentLevel = 1;
        MaxLevel = 3;

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
        //// 프로젝타일 생성
        //ProjectileSlashAttack projectile =
        //    Object.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) 
        //    as ProjectileSlashAttack;

        //// 프로젝타일에 공격력 건네줌
        //projectile.SetProjectileAtk(_atk);

        //Debug.Log("SlashAttack 스킬 공격");

    }
}

using System;
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
    private float _skillAtk;
    private float _delay;
    private ProjectileBase _projectile;

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_SlashAttack(SkillData skillData_SlashAttack)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_SlashAttack.ID);
        CurrentLevel = 0;
        MaxLevel = 2;

        StatSetting(skillData_SlashAttack);
    }

    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData skillData_SlashAttack)
    {
        _skillAtk = skillData_SlashAttack.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _delay = skillData_SlashAttack.Delay;

        _projectile = ResourceManager.Instance.SkillProjectileDict[Id];
    }

    /// <summary>
    /// 레벨업 함수 : 생성되어있는 얘를 다른 데이터로 덮어씌우기 위함
    /// </summary>
    public override void LevelUp()
    {
        SkillData skillData_SlashAttack =
            PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(Id, ++CurrentLevel);

        StatSetting(skillData_SlashAttack);
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
        ProjectileSlashAttack projectile =
            GameObject.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity)
            as ProjectileSlashAttack;

        // 프로젝타일에 공격력 건네줌
        projectile.SetAtk(_skillAtk);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SlashAttack : Skill_Base
{
    private ProjectileSlashAttackStatArgs _statArgs = new ProjectileSlashAttackStatArgs();      // 프로젝타일 슬래시어택에 필요한 스탯들
    private float _delay;                                                                       // 스킬 딜레이
    private ProjectileSlashAttack _projectile;                                                  // 스킬 프로젝타일

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_SlashAttack(SkillData skillData)
    {
        // 기본 정보 할당
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);
        _projectile = ResourceManager.Instance.SkillProjectileDict[ID] as ProjectileSlashAttack;

        // 스탯 셋팅
        StatSetting(skillData);
    }

    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        // 스탯 셋팅
        _statArgs.Atk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
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
        // 프로젝타일 생성
        ProjectileSlashAttack projectile = GameObject.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity);
        
        // 생성한 프로젝타일 스탯 셋팅
        projectile.SetStats(_statArgs);

        // 사운드 재생
        AudioManager.Instance.PlaySFX(SFXType.Melee_Attack);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_Meteor : Skill_Base
{
    private ProjectileMeteorArgs _statArgs = new ProjectileMeteorArgs();    // 프로젝타일 메테오에 필요한 스탯들
    private int _projectileCount;                                           // 프로젝타일 갯수
    private float _delay;                                                   // 딜레이
    private float _range;                                                   // 범위
    private ProjectileMeteor _projectile;                                   // 프로젝타일

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Meteor(SkillData skillData)
    {
        // 기본 정보 할당
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);
        _projectile = ResourceManager.Instance.SkillProjectileDict[ID] as ProjectileMeteor;

        // 스탯 셋팅
        StatSetting(skillData);
    }

    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        _statArgs.Atk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _delay = skillData.Delay;
        _range = skillData.Range;
        _projectileCount = skillData.ProjectileCount;
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
        PlaySceneManager.Instance.SkillManager.StartCoroutine(MeteorSkill(skillAttackArgs));
    }

    /// <summary>
    /// 메테오 스킬 코루틴
    /// </summary>
    private IEnumerator MeteorSkill(SkillAttackArgs skillAttackArgs)
    {
        List<Enemy> targetEnemyList = PlaySceneManager.Instance.EnemyManager.Get_RandomEnemys_In_Distance(
            skillAttackArgs.StartSkillPos, _range, _projectileCount);
        
        if (targetEnemyList.Count == 0)
            yield break;

        for (int i = 0; i < targetEnemyList.Count; i++)
        {
            // 프로젝타일 생성
            ProjectileMeteor projectile = GameObject.Instantiate(
                _projectile, targetEnemyList[i].transform.position, Quaternion.identity);

            // 생성한 프로젝타일 스탯 셋팅
            projectile.SetStats(_statArgs);

            yield return new WaitForSeconds(0.2f);
        }
    }
}

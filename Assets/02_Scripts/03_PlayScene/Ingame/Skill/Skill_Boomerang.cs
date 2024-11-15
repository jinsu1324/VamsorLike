using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Skill_Boomerang : Skill_Base
{
    private ProjectileBoomerangStatArgs _statArgs = new ProjectileBoomerangStatArgs();    // 프로젝타일 부메랑에 필요한 스탯들
    private int _projectileCount;                                                         // 프로젝타일 갯수
    private ProjectileBoomerang _projectile;                                              // 스폰 프로젝타일
    private List<ProjectileBoomerang> _projectileList = new List<ProjectileBoomerang>();  // 스폰되어있는 부메랑프로젝타일 리스트
    private bool _isBoomerangStarted = false;                                             // 부메랑 시작해도 되는지

    /// <summary>
    /// 생성자
    /// </summary>
    public Skill_Boomerang(SkillData skillData)
    {
        // 기본정보 할당
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);

        // 스탯셋팅
        StatSetting(skillData);

        // 프로젝타일 생성 
        CreateProjectileBoomerang();
    }

    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        // 스탯 셋팅
        _statArgs.Atk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _statArgs.Speed = skillData.ProjectileSpeed;
        _statArgs.Range = skillData.Range;
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

        // 부메랑 갯수 업데이트
        UpdateProjectileCount();
    }

    /// <summary>
    /// 부메랑 프로젝타일 생성
    /// </summary>
    private void CreateProjectileBoomerang()
    {
        // 스킬 프로젝타일 카운트수만큼 부메랑 생성
        for (int i = 0; i < _projectileCount; i++)
        {
            ProjectileBoomerang projectile = GameObject.Instantiate(
                ResourceManager.Instance.SkillProjectileDict[ID] as ProjectileBoomerang,
                PlaySceneManager.Instance.MyHeroObj.transform.position,
                Quaternion.identity
            );
            projectile.SetStats(_statArgs);
            _projectileList.Add(projectile);
        }
        _isBoomerangStarted = true;
    }


    /// <summary>
    /// 부메랑 갯수 업데이트
    /// </summary>
    private void UpdateProjectileCount()
    {
        // 1. 기존 부메랑 제거
        foreach (var projectile in _projectileList)
        {
            if (projectile != null)
                GameObject.Destroy(projectile.gameObject); // 부메랑 오브젝트 삭제
        }
        _projectileList.Clear(); // 리스트 초기화

        // 2. 새로운 부메랑 생성
        for (int i = 0; i < _projectileCount; i++)
        {
            ProjectileBoomerang projectile = GameObject.Instantiate(
                ResourceManager.Instance.SkillProjectileDict[ID] as ProjectileBoomerang,
                PlaySceneManager.Instance.MyHeroObj.transform.position,
                Quaternion.identity
            );
            projectile.SetStats(_statArgs);
            _projectileList.Add(projectile);
        }
    }

    /// <summary>
    /// 스킬 쿨타임 관리
    /// </summary>
    public override bool SkillCooltime() => _isBoomerangStarted;

    /// <summary>
    /// 스킬 공격
    /// </summary>
    public override void UseSkill(SkillAttackArgs skillAttackArgs)
    {
        if (_isBoomerangStarted == false) 
            return;

        // 각 부메랑 간 각도 계산
        float angle = 360f / _projectileList.Count; 
        
        for (int i = 0; i < _projectileList.Count; i++)
        {
            // 각 부메랑의 초기 각도 설정
            float angleOffset = i * angle; 

            // 부메랑 회전
            _projectileList[i].AroundBoomerang(skillAttackArgs.StartSkillPos, angleOffset);
        }
    }    
}

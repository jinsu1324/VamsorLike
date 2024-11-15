using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Skill_Boomerang : Skill_Base
{
    private ProjectileBoomerangStatArgs _statArgs = new ProjectileBoomerangStatArgs();    // ������Ÿ�� �θ޶��� �ʿ��� ���ȵ�
    private int _projectileCount;                                                         // ������Ÿ�� ����
    private ProjectileBoomerang _projectile;                                              // ���� ������Ÿ��
    private List<ProjectileBoomerang> _projectileList = new List<ProjectileBoomerang>();  // �����Ǿ��ִ� �θ޶�������Ÿ�� ����Ʈ
    private bool _isBoomerangStarted = false;                                             // �θ޶� �����ص� �Ǵ���

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Boomerang(SkillData skillData)
    {
        // �⺻���� �Ҵ�
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);

        // ���ȼ���
        StatSetting(skillData);

        // ������Ÿ�� ���� 
        CreateProjectileBoomerang();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        // ���� ����
        _statArgs.Atk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _statArgs.Speed = skillData.ProjectileSpeed;
        _statArgs.Range = skillData.Range;
        _projectileCount = skillData.ProjectileCount;
    }

    /// <summary>
    /// ��ų ������
    /// </summary>
    public override void LevelUp()
    {
        // ���緹���� 1 ������Ű��, �� ������ ��ų�����͸� �ٽ� ������
        SkillData skillData =
           PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(ID, ++CurrentLevel);

        // �������� �������� �ٽ� ����
        StatSetting(skillData);

        // �θ޶� ���� ������Ʈ
        UpdateProjectileCount();
    }

    /// <summary>
    /// �θ޶� ������Ÿ�� ����
    /// </summary>
    private void CreateProjectileBoomerang()
    {
        // ��ų ������Ÿ�� ī��Ʈ����ŭ �θ޶� ����
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
    /// �θ޶� ���� ������Ʈ
    /// </summary>
    private void UpdateProjectileCount()
    {
        // 1. ���� �θ޶� ����
        foreach (var projectile in _projectileList)
        {
            if (projectile != null)
                GameObject.Destroy(projectile.gameObject); // �θ޶� ������Ʈ ����
        }
        _projectileList.Clear(); // ����Ʈ �ʱ�ȭ

        // 2. ���ο� �θ޶� ����
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
    /// ��ų ��Ÿ�� ����
    /// </summary>
    public override bool SkillCooltime() => _isBoomerangStarted;

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void UseSkill(SkillAttackArgs skillAttackArgs)
    {
        if (_isBoomerangStarted == false) 
            return;

        // �� �θ޶� �� ���� ���
        float angle = 360f / _projectileList.Count; 
        
        for (int i = 0; i < _projectileList.Count; i++)
        {
            // �� �θ޶��� �ʱ� ���� ����
            float angleOffset = i * angle; 

            // �θ޶� ȸ��
            _projectileList[i].AroundBoomerang(skillAttackArgs.StartSkillPos, angleOffset);
        }
    }    
}

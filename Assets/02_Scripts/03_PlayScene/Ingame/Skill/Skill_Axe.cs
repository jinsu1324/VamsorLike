using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Axe : Skill_Base
{
    private ProjectileAxeArgs _statArgs = new ProjectileAxeArgs();              // ������Ÿ�� ������ �ʿ��� ���ȵ�
    private int _projectileCount;                                               // ������Ÿ�� ����
    private float _delay;                                                       // ������
    private ProjectileAxe _projectile;                                          // ������Ÿ��
    private List<ProjectileAxe> _projectileList = new List<ProjectileAxe>();    // ������Ÿ�� ����Ʈ

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Axe(SkillData skillData)
    {
        // �⺻ ���� �Ҵ�
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);
        _projectile = ResourceManager.Instance.SkillProjectileDict[ID] as ProjectileAxe;

        // ���� ����
        StatSetting(skillData);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        _statArgs.Atk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _delay = skillData.Delay;
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
    }

    /// <summary>
    /// ��ų ��Ÿ�� ����
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
    /// ��ų ����
    /// </summary>
    public override void UseSkill(SkillAttackArgs skillAttackArgs)
    {
        for (int i = 0; i < _projectileCount; i++)
        {
            // ������Ÿ�� ����
            ProjectileAxe projectile = GameObject.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity);

            // ������ ������Ÿ�� ���� ����
            projectile.SetStats(_statArgs);
        }
    }
}

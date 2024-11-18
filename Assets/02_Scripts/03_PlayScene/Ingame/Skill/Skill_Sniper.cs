using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Skill_Sniper : Skill_Base
{
    private ProjectileSniperArgs _statArgs = new ProjectileSniperArgs();    // ������Ÿ�� �������ۿ� �ʿ��� ���ȵ�
    private float _range;                                                   // ����ü ���� ����
    private float _delay;                                                   // ������
    private ProjectileSniper _projectile;                                   // ��ų ������Ÿ��

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Sniper(SkillData skillData)
    {
        // �⺻ ���� �Ҵ�
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);
        _projectile = ResourceManager.Instance.SkillProjectileDict[ID] as ProjectileSniper;

        // ��ų ���� ����
        StatSetting(skillData);
    }

    /// <summary>
    /// ��ų ���� ����
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        _statArgs.Atk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _statArgs.Speed = skillData.ProjectileSpeed;
        _range = skillData.Range;
        _delay = skillData.Delay;
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
        // ��Ÿ� �� ���� ����� ���� 1���� ã��
        List<Enemy> targetEnemyList =
            PlaySceneManager.Instance.EnemyManager.Get_ClosestEnemys_In_Distance(
                skillAttackArgs.StartSkillPos, 
                _range, 
                1);

        if (targetEnemyList.Count == 0)
            return;

        // ����ü ���� ���� ����
        Vector3 dir = targetEnemyList[0].transform.position - skillAttackArgs.StartSkillPos;
        dir.Normalize();
        _statArgs.Dir = dir;

        // ������Ÿ�� ����
        ProjectileSniper projectile = GameObject.Instantiate(
            _projectile, skillAttackArgs.StartSkillPos, Quaternion.identity);

        // ������Ÿ�� ���� ����
        projectile.SetStats(_statArgs);
    }
}

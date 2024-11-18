using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SlashAttack : Skill_Base
{
    private ProjectileSlashAttackStatArgs _statArgs = new ProjectileSlashAttackStatArgs();      // ������Ÿ�� �����þ��ÿ� �ʿ��� ���ȵ�
    private float _delay;                                                                       // ��ų ������
    private ProjectileSlashAttack _projectile;                                                  // ��ų ������Ÿ��

    /// <summary>
    /// ������
    /// </summary>
    public Skill_SlashAttack(SkillData skillData)
    {
        // �⺻ ���� �Ҵ�
        ID = (SkillID)Enum.Parse(typeof(SkillID), skillData.ID);
        CurrentLevel = 1;
        MaxLevel = PlaySceneManager.Instance.SkillManager.GetSkillMaxLevel(ID);
        _projectile = ResourceManager.Instance.SkillProjectileDict[ID] as ProjectileSlashAttack;

        // ���� ����
        StatSetting(skillData);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData skillData)
    {
        // ���� ����
        _statArgs.Atk = skillData.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
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
        // ������Ÿ�� ����
        ProjectileSlashAttack projectile = GameObject.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity);
        
        // ������ ������Ÿ�� ���� ����
        projectile.SetStats(_statArgs);

        // ���� ���
        AudioManager.Instance.PlaySFX(SFXType.Melee_Attack);
    }
}

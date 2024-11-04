using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// SlashAttack ��ų Ŭ���� : ��Ÿ�� bool �Ǻ� / ���ݷ���
/// </summary>
public class Skill_SlashAttack : Skill_Base
{
    // ��ų ������ ����
    private float _skillAtk;
    private float _delay;
    private ProjectileBase _projectile;

    /// <summary>
    /// ������
    /// </summary>
    public Skill_SlashAttack(SkillData skillData_SlashAttack)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_SlashAttack.ID);
        CurrentLevel = 0;
        MaxLevel = 2;

        StatSetting(skillData_SlashAttack);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData skillData_SlashAttack)
    {
        _skillAtk = skillData_SlashAttack.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _delay = skillData_SlashAttack.Delay;

        _projectile = ResourceManager.Instance.SkillProjectileDict[Id];
    }

    /// <summary>
    /// ������ �Լ� : �����Ǿ��ִ� �긦 �ٸ� �����ͷ� ������ ����
    /// </summary>
    public override void LevelUp()
    {
        SkillData skillData_SlashAttack =
            PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(Id, ++CurrentLevel);

        StatSetting(skillData_SlashAttack);
    }

    /// <summary>
    /// ��ų ��Ÿ�� ���� (�ð� ������ ���� ��ų ���� �������� true false ��ȯ)
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
        ProjectileSlashAttack projectile =
            GameObject.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity)
            as ProjectileSlashAttack;

        // ������Ÿ�Ͽ� ���ݷ� �ǳ���
        projectile.SetAtk(_skillAtk);
    }
}

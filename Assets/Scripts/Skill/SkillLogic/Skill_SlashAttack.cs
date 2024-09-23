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
    private int _atk;
    private float _delay;
    private ProjectileBase _projectile;


    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData_SlashAttack skillData_SlashAttack)
    {
        _atk = skillData_SlashAttack.Atk;
        _delay = skillData_SlashAttack.Delay;
        _projectile = skillData_SlashAttack.Projectile;
    }


    /// <summary>
    /// ������ �Լ� : �����Ǿ��ִ� �긦 �ٸ� �����ͷ� ������ ����
    /// </summary>
    public override void LevelUp()
    {
        SkillData_SlashAttack skillData_SlashAttack =
            SkillManager.Instance.SkillData_as_Dict<SkillData_SlashAttack>(Id, ++CurrentLevel);

        StatSetting(skillData_SlashAttack);
    }   

    /// <summary>
    /// ������
    /// </summary>
    public Skill_SlashAttack(SkillData_SlashAttack skillData_SlashAttack)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID),skillData_SlashAttack.Id);
        CurrentLevel = 1;
        MaxLevel = 3;

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
        //// ������Ÿ�� ����
        //ProjectileSlashAttack projectile =
        //    Object.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) 
        //    as ProjectileSlashAttack;

        //// ������Ÿ�Ͽ� ���ݷ� �ǳ���
        //projectile.SetProjectileAtk(_atk);

        //Debug.Log("SlashAttack ��ų ����");

    }
}

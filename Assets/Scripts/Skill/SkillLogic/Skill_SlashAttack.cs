using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// SlashAttack ��ų ����
/// </summary>
public class Skill_SlashAttack : Skill_Base
{

    private SkillData_SlashAttack _skillData_SlashAttack;

    // ������Ÿ��
    private ProjectileSlashAttack _spawnedProjectileSlashAttack;

    // ������
    public Skill_SlashAttack(SkillData_SlashAttack skillData_SlashAttack, Vector3 pos)
    {
        _skillData_SlashAttack = skillData_SlashAttack;    
    }

    /// <summary>
    /// ��ų ��Ÿ�� ���� (�ð� ������ ���� ��ų ���� �������� true false ��ȯ)
    /// </summary>
    public override bool SkillUpdate()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _skillData_SlashAttack.Delay)
        {
            _time %= _skillData_SlashAttack.Delay;
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
    public override void AttackFunc(SkillAttackArgs skillAttackArgs)
    {
        // ������Ÿ�� ����
        _spawnedProjectileSlashAttack =
            Object.Instantiate(_skillData_SlashAttack.Projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSlashAttack;

        // ������Ÿ�Ͽ� ���ݷ� �ǳ���
        _spawnedProjectileSlashAttack.TakeSkillAtk(_skillData_SlashAttack.Atk);

    }
}

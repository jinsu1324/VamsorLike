using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

/// <summary>
/// SlashAttack ��ų Ŭ���� : ��Ÿ�� bool �Ǻ� / ���ݷ���
/// </summary>
public class Skill_SlashAttack : Skill_Base
{

    private SkillData_SlashAttack _skillData_SlashAttack = new SkillData_SlashAttack();

    // ������Ÿ��
    private ProjectileSlashAttack _spawnedProjectileSlashAttack;



    // ������
    public Skill_SlashAttack() : base("SlashAttack", "������ ����", "������ ������ ���� �����մϴ�.", 3)
    {
        
    }


    /// <summary>
    /// ��ų ��Ÿ�� ���� (�ð� ������ ���� ��ų ���� �������� true false ��ȯ)
    /// </summary>
    public override bool SkillCooltime()
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
    public override void UseSkill(SkillAttackArgs skillAttackArgs)
    {
        //// ������Ÿ�� ����
        //_spawnedProjectileSlashAttack =
        //    Object.Instantiate(_skillData_SlashAttack.Projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSlashAttack;

        //// ������Ÿ�Ͽ� ���ݷ� �ǳ���
        //_spawnedProjectileSlashAttack.TakeSkillAtk(_skillData_SlashAttack.Atk);

        Debug.Log("SlashAttack ��ų ����");

    }
}

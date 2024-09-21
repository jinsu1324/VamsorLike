using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;


/// <summary>
/// Sniper ��ų Ŭ���� : ��Ÿ�� bool �Ǻ� / ���ݷ���
/// </summary>
public class Skill_Sniper : Skill_Base
{
    private int _atk;
    private float _projectileSpeed;
    private float _range;
    private float _delay;


    private SkillData_Sniper _skillData_Sniper = new SkillData_Sniper();
    // ������Ÿ��
    private ProjectileSniper _spawnedProjectileSniper;



    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData_Sniper skillData_Sniper)
    {
        _atk = skillData_Sniper.Atk;
        _projectileSpeed = skillData_Sniper.ProjectileSpeed;
        _range = skillData_Sniper.Range;
        _delay = skillData_Sniper.Delay;
    }


    /// <summary>
    /// ������ �Լ� : �����Ǿ��ִ� �긦 �ٸ� �����ͷ� ������ ����
    /// </summary>
    public override void LevelUp()
    {
        SkillData_Sniper skillData_Sniper =
            DataManager.Instance.SkillData_as_SkillDataDict<SkillData_Sniper>(Id, ++CurrentLevel);

        StatSetting(skillData_Sniper);

    }

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Sniper(SkillData_Sniper skillData_Sniper)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_Sniper.Id);
        CurrentLevel = 1;
        MaxLevel = 3;

        StatSetting(skillData_Sniper);

    }



    /// <summary>
    /// ��ų ��Ÿ�� ���� (�ð� ������ ���� ��ų ���� �������� true false ��ȯ)
    /// </summary>
    public override bool SkillCooltime()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _skillData_Sniper.Delay)
        {
            _time %= _skillData_Sniper.Delay;
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
        //// ��Ÿ� �� ���� ����� ���� ã��
        //MonsterObject closestTargetMonster =
        //    MonsterManager.Instance.GetClosestMonstersByDistance(skillAttackArgs.StartSkillPos, _skillData_Sniper.Range);

        //// ������Ÿ�� ����
        //_spawnedProjectileSniper =
        //    Object.Instantiate(_skillData_Sniper.Projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSniper;

        //// ���ݷ� �ǳ���
        //_spawnedProjectileSniper.TakeSkillAtk(_skillData_Sniper.Atk);

        //// ������Ÿ�� Ÿ�ٸ��ͷ� �̵�
        //_spawnedProjectileSniper.SettingProjectileInfo(closestTargetMonster);

        Debug.Log("Sniper ��ų ����");
    }
}

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
    // ���ȵ�
    private float _skillAtk;
    private float _projectileSpeed;
    private float _range;
    private float _delay;
    private ProjectileBase _projectile;

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Sniper(SkillData skillData_Sniper)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_Sniper.ID);
        CurrentLevel = 0;
        MaxLevel = 2;

        StatSetting(skillData_Sniper);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData skillData_Sniper)
    {
        _skillAtk = skillData_Sniper.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _projectileSpeed = skillData_Sniper.ProjectileSpeed;
        _range = skillData_Sniper.Range;
        _delay = skillData_Sniper.Delay;

        _projectile = ResourceManager.Instance.SkillProjectileDict[Id];
    }

    /// <summary>
    /// ������ �Լ� : �����Ǿ��ִ� �긦 �ٸ� �����ͷ� ������ ����
    /// </summary>
    public override void LevelUp()
    {
        SkillData skillData_Sniper =
            PlaySceneManager.Instance.SkillManager.GetSkillData_by_SkillIDLevel(Id, ++CurrentLevel);

        StatSetting(skillData_Sniper);
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
        // ��Ÿ� �� ���� ����� ���� ã��
        Enemy closestTargetMonster =
            PlaySceneManager.Instance.EnemyManager.Get_ClosestEnemy_In_Distance(skillAttackArgs.StartSkillPos, _range);

        // ������Ÿ�� ����
        ProjectileSniper _spawnedProjectile =
            GameObject.Instantiate(_projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSniper;

        // ���ݷ� �ǳ���
        _spawnedProjectile.SetAtk(_skillAtk);

        // ������Ÿ�� Ÿ�ٸ��ͷ� �̵�
        _spawnedProjectile.SettingProjectileInfo(closestTargetMonster);
    }
}

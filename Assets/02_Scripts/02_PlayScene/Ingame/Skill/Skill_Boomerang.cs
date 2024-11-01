using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

/// <summary>
/// Boomerang ��ų Ŭ���� : ��Ÿ�� bool �Ǻ� / ���ݷ���
/// </summary>
public class Skill_Boomerang : Skill_Base
{
    // ��ų ������ ����
    private float _skillAtk;
    private float _projectileSpeed;
    private int _projectileCount;
    private float _range;
    private ProjectileBoomerang _spawnedProjectile;

    // �θ޶� �����ص� �Ǵ���
    private bool _isBoomerangStarted = false;

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Boomerang(SkillData_Boomerang skillData_Boomerang)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_Boomerang.ID);
        CurrentLevel = 0;
        MaxLevel = 2;

        StatSetting(skillData_Boomerang);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData_Boomerang skillData_Boomerang)
    {
        _skillAtk = skillData_Boomerang.AtkPercentage * PlaySceneManager.Instance.MyHeroObj.Atk;
        _projectileSpeed = skillData_Boomerang.ProjectileSpeed;
        _projectileCount = skillData_Boomerang.ProjectileCount;
        _range = skillData_Boomerang.Range;

        CreateBoomerangProjectile(skillData_Boomerang);
    }
    
    /// <summary>
    /// ������ �Լ� : �����Ǿ��ִ� �긦 �ٸ� �����ͷ� ������ ����
    /// </summary>
    public override void LevelUp()
    {
        // �����ʿ�!
        //SkillData_Boomerang skillData_Boomerang =
        //   PlaySceneManager.Instance.SkillManager.SkillData_as_Dict<SkillData_Boomerang>(Id, ++CurrentLevel);

        //StatSetting(skillData_Boomerang);
    }

    /// <summary>
    /// �θ޶� ������Ÿ�� ����
    /// </summary>
    private void CreateBoomerangProjectile(SkillData_Boomerang skillData_Boomerang)
    {
        // ������Ÿ�� ����
        _spawnedProjectile = GameObject.Instantiate(
                skillData_Boomerang.Projectile,
                PlaySceneManager.Instance.MyHeroObj.transform.position,
                Quaternion.identity) as ProjectileBoomerang;

        // �θ޶��� �����Ǹ� ��� �����ϰԲ� ���� ����
        _isBoomerangStarted = true;
    } 

    /// <summary>
    /// ��ų ��Ÿ�� ���� (�ð� ������ ���� ��ų ���� �������� true false ��ȯ)
    /// </summary>
    public override bool SkillCooltime()
    {
        if (_isBoomerangStarted)
            return true;
        else
            return false;
    }

    /// <summary>
    /// ��ų ����
    /// </summary>
    public override void UseSkill(SkillAttackArgs skillAttackArgs)
    {
        _spawnedProjectile.AroundBoomerang(skillAttackArgs.StartSkillPos);
        _spawnedProjectile.SetAtk(_skillAtk);
    }    
}

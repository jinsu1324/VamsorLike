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
    // �θ޶� �����ص� �Ǵ���
    private bool _isBoomerangStarted = false;

    // ������Ÿ��
    private ProjectileBoomerang _SpawnedProjectileBoomerang;

    // ��ų ������ ����
    private float _skillAtk;
    private float _projectileSpeed;
    private int _projectileCount;
    private float _range;

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Boomerang(SkillData_Boomerang skillData_Boomerang)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_Boomerang.Id);
        CurrentLevel = 1;
        MaxLevel = 3;

        StatSetting(skillData_Boomerang);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData_Boomerang skillData_Boomerang)
    {
        _skillAtk = skillData_Boomerang.AtkPercentage;
        _projectileSpeed = skillData_Boomerang.ProjectileSpeed;
        _projectileCount = skillData_Boomerang.ProjectileCount;
        _range = skillData_Boomerang.Range;
        _SpawnedProjectileBoomerang = skillData_Boomerang.Projectile as ProjectileBoomerang;

        CreateBoomerangProjectile();
    }
    
    /// <summary>
    /// ������ �Լ� : �����Ǿ��ִ� �긦 �ٸ� �����ͷ� ������ ����
    /// </summary>
    public override void LevelUp()
    {
        SkillData_Boomerang skillData_Boomerang =
           SkillManager.Instance.SkillData_as_Dict<SkillData_Boomerang>(Id, ++CurrentLevel);

        StatSetting(skillData_Boomerang);
    }

    /// <summary>
    /// �θ޶� ������Ÿ�� ����
    /// </summary>
    private void CreateBoomerangProjectile()
    {
        // ������Ÿ�� ����
        GameObject.Instantiate(
            _SpawnedProjectileBoomerang,
            PlaySceneManager.ThisGameHeroObject.transform.position,
            Quaternion.identity);

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
        _SpawnedProjectileBoomerang.AroundBoomerang(skillAttackArgs.StartSkillPos);
        _SpawnedProjectileBoomerang.SetProjectileAtk(_skillAtk);
    }    
}

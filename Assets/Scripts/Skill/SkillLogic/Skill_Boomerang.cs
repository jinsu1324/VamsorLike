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

    private SkillData_Boomerang _skillData_Boomerang = new SkillData_Boomerang();

    // ������Ÿ��
    private ProjectileBoomerang _SpawnedProjectileBoomerang;


    // ��ų ������ ����
    private int _atk;
    private float _projectileSpeed;
    private int _projectileCount;
    private float _range;


    /// <summary>
    /// ���� ����
    /// </summary>
    private void StatSetting(SkillData_Boomerang skillData_Boomerang)
    {
        _atk = skillData_Boomerang.Atk;
        _projectileSpeed = skillData_Boomerang.ProjectileSpeed;
        _projectileCount = skillData_Boomerang.ProjectileCount;
        _range = skillData_Boomerang.Range;
    }

    /// <summary>
    /// ������ �Լ� : �����Ǿ��ִ� �긦 �ٸ� �����ͷ� ������ ����
    /// </summary>
    public override void LevelUp()
    {
        SkillData_Boomerang skillData_Boomerang =
            DataManager.Instance.SkillData_as_SkillDataDict<SkillData_Boomerang>(Id, ++CurrentLevel);

        StatSetting(skillData_Boomerang);
    }

    /// <summary>
    /// ������
    /// </summary>
    public Skill_Boomerang(SkillData_Boomerang skillData_Boomerang)
    {
        Id = (SkillID)Enum.Parse(typeof(SkillID), skillData_Boomerang.Id);
        CurrentLevel = 1;
        MaxLevel = 3;

        StatSetting(skillData_Boomerang);



        // // ������Ÿ�� ����
        // _SpawnedProjectileBoomerang = Object.Instantiate(
        //    _skillData_Boomerang.Projectile,
        //    PlaySceneManager.ThisGameHeroObject.transform.position,
        //    Quaternion.identity)
        //as ProjectileBoomerang;

        // // �θ޶��� �����Ǹ� ��� �����ϰԲ� ���� ����
        // _isBoomerangStarted = true;
    }

    


    /// <summary>
    /// ��ų ��Ÿ�� ���� (�ð� ������ ���� ��ų ���� �������� true false ��ȯ)
    /// </summary>
    public override bool SkillCooltime()
    {
        // �θ޶��� �����Ǹ� ��� ���ƾ��Ѵ�.
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
        //_SpawnedProjectileBoomerang.AroundBoomerang(skillAttackArgs.StartSkillPos);
        //_SpawnedProjectileBoomerang.TakeSkillAtk(_skillData_Boomerang.Atk);

        Debug.Log("Boomerang ��ų ����");
    }    
}

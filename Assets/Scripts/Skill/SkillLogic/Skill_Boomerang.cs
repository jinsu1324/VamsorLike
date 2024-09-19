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

    // ������
    public Skill_Boomerang(/*SkillData_Boomerang skillData_Boomerang*/)
    {      
        //// ������Ÿ�� ����
        // _SpawnedProjectileBoomerang = Object.Instantiate(
        //    _skillData_Boomerang.Projectile, 
        //    PlaySceneManager.ThisGameHeroObject.transform.position, 
        //    Quaternion.identity) 
        //as ProjectileBoomerang;

        //// �θ޶��� �����Ǹ� ��� �����ϰԲ� ���� ����
        //_isBoomerangStarted = true;
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

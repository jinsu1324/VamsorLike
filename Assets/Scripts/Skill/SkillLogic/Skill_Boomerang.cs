using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

// ��ų �θ޶� ���ݷ���, ��Ÿ��
public class Skill_Boomerang : Skill_Base
{
    // �θ޶� �����ص� �Ǵ���
    private bool _isBoomerangStarted = false;

    // ������Ÿ��
    private ProjectileBoomerang _SpawnedProjectileBoomerang;

    // ������
    public Skill_Boomerang(SkillData_Base skillData, Vector3 pos)
    {
        // ��ų �θ޶��� �ν��Ͻ�ȭ �Ǵ� ����, �Ű������� �޾ƿ� �����Ͱ��� �� SkillBoomerang�� �����ͷ� �־���
        //_skillData = skillData;

        // ������Ÿ�� ����
        // Imsi _SpawnedProjectileBoomerang = 
        // Imsi Object.Instantiate(skillData.Projectile, pos, Quaternion.identity) as ProjectileBoomerang;

        // �θ޶��� �����Ǹ� ��� �����ϰԲ� ���� ����
        //_isBoomerangStarted = true;
    }

    // ��ų ���� �������� true false ��ȯ
    public override bool SkillUpdate()
    {
        // �θ޶��� �����Ǹ� ��� ���ƾ��Ѵ�.
        if (_isBoomerangStarted)
            return true;
        else
            return false;
    }

    // ���� ����
    public override void AttackFunc(SkillAttackArgs skillAttackArgs)
    {
        //_SpawnedProjectileBoomerang.AroundBoomerang(skillAttackArgs.StartSkillPos);
        //_SpawnedProjectileBoomerang.TakeSkillAtk(_skillData.Atk);
    }    
}

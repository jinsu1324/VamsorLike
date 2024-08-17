using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

// ��ų �θ޶� ���ݷ���, ��Ÿ��
public class SkillBoomerang : Skill
{
    // �θ޶� �����ص� �Ǵ���
    private bool _isBoomerangStarted = false;

    // ������Ÿ��
    private Projectile _SpawnedProjectile;

    // ������
    public SkillBoomerang(SkillData skillData, Vector3 pos)
    {
        // ��ų �θ޶��� �ν��Ͻ�ȭ �Ǵ� ����, �Ű������� �޾ƿ� �����Ͱ��� �� SkillBoomerang�� �����ͷ� �־���
        _skillData = skillData;


        // ������Ÿ�� ����
        _SpawnedProjectile = Object.Instantiate(_skillData.Projectile, pos, Quaternion.identity);

        // �θ޶��� �����Ǹ� ��� �����ϰԲ� ���� ����
        _isBoomerangStarted = true;
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
    public override void AttackFunc(Vector3 skillPos)
    {
        _SpawnedProjectile.RotateProjectile(skillPos);
    }

    
}

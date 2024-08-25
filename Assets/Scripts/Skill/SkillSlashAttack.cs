using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class SkillSlashAttack : SkillBase
{
    // ������Ÿ��
    private ProjectileSlashAttack _spawnedProjectileSlashAttack;

    // ������
    public SkillSlashAttack(SkillData skillData, Vector3 pos)
    {
        // ��ų �����þ����� �ν��Ͻ�ȭ �Ǵ� ����, �Ű������� �޾ƿ� �����Ͱ��� �� SkillSlashAttack�� �����ͷ� �־���
        _skillData = skillData;    
    }

    // ��ų ��Ÿ�� ���� (��ų ���� �������� true false ��ȯ)
    public override bool SkillUpdate()
    {
        _time += Time.fixedDeltaTime;

        if (_time > _skillData.Delay)
        {
            _time %= _skillData.Delay;
            return true;
        }
        else
        {
            return false;
        }

    }

    // ���� ����
    public override void AttackFunc(SkillAttackArgs skillAttackArgs)
    {
        // ������Ÿ�� ����
        _spawnedProjectileSlashAttack =
            Object.Instantiate(_skillData.Projectile, skillAttackArgs.StartSkillPos, Quaternion.identity) as ProjectileSlashAttack;

        // ������Ÿ�Ͽ� ���ݷ� �ǳ���
        _spawnedProjectileSlashAttack.TakeSkillAtk(_skillData.Atk);

    }
}

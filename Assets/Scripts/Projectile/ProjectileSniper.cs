using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSniper : Projectile
{
    private MonsterObject _targetMonster;

    private void FixedUpdate()
    {
        MoveToTarget();

        if (IsArrivedAtMonster())
        {
            _targetMonster.HPMinus(_skillAtk);
            ProjectileDestroy();
        }
    }

    // ������Ÿ�� �ʿ� ������ ����
    public void SettingProjectileInfo(MonsterObject targetMonster)
    {
        _targetMonster = targetMonster;
    }


    // ������Ÿ�� ����
    public void ProjectileDestroy()
    {
        Destroy(gameObject);
    }   


    // ������Ÿ�� �̵�
    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetMonster.transform.position,
            10.0f * Time.fixedDeltaTime);
    }


    // �Ѿ��� ���Ϳ� �����ߴ��� bool�� ��ȯ
    private bool IsArrivedAtMonster()
    {
        if (0.1f >= Vector3.Distance(transform.position, _targetMonster.transform.position))
        {
            return true;
        }

        return false;
    }
}

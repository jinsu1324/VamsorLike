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

    // 프로젝타일 필요 정보들 셋팅
    public void SettingProjectileInfo(MonsterObject targetMonster)
    {
        _targetMonster = targetMonster;
    }


    // 프로젝타일 삭제
    public void ProjectileDestroy()
    {
        Destroy(gameObject);
    }   


    // 프로젝타일 이동
    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetMonster.transform.position,
            10.0f * Time.fixedDeltaTime);
    }


    // 총알이 몬스터에 도착했는지 bool값 반환
    private bool IsArrivedAtMonster()
    {
        if (0.1f >= Vector3.Distance(transform.position, _targetMonster.transform.position))
        {
            return true;
        }

        return false;
    }
}

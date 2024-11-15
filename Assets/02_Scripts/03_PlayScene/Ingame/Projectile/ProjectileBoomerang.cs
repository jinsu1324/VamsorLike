using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileBoomerangStatArgs
{
    public float Atk;
    public float Speed;
    public float Range;
}

public class ProjectileBoomerang : ProjectileBase
{
    private float _range;               // 범위 (반지름)
    private float _speed;               // 속도
    private float _angleOffset;         // 부메랑의 초기 각도 오프셋
    private float _currentAngle = 0;    // 현재 각도

    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    public void SetStats(ProjectileBoomerangStatArgs projectileBoomerangStatArgs)
    {
        _atk = projectileBoomerangStatArgs.Atk;
        _range = projectileBoomerangStatArgs.Range;
        _speed = projectileBoomerangStatArgs.Speed;
    }

    /// <summary>
    /// 이펙트 재생
    /// </summary>
    protected override void PlayEffect(Collider2D collision)
    {
        PlaySceneManager.Instance.EffectManager.GetEffect(
            EffectName.FX_Hit.ToString(),
            collision.gameObject.transform.position);
    }

    /// <summary>
    /// 부메랑 영웅 주변으로 회전
    /// </summary>
    public void AroundBoomerang(Vector3 centerPos, float angleOffset)
    {
        // 지속적인 회전
        _currentAngle += _speed * Time.fixedDeltaTime;

        // 부메랑마다 고유 각도 오프셋 추가해줘서 간격 유지하도록
        float angle = _currentAngle + angleOffset;    

        // 부메랑 위치 계산
        transform.position = centerPos + new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        ) * _range;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileSniperArgs
{
    public float Atk;
    public float Speed;
    public Vector3 Dir;
}

public class ProjectileSniper : ProjectileBase
{
    private float _speed;               // 투사체 속도
    private Vector3 _dir;               // 투사체 이동 방향
    private float _maxLifetime = 2f;    // 투사체 생명 주기
    private float _lifetime;            // 투사체 생명 주기 체크

    /// <summary>
    /// FixedUpdate
    /// </summary>
    private void FixedUpdate()
    {
        // 투사체 움직임
        transform.position += _dir * _speed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        // 생명 주기 체크
        _lifetime += Time.deltaTime;

        // 생명 주기가 끝나면 제거
        if (_lifetime > _maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    public void SetStats(ProjectileSniperArgs statArgs)
    {
        _atk = statArgs.Atk;
        _speed = statArgs.Speed;
        _dir = statArgs.Dir;
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
}

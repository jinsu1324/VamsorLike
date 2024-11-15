using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileAxeArgs
{
    public float Atk;
}

public class ProjectileAxe : ProjectileBase
{
    [SerializeField]
    private Rigidbody2D rigidBody2D;    // 리지드바디

    private float _initialSpeed = 15f;  // 초기 위로 솟아오르는 속도
    private float _gravityScale = 2f;   // 중력 비율 (Rigidbody2D의 중력 스케일 조정)
    private float _maxLifetime = 2f;    // 투사체 생명 주기
    private float _lifetime;            // 투사체 생명 주기 체크
    private float minAngle = -15f;      // 최소 발사 각도 (기준 위쪽)
    private float maxAngle = 15f;       // 최대 발사 각도 (기준 위쪽)

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // 랜덤한 발사 각도 계산
        float randomAngle = Random.Range(minAngle, maxAngle);
        Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

        // 초기 속도와 방향 설정
        rigidBody2D.velocity = direction * _initialSpeed;

        // 중력 스케일 설정
        rigidBody2D.gravityScale = _gravityScale;
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
    public void SetStats(ProjectileAxeArgs statArgs)
    {
        _atk = statArgs.Atk;
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

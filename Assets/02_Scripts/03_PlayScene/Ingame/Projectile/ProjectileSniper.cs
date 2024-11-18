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
    private float _speed;               // ����ü �ӵ�
    private Vector3 _dir;               // ����ü �̵� ����
    private float _maxLifetime = 2f;    // ����ü ���� �ֱ�
    private float _lifetime;            // ����ü ���� �ֱ� üũ

    /// <summary>
    /// FixedUpdate
    /// </summary>
    private void FixedUpdate()
    {
        // ����ü ������
        transform.position += _dir * _speed * Time.fixedDeltaTime;
    }

    /// <summary>
    /// Update
    /// </summary>
    private void Update()
    {
        // ���� �ֱ� üũ
        _lifetime += Time.deltaTime;

        // ���� �ֱⰡ ������ ����
        if (_lifetime > _maxLifetime)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void SetStats(ProjectileSniperArgs statArgs)
    {
        _atk = statArgs.Atk;
        _speed = statArgs.Speed;
        _dir = statArgs.Dir;
    }

    /// <summary>
    /// ����Ʈ ���
    /// </summary>
    protected override void PlayEffect(Collider2D collision)
    {
        PlaySceneManager.Instance.EffectManager.GetEffect(
            EffectName.FX_Hit.ToString(),
            collision.gameObject.transform.position);
    }
}

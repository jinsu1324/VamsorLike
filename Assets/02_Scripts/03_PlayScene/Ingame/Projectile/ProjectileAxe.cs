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
    private Rigidbody2D rigidBody2D;    // ������ٵ�

    private float _initialSpeed = 15f;  // �ʱ� ���� �ھƿ����� �ӵ�
    private float _gravityScale = 2f;   // �߷� ���� (Rigidbody2D�� �߷� ������ ����)
    private float _maxLifetime = 2f;    // ����ü ���� �ֱ�
    private float _lifetime;            // ����ü ���� �ֱ� üũ
    private float minAngle = -15f;      // �ּ� �߻� ���� (���� ����)
    private float maxAngle = 15f;       // �ִ� �߻� ���� (���� ����)

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        // ������ �߻� ���� ���
        float randomAngle = Random.Range(minAngle, maxAngle);
        Vector2 direction = Quaternion.Euler(0, 0, randomAngle) * Vector2.up;

        // �ʱ� �ӵ��� ���� ����
        rigidBody2D.velocity = direction * _initialSpeed;

        // �߷� ������ ����
        rigidBody2D.gravityScale = _gravityScale;
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
    public void SetStats(ProjectileAxeArgs statArgs)
    {
        _atk = statArgs.Atk;
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

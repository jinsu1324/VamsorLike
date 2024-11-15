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
    private float _range;               // ���� (������)
    private float _speed;               // �ӵ�
    private float _angleOffset;         // �θ޶��� �ʱ� ���� ������
    private float _currentAngle = 0;    // ���� ����

    /// <summary>
    /// ���� ����
    /// </summary>
    public void SetStats(ProjectileBoomerangStatArgs projectileBoomerangStatArgs)
    {
        _atk = projectileBoomerangStatArgs.Atk;
        _range = projectileBoomerangStatArgs.Range;
        _speed = projectileBoomerangStatArgs.Speed;
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

    /// <summary>
    /// �θ޶� ���� �ֺ����� ȸ��
    /// </summary>
    public void AroundBoomerang(Vector3 centerPos, float angleOffset)
    {
        // �������� ȸ��
        _currentAngle += _speed * Time.fixedDeltaTime;

        // �θ޶����� ���� ���� ������ �߰����༭ ���� �����ϵ���
        float angle = _currentAngle + angleOffset;    

        // �θ޶� ��ġ ���
        transform.position = centerPos + new Vector3(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        ) * _range;
    }
}
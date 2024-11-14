using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SlashAttack ��ų�� ������Ÿ��
/// </summary>
public class ProjectileSlashAttack : ProjectileBase
{
    [SerializeField]
    private BoxCollider2D _boxCollider2D;           // ���� ���� �Ǻ� �ݶ��̴�

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Invoke("OFF_Collider", 0.1f);
    }

    /// <summary>
    /// �ݶ��̴� ����
    /// </summary>
    private void OFF_Collider()
    {
        _boxCollider2D.enabled = false;
    }

    /// <summary>
    /// ���Ϳ� �浹���� ��, ���ݷ¸�ŭ ������ HP�� ����
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Layer.Enemy.ToString()))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // ���� �׾����� �׳� ����
            if (enemy.IsDead == true)
                return;

            collision.gameObject.GetComponent<Enemy>().HPMinus(_atk);

            PlaySceneManager.Instance.EffectManager.GetEffect(
                EffectName.FX_Hit.ToString(),
                collision.gameObject.transform.position);
        }
    }

}

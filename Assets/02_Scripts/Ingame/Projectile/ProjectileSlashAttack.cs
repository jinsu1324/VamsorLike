using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SlashAttack ��ų�� ������Ÿ��
/// </summary>
public class ProjectileSlashAttack : ProjectileBase
{
    private void Start()
    {
        // 0.1�� �Ŀ� �ı�
        Destroy(gameObject, 0.1f);
    }

    /// <summary>
    /// ���Ϳ� �浹���� ��, ���ݷ¸�ŭ ������ HP�� ����
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Enemy.ToString())
        {
            collision.GetComponent<Enemy>().HPMinus(_atk);
        }
    }

}

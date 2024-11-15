using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : ObjectPoolObject
{   
    protected float _atk;           // ��ų ���ݷ�

    /// <summary>
    /// ����Ʈ ���
    /// </summary>
    protected abstract void PlayEffect(Collider2D collision);
    
    /// <summary>
    /// �� ���� �� ����
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Layer.Enemy.ToString()))
        {
            // ���� �׾����� �׳� ����
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.IsDead == true)
                return;

            // �� ü�� ����
            collision.gameObject.GetComponent<Enemy>().HPMinus(_atk);

            // ����Ʈ ���
            PlayEffect(collision);
        }
    }

}

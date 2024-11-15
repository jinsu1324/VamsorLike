using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : ObjectPoolObject
{   
    protected float _atk;           // 스킬 공격력

    /// <summary>
    /// 이펙트 재생
    /// </summary>
    protected abstract void PlayEffect(Collider2D collision);
    
    /// <summary>
    /// 적 감지 및 공격
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Layer.Enemy.ToString()))
        {
            // 적이 죽었으면 그냥 리턴
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy.IsDead == true)
                return;

            // 적 체력 감소
            collision.gameObject.GetComponent<Enemy>().HPMinus(_atk);

            // 이펙트 재생
            PlayEffect(collision);
        }
    }

}

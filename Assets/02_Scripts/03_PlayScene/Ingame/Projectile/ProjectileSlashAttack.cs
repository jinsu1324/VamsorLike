using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SlashAttack 스킬의 프로젝타일
/// </summary>
public class ProjectileSlashAttack : ProjectileBase
{
    [SerializeField]
    private BoxCollider2D _boxCollider2D;           // 몬스터 접촉 판별 콜라이더

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Invoke("OFF_Collider", 0.1f);
    }

    /// <summary>
    /// 콜라이더 끄기
    /// </summary>
    private void OFF_Collider()
    {
        _boxCollider2D.enabled = false;
    }

    /// <summary>
    /// 몬스터와 충돌했을 때, 공격력만큼 몬스터의 HP를 깎음
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Layer.Enemy.ToString()))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // 적이 죽었으면 그냥 리턴
            if (enemy.IsDead == true)
                return;

            collision.gameObject.GetComponent<Enemy>().HPMinus(_atk);

            PlaySceneManager.Instance.EffectManager.GetEffect(
                EffectName.FX_Hit.ToString(),
                collision.gameObject.transform.position);
        }
    }

}

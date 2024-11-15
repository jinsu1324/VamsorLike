using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ProjectileSlashAttackStatArgs
{
    public float Atk;
}


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
    /// 스탯 셋팅
    /// </summary>
    public void SetStats(ProjectileSlashAttackStatArgs projectileSlashAttackStatArgs)
    {
        _atk = projectileSlashAttackStatArgs.Atk;
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

    /// <summary>
    /// 콜라이더 끄기
    /// </summary>
    private void OFF_Collider()
    {
        _boxCollider2D.enabled = false;
    }

}

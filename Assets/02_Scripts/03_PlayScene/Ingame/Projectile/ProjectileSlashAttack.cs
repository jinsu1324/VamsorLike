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
    private BoxCollider2D _boxCollider2D;           // ���� ���� �Ǻ� �ݶ��̴�

    /// <summary>
    /// Start
    /// </summary>
    private void Start()
    {
        Invoke("OFF_Collider", 0.1f);
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void SetStats(ProjectileSlashAttackStatArgs projectileSlashAttackStatArgs)
    {
        _atk = projectileSlashAttackStatArgs.Atk;
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
    /// �ݶ��̴� ����
    /// </summary>
    private void OFF_Collider()
    {
        _boxCollider2D.enabled = false;
    }

}

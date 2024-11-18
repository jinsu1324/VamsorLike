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
    public void SetStats(ProjectileSlashAttackStatArgs statArgs)
    {
        _atk = statArgs.Atk;
        _isAudioPlayed = false;
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
    /// ����� ���
    /// </summary>
    protected override void PlayAudio()
    {
        if (_isAudioPlayed == false)
        {
            AudioManager.Instance.PlaySFX(SFXType.Melee_Hit);
            _isAudioPlayed = true;
        }
    }

    /// <summary>
    /// �ݶ��̴� ����
    /// </summary>
    private void OFF_Collider()
    {
        _boxCollider2D.enabled = false;
    }

}

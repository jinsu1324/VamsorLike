using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct ProjectileMeteorArgs
{
    public float Atk;
}

public class ProjectileMeteor : ProjectileBase
{
    [SerializeField]
    private BoxCollider2D _boxCollider2D;           // ���� ���� �Ǻ� �ݶ��̴�

    /// <summary>
    /// ���� ����
    /// </summary>
    public void SetStats(ProjectileMeteorArgs statArgs)
    {
        _atk = statArgs.Atk;
        _boxCollider2D.enabled = false;
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
            AudioManager.Instance.PlaySFX(SFXType.MagicWideArea_Hit);
            _isAudioPlayed = true;
        }
    }

    /// <summary>
    /// �ݶ��̴� �ѱ�
    /// </summary>
    public void OnCollider()
    {
        _boxCollider2D.enabled = true;
        Invoke("OFFCollider", 0.1f);
    }

    /// <summary>
    /// �ݶ��̴� ����
    /// </summary>
    private void OFFCollider()
    {
        _boxCollider2D.enabled = false;
    }

    /// <summary>
    /// ���ӿ�����Ʈ �ı�
    /// </summary>
    public void DestroyGO()
    {
        Destroy(gameObject);
    }

    
}

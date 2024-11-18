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
    private BoxCollider2D _boxCollider2D;           // 몬스터 접촉 판별 콜라이더

    /// <summary>
    /// 스탯 셋팅
    /// </summary>
    public void SetStats(ProjectileMeteorArgs statArgs)
    {
        _atk = statArgs.Atk;
        _boxCollider2D.enabled = false;
        _isAudioPlayed = false;
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
    /// 오디오 재생
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
    /// 콜라이더 켜기
    /// </summary>
    public void OnCollider()
    {
        _boxCollider2D.enabled = true;
        Invoke("OFFCollider", 0.1f);
    }

    /// <summary>
    /// 콜라이더 끄기
    /// </summary>
    private void OFFCollider()
    {
        _boxCollider2D.enabled = false;
    }

    /// <summary>
    /// 게임오브젝트 파괴
    /// </summary>
    public void DestroyGO()
    {
        Destroy(gameObject);
    }

    
}

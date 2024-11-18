using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : ItemBase
{
    /// <summary>
    /// 자석아이템 획득 요청
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        collision.gameObject.GetComponent<HeroObj>().AcquireMagnet_and_Request();
        
        base.ItemPickUp(collision);

        // 오디오 재생
        AudioManager.Instance.PlaySFX(SFXType.GetMagnetItem);
    }
}

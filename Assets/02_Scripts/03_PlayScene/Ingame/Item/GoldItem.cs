using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : ItemBase
{
    private int _goldAmount = 10;    // °ñµå ¾ç

    /// <summary>
    /// Gold È¹µæ ¿äÃ»
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        collision.gameObject.GetComponent<HeroObj>().AcquireGold_and_Request(_goldAmount);

        PlaySceneCanvas.Instance.PlayAchivementUI.MoveIconManager.StartMove_IconToTargetIcon(this.gameObject.transform);

        base.ItemPickUp(collision);

        // ¿Àµð¿À Àç»ý
        AudioManager.Instance.PlaySFX(SFXType.GetGoldItem);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : ItemBase
{
    private int _goldAmount = 1;    // ∞ÒµÂ æÁ

    /// <summary>
    /// Gold »πµÊ ø‰√ª
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        base.ItemPickUp(collision);
        Debug.Log($"{_goldAmount} ∞ÒµÂ∏¶ »πµÊ«ﬂΩ¿¥œ¥Ÿ.");
        collision.GetComponent<HeroObj>().AcquireGold_and_Request(_goldAmount);
    }
}

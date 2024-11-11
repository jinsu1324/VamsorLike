using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPItem : ItemBase
{
    private int _expAmount = 10;    // °æÇèÄ¡ ¾ç

    
    /// <summary>
    /// EXP È¹µæ ¿äÃ»
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        collision.gameObject.GetComponent<HeroObj>().AcquireExp_and_Request(_expAmount);

        base.ItemPickUp(collision);
    }
}

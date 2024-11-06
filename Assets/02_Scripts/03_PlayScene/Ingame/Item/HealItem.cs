using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    private int _healAmount = 50;    // Èú·®

    /// <summary>
    /// Heal È¹µæ ¿äÃ»
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        collision.gameObject.GetComponent<HeroObj>().Heal(_healAmount);

        Debug.Log($"{_healAmount} ¸¸Å­ ÈúÀÌ µÇ¾ú½À´Ï´Ù.");

        base.ItemPickUp(collision);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    private int _healAmount = 50;    // Èú·®

    /// <summary>
    /// EXP È¹µæ ¿äÃ»
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        base.ItemPickUp(collision);
        Debug.Log($"{_healAmount} ¸¸Å­ ÈúÀÌ µÇ¾ú½À´Ï´Ù.");
        collision.GetComponent<HeroObj>().Heal(_healAmount);
    }
}

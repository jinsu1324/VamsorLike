using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldItem : ItemBase
{
    private int _goldAmount = 1;    // ��� ��

    /// <summary>
    /// Gold ȹ�� ��û
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        collision.GetComponent<HeroObj>().AcquireGold_and_Request(_goldAmount);
        
        Debug.Log($"{_goldAmount} ��带 ȹ���߽��ϴ�.");
        
        base.ItemPickUp(collision);
    }
}

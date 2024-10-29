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
        base.ItemPickUp(collision);
        Debug.Log($"{_goldAmount} ��带 ȹ���߽��ϴ�.");
        collision.GetComponent<HeroObj>().AcquireGold_and_Request(_goldAmount);
    }
}

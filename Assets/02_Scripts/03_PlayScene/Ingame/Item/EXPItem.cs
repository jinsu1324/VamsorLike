using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPItem : ItemBase
{
    private int _expAmount = 10;    // 경험치 양

    /// <summary>
    /// EXP 획득 요청
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        collision.gameObject.GetComponent<HeroObj>().AcquireExp_and_Request(_expAmount);
        
        Debug.Log($"{_expAmount} 경험치를 획득했습니다.");
        
        base.ItemPickUp(collision);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPItem : ItemBase
{
    private int _expAmount = 10;    // ����ġ ��

    /// <summary>
    /// EXP ȹ�� ��û
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        base.ItemPickUp(collision);
        Debug.Log($"{_expAmount} ����ġ�� ȹ���߽��ϴ�.");
        collision.GetComponent<HeroObj>().AcquireExp_RequestLevelManager(_expAmount);
    }
}

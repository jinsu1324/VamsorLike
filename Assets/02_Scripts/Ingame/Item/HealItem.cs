using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    private int _healAmount = 50;    // ����

    /// <summary>
    /// EXP ȹ�� ��û
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        base.ItemPickUp(collision);
        Debug.Log($"{_healAmount} ��ŭ ���� �Ǿ����ϴ�.");
        collision.GetComponent<HeroObj>().Heal(_healAmount);
    }
}

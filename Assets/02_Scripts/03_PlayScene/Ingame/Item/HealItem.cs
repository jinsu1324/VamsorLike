using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    private int _healAmount = 50;    // ����

    /// <summary>
    /// Heal ȹ�� ��û
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        collision.gameObject.GetComponent<HeroObj>().Heal(_healAmount);

        Debug.Log($"{_healAmount} ��ŭ ���� �Ǿ����ϴ�.");

        base.ItemPickUp(collision);
    }
}

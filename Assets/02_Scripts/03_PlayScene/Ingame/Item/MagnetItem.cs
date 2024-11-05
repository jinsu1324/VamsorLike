using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : ItemBase
{
    /// <summary>
    /// �ڼ������� ȹ�� ��û
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        collision.GetComponent<HeroObj>().AcquireMagnet_and_Request();

        Debug.Log($" �ڼ� �������� ȹ���Ͽ����ϴ�.");

        base.ItemPickUp(collision);
    }
}

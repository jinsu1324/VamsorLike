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
        collision.gameObject.GetComponent<HeroObj>().AcquireMagnet_and_Request();
        
        base.ItemPickUp(collision);
    }
}

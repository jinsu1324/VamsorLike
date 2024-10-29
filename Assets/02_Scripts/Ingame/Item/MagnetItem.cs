using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetItem : ItemBase
{
    /// <summary>
    /// 자석아이템 획득 요청
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {
        collision.GetComponent<HeroObj>().AcquireMagnet_and_Request();

        Debug.Log($" 자석 아이템을 획득하였습니다.");

        base.ItemPickUp(collision);
    }
}

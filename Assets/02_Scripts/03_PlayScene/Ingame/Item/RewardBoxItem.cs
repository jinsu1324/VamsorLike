using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBoxItem : ItemBase
{
    /// <summary>
    /// 보상상자 획득 요청
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {        
        collision.GetComponent<HeroObj>().AcquireRewardBox_and_Request();        

        Debug.Log($"보상상자를 획득했습니다.");

        base.ItemPickUp(collision);
    }
}

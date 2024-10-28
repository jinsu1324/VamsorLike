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
        base.ItemPickUp(collision);
        Debug.Log($"보상상자를 획득했습니다.");
        collision.GetComponent<HeroObj>().AcquireRewardBox_RequestRewardBoxPopup();        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBoxItem : ItemBase
{
    /// <summary>
    /// ∫∏ªÛªÛ¿⁄ »πµÊ ø‰√ª
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {        
        collision.gameObject.GetComponent<HeroObj>().AcquireRewardBox_and_Request();
        
        base.ItemPickUp(collision);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardBoxItem : ItemBase
{
    /// <summary>
    /// ������� ȹ�� ��û
    /// </summary>
    protected override void ItemPickUp(Collider2D collision)
    {        
        base.ItemPickUp(collision);
        Debug.Log($"������ڸ� ȹ���߽��ϴ�.");
        collision.GetComponent<HeroObj>().AcquireRewardBox_RequestRewardBoxPopup();        
    }
}

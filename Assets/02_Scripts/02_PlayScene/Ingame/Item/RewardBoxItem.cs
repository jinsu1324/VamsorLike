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
        collision.GetComponent<HeroObj>().AcquireRewardBox_and_Request();        

        Debug.Log($"������ڸ� ȹ���߽��ϴ�.");

        base.ItemPickUp(collision);
    }
}

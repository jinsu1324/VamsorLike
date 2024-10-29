using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemID
{
    EXP,
    Gold,
    RewardBox
}

public abstract class ItemBase : ObjectPoolObject
{
    /// <summary>
    /// 아이템이 영웅에 닿으면 실행
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Hero.ToString())
        {
            ItemPickUp(collision);
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 아이템을 주웠을 때 행동을 정의할 추상 함수
    /// </summary>
    protected virtual void ItemPickUp(Collider2D collision)
    {
        // 기본 효과 있으면 여기에 구현
    }
}

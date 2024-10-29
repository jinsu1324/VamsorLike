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
    /// �������� ������ ������ ����
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
    /// �������� �ֿ��� �� �ൿ�� ������ �߻� �Լ�
    /// </summary>
    protected virtual void ItemPickUp(Collider2D collision)
    {
        // �⺻ ȿ�� ������ ���⿡ ����
    }
}

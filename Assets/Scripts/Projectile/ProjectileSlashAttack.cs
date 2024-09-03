using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSlashAttack : ProjectileBase
{
    private void Start()
    {
        AttackAfterDestroy(0.1f);
    }

    public void AttackAfterDestroy(float delay)
    {
        Destroy(this.gameObject, delay);
    }


    // ���Ϳ� �浹������ ���ݷ¸�ŭ ���� ü�±��
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Monster.ToString())
        {
            collision.GetComponent<MonsterObject>().HPMinus(_skillAtk);
        }
    }

}

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


    // 몬스터와 충돌했을때 공격력만큼 몬스터 체력깎기
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Monster.ToString())
        {
            collision.GetComponent<MonsterObject>().HPMinus(_skillAtk);
        }
    }

}

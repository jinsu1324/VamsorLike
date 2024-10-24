using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SlashAttack 스킬의 프로젝타일
/// </summary>
public class ProjectileSlashAttack : ProjectileBase
{
    private void Start()
    {
        // 0.1초 후에 파괴
        Destroy(gameObject, 0.1f);
    }

    /// <summary>
    /// 몬스터와 충돌했을 때, 공격력만큼 몬스터의 HP를 깎음
    /// </summary>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Enemy.ToString())
        {
            collision.GetComponent<Enemy>().HPMinus(_atk);
        }
    }

}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 프로젝타일 부모
public class Projectile : SerializedMonoBehaviour
{   
    // 스킬의 공격력을 받아올 변수
    protected int _skillAtk;    


    // 스킬의 공격력을 받아옴
    public void TakeSkillAtk(int atk)
    {
        _skillAtk = atk;
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

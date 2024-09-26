using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBoomerang : ProjectileBase
{
    private float _radius = 2.0f; // 반지름
    private float _speed = 2.0f;  // 속도
    private float _angle = 0; // 각도 저장할 변수

    // 부메랑 영웅 주변으로 회전
    public void AroundBoomerang(Vector3 pos)
    {        
        _angle += _speed * Time.fixedDeltaTime;
        transform.position = pos + new Vector3(Mathf.Cos(_angle), Mathf.Sin(_angle), 0) * _radius;
    }

    // 몬스터와 충돌했을때 공격력만큼 몬스터 체력깎기
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tag.Monster.ToString())
        {
            collision.GetComponent<MonsterObject>().HPMinus(_atk);
        }
    }
}
